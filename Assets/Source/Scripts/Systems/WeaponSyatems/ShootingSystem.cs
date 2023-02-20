using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ShootingSystem))]
public sealed class ShootingSystem : UpdateSystem
{
    [SerializeField] private ParticleSystem _impactEffect;
    [SerializeField] private float _impactEffectLifetime;

    private Filter _weaponsFilter;
    private Stash<Weapon> _weaponStash;

    public override void OnAwake()
    {
        _weaponsFilter = World.Filter.With<Weapon>().With<HammerPressed>().Without<Reload>();
        _weaponStash = World.GetStash<Weapon>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _weaponsFilter)
        {
            ref Weapon weapon = ref _weaponStash.Get(entity);
            Shoot(weapon.ShootPoint.position, weapon.ShootPoint.forward, 50f, weapon.Damage);
            entity.AddComponent<Reload>().Time = weapon.IntervalBetweenShoots;
        }
    }

    private void Shoot(Vector3 origin, Vector3 direction, float maxDistance, float damage)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, maxDistance))
        {
            ParticleSystem impactEffect = Instantiate(_impactEffect, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal) * Quaternion.Euler(90, 0, 0));
            Destroy(impactEffect.gameObject, _impactEffectLifetime);

            if (hitInfo.collider.TryGetComponent(out DamagableProvider damagableProvider))
            {
                Entity entity = damagableProvider.Entity;

                if (entity.Has<TakeDamageComponent>())
                {
                    ref TakeDamageComponent takeDamageComponent = ref entity.GetComponent<TakeDamageComponent>();
                    SetDamage(ref takeDamageComponent, damage);
                }
                else
                {
                    ref TakeDamageComponent takeDamageComponent = ref entity.AddComponent<TakeDamageComponent>();
                    SetDamage(ref takeDamageComponent, damage);
                }
            }
        }
    }

    private void SetDamage(ref TakeDamageComponent takeDamageComponent, float damage)
    {
        takeDamageComponent.Damage += damage;
    }
}
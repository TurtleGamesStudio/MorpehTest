using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem
{
    private Filter _filter;
    private Stash<Health> _healthStash;

    public override void OnAwake()
    {
        _filter = World.Filter.With<Damagable>().With<Health>();
        _healthStash = World.GetStash<Health>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref Health health = ref _healthStash.Get(entity);

            if (health.Value == 0)
            {
                Die(entity);
            }
        }
    }

    private void Die(Entity entity)
    {
        Transform transform = entity.GetComponent<Damagable>().Transform;
        entity.RemoveComponent<Damagable>();
        entity.AddComponent<DieEvent>().Transform = transform;
    }
}
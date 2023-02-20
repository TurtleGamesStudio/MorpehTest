using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem
{
    private Filter _filter;
    private Stash<Health> _healthStash;
    private Stash<TakeDamageComponent> _damageStash;

    public override void OnAwake()
    {
        _filter = World.Filter.With<TakeDamageComponent>().With<Damagable>().With<Health>();
        _healthStash = World.GetStash<Health>();
        _damageStash = World.GetStash<TakeDamageComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            ref Health health = ref _healthStash.Get(entity);
            ref TakeDamageComponent takeDamageComponent = ref _damageStash.Get(entity);
            TakeDamage(entity, ref health, takeDamageComponent);
        }
    }

    private void TakeDamage(Entity entity, ref Health health, TakeDamageComponent takeDamageComponent)
    {
        health.Decrease(takeDamageComponent.Damage);
        entity.RemoveComponent<TakeDamageComponent>();
    }
}
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ReloadSystem))]
public sealed class ReloadSystem : UpdateSystem
{
    private Filter _weaponsFilter;
    private Stash<Reload> _weaponStash;

    public override void OnAwake()
    {
        _weaponsFilter = World.Filter.With<Reload>();
        _weaponStash = World.GetStash<Reload>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _weaponsFilter)
        {
            ref Reload reload = ref _weaponStash.Get(entity);
            Reloading(entity, ref reload, deltaTime);
        }
    }

    private void Reloading(Entity entity, ref Reload reload, float deltaTime)
    {
        reload.Tick(deltaTime);

        if (reload.Time <= 0)
        {
            entity.RemoveComponent<Reload>();
        }
    }
}
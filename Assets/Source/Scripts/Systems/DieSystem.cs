using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DieSystem))]
public sealed class DieSystem : UpdateSystem
{
    private Filter _filter;
    private Stash<DieEvent> _dyingStash;
    public override void OnAwake()
    {
        _filter = World.Filter.With<DieEvent>();
        _dyingStash = World.GetStash<DieEvent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            GameObject destroingObject = _dyingStash.Get(entity).Transform.gameObject;

            Entity scoreEntity = World.CreateEntity();
            scoreEntity.AddComponent<ScoreEvent>().Value = 1;

            World.RemoveEntity(entity);
            Destroy(destroingObject);
        }
    }
}
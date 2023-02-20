using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ScoreSystem))]
public sealed class ScoreSystem : UpdateSystem
{
    private Filter _scoreEventFilter;
    private Stash<ScoreEvent> _scoreEventStash;

    private Stash<Score> _scoreStash;
    private Entity _entity;

    public override void OnAwake()
    {
        _scoreEventFilter = World.Filter.With<ScoreEvent>();
        _scoreEventStash = World.GetStash<ScoreEvent>();

        Filter scoreFilter = World.Filter.With<Score>();
        _scoreStash = World.GetStash<Score>();
        _entity = scoreFilter.First();
    }

    public override void OnUpdate(float deltaTime)
    {
        ref Score score = ref _scoreStash.Get(_entity);

        foreach (Entity entity in _scoreEventFilter)
        {
            int value = _scoreEventStash.Get(entity).Value;
            score.Increase(value);
            World.RemoveEntity(entity);
        }
    }
}
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ScoreViewSystem))]
public sealed class ScoreViewSystem : UpdateSystem
{
    private Stash<ScoreView> _scoreViewStash;
    private Stash<Score> _scoreStash;

    private Entity _entity;
    private Entity _view;

    public override void OnAwake()
    {
        Filter scoreEventFilter = World.Filter.With<ScoreView>();
        _view = scoreEventFilter.First();
        _scoreViewStash = World.GetStash<ScoreView>();

        Filter scoreFilter = World.Filter.With<Score>();
        _entity = scoreFilter.First();
        _scoreStash = World.GetStash<Score>();
    }

    public override void OnUpdate(float deltaTime)
    {
        int value = _scoreStash.Get(_entity).Value;
        _scoreViewStash.Get(_view).Update(value);
    }
}
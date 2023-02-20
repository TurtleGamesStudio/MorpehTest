using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputConverter))]
public sealed class InputConverter : UpdateSystem
{
    private Filter _filter;
    private Entity _entity;

    private Stash<Vector2Component> _vector2Components;
    private Stash<MovementParametrs> _movementParametrs;

    public override void OnAwake()
    {
        _filter = World.Filter.With<Player>().With<MovementParametrs>().With<Vector2Component>();
        _entity = _filter.First();

        _vector2Components = World.GetStash<Vector2Component>();
        _movementParametrs = World.GetStash<MovementParametrs>();
    }

    public override void OnUpdate(float deltaTime)
    {
        Vector2 horizontalDirection = _entity.GetComponent<Vector2Component>().Vector2;
        ref Vector3 worldDirection = ref _entity.GetComponent<MovementParametrs>().Direction;

        worldDirection = RotateTo90degres(horizontalDirection);
    }

    private Vector3 RotateTo90degres(Vector3 vector)
    {
        return new Vector3(vector.x, vector.z, vector.y);
    }
}
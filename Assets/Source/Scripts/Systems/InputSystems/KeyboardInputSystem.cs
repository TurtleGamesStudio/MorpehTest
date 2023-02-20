using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(KeyboardInputSystem))]
public sealed class KeyboardInputSystem : UpdateSystem
{
    private Filter _filter;
    private Entity _entity;

    private Stash<Vector2Component> _vector2Componets;
    private Vector2Component _direction;

    public Vector2 Direction { get; private set; } = Vector2.zero;

    public override void OnAwake()
    {
        _filter = World.Filter.With<Player>().With<Vector2Component>();
        _vector2Componets = World.GetStash<Vector2Component>();
        _entity = _filter.First();
    }

    public override void OnUpdate(float deltaTime)
    {
        ref Vector2 direction = ref _vector2Componets.Get(_entity).Vector2;

        ChangeDirection(ref direction);
    }

    public void ChangeDirection(ref Vector2 direction)
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction -= Vector2.up;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction -= Vector2.right;
        }

        direction.Normalize();
    }
}
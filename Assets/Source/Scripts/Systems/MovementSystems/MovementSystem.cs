using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : UpdateSystem
{
    private Entity _player;
    private Filter _playerFilter;
    private Stash<Rigidbodyable> _rigidbodyables;
    private Stash<MovementParametrs> _movementParametrs;
    private Transform _transform;

    public override void OnAwake()
    {
        _playerFilter = World.Filter.With<Player>().With<Rigidbodyable>().With<MovementParametrs>();
        _player = _playerFilter.First();

        _rigidbodyables = World.GetStash<Rigidbodyable>();
        _movementParametrs = World.GetStash<MovementParametrs>();

        Stash<Player> playerStash = World.GetStash<Player>();
        _transform = playerStash.Get(_player).Transform;
    }

    public override void OnUpdate(float deltaTime)
    {
        Rigidbody rigidbody = _player.GetComponent<Rigidbodyable>().Rigidbody;
        Vector3 direction = _transform.rotation * _player.GetComponent<MovementParametrs>().Direction;
        float speed = _player.GetComponent<MovementParametrs>().MaxSpeed;

        Move(rigidbody, direction, speed, deltaTime);
    }

    private void Move(Rigidbody rigidbody, Vector3 direction, float speed, float deltaTime)
    {
        rigidbody.velocity = speed * direction * deltaTime;// Time.deltaTime;
    }
}
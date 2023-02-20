using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine.UIElements;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HorizontalRotationSystem))]
public sealed class HorizontalRotationSystem : UpdateSystem
{
    [SerializeField] private float _sensetivity;

    private Filter _filter;
    private Stash<Player> _transformableStash;
    private Entity _entity;
    private Transform _transform;

    public override void OnAwake()
    {
        _filter = World.Filter.With<Player>();
        _entity = _filter.First();
        _transformableStash = World.GetStash<Player>();
        _transform = _transformableStash.Get(_entity).Transform;
    }

    public override void OnUpdate(float deltaTime)
    {
        float deltaX = Input.GetAxis("Mouse X");
        RotateHorizontal(_transform, deltaX);
    }

    private void RotateHorizontal(Transform transform, float value)
    {
        transform.localEulerAngles += new Vector3(0f, value * _sensetivity, 0f);
    }
}
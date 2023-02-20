using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine.UIElements;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(VerticalRotationSystem))]
public sealed class VerticalRotationSystem : UpdateSystem
{
    [SerializeField] private float _sensetivity;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    private float _eulerX;
    private Filter _filter;
    private Stash<VerticalRotatable> _transformableStash;
    private Entity _entity;
    private Transform _transform;

    public override void OnAwake()
    {
        _filter = World.Filter.With<VerticalRotatable>();
        _entity = _filter.First();
        _transformableStash = World.GetStash<VerticalRotatable>();
        _transform = _transformableStash.Get(_entity).Transform;
        _eulerX = 0;
    }

    public override void OnUpdate(float deltaTime)
    {
        float deltaY = Input.GetAxis("Mouse Y");
        RotateVertical(_transform, deltaY);
    }

    private void RotateVertical(Transform transform, float value)
    {
        _eulerX += value * _sensetivity;
        _eulerX = Mathf.Clamp(_eulerX, _minAngle, _maxAngle);
        transform.localEulerAngles = Vector3.left * _eulerX;
    }
}
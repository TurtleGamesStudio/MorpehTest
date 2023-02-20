using UnityEngine;
using Scellecs.Morpeh;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private PlayerProvider _player;

    [Header("Health")]
    [SerializeField] private HealthProvider _healthProvider;
    [SerializeField] private int _healthPoints;

    [Header("Movement")]
    [SerializeField] private MovementParametrsProvider _movementParametrsProvider;
    [SerializeField] private float _maxSpeed;

    [Header("Rigidbody")]
    [SerializeField] private RigidbodyableProvider _rigidbodyableProvider;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("InputedData")]
    [SerializeField] private Vector2Provider _inputPsitionProvider;

    [Header("Camera")]
    [SerializeField] private VerticalRotatableProvider _verticalRotatableProvider;
    [SerializeField] private Transform _camera;

    World world
    { get; set; }

    private void Start()
    {
        ref Health health = ref _healthProvider.GetData();
        health.Value = 1;

        _movementParametrsProvider.GetData().MaxSpeed = _maxSpeed;
        _rigidbodyableProvider.GetData().Rigidbody = _rigidbody;
        _player.GetData().Transform = transform;
        _verticalRotatableProvider.GetData().Transform = _camera;
    }
}

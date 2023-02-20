using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Collections.Generic;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SpawnSystem))]
public sealed class SpawnSystem : UpdateSystem
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _spawnInterval;

    private Filter _filter;
    private Stash<SpawnPoints> _stash;
    private Entity _entity;
    private float _time;
    private List<Transform> _availablePoints;

    public override void OnAwake()
    {
        _filter = World.Filter.With<SpawnPoints>();
        _stash = World.GetStash<SpawnPoints>();
        _entity = _filter.First();

        ref SpawnPoints spawnPoints = ref _stash.Get(_entity);
        _availablePoints = new List<Transform>(spawnPoints.Transforms.Length);
    }

    public override void OnUpdate(float deltaTime)
    {
        _time += deltaTime;

        if (_time >= _spawnInterval)
        {
            TrySpawn();
            _time -= _spawnInterval;
        }
    }

    private void TrySpawn()
    {
        ref SpawnPoints spawnPoints = ref _stash.Get(_entity);
        foreach (Transform transform in spawnPoints.Transforms)
        {
            if (Physics.CheckSphere(transform.position, _radius, _layerMask) == false)
            {
                _availablePoints.Add(transform);
            }
        }

        if (_availablePoints.Count != 0)
        {
            int index = Random.Range(0, _availablePoints.Count);
            Transform spawnPoint = _availablePoints[index];

            Instantiate(_template, spawnPoint.position, spawnPoint.rotation);
            _availablePoints.Clear();
        }
    }
}
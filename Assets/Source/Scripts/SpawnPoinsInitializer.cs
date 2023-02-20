using UnityEngine;

public class SpawnPoinsInitializer : MonoBehaviour
{
    [SerializeField] private SpawnPointsProvider _spawnPointsProvider;
    [SerializeField] private Transform[] _transforms;

    private void Start()
    {
        _spawnPointsProvider.GetData().Transforms = _transforms;
    }
}

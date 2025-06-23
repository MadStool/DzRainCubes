using UnityEngine;

public class CubeSimulation : MonoBehaviour
{
    [SerializeField] private ObjectPool _cubePool;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start() => _cubeSpawner.Initialize(_cubePool);

    private void OnValidate()
    {
        if (_cubePool == null) 
            _cubePool = GetComponentInChildren<ObjectPool>();

        if (_cubeSpawner == null) 
            _cubeSpawner = GetComponentInChildren<CubeSpawner>();
    }
}
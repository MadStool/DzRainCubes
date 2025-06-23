using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10f, 0f, 10f);
    [SerializeField] private float _spawnHeight = 10f;
    [SerializeField] private Color _initialCubeColor = Color.white;

    private ObjectPool _cubePool;
    private float _nextSpawnTime;

    public void Initialize(ObjectPool pool) => _cubePool = pool;

    private void Update()
    {
        if (_cubePool == null || Time.time < _nextSpawnTime)
            return;

        SpawnCube();
        _nextSpawnTime = Time.time + 1f / _spawnRate;
    }

    private void SpawnCube()
    {
        GameObject cube = _cubePool.GetFromPool();
        CubeInteraction interaction = cube.GetComponent<CubeInteraction>();

        interaction.Initialize(_cubePool);

        cube.transform.position = GetRandomPosition();
        cube.GetComponent<Renderer>().material.color = _initialCubeColor;
        ResetPhysics(cube);
    }

    private Vector3 GetRandomPosition() => new Vector3(
        Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2),
        _spawnHeight,
        Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2)
    );

    private void ResetPhysics(GameObject cube)
    {
        Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

        if (rigidbody == null) 
            return;

        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(0, _spawnHeight, 0),
            new Vector3(_spawnAreaSize.x, 0.1f, _spawnAreaSize.z)
        );
    }
}
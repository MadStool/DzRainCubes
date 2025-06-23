using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10f, 0f, 10f);
    [SerializeField] private float _spawnHeight = 10f;
    [SerializeField] private Color _initialCubeColor = Color.white;
    [SerializeField] private ObjectPool _cubePool;

    private Coroutine _spawningCoroutine;
    private bool _isSpawningActive;

    private void Start()
    {
        ValidateDependencies();
        StartSpawning();
    }

    private void OnValidate()
    {
        ValidateDependencies();
    }

    private void ValidateDependencies()
    {
        if (_cubePool == null)
            _cubePool = GetComponentInChildren<ObjectPool>(true);
    }

    public void StartSpawning()
    {
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);

        _isSpawningActive = true;
        _spawningCoroutine = StartCoroutine(SpawnCubesRoutine());
    }

    public void StopSpawning()
    {
        _isSpawningActive = false;

        if (_spawningCoroutine != null)
        {
            StopCoroutine(_spawningCoroutine);
            _spawningCoroutine = null;
        }
    }

    private IEnumerator SpawnCubesRoutine()
    {
        while (_isSpawningActive)
        {
            if (_cubePool != null)
                SpawnCube();

            yield return new WaitForSeconds(1f / _spawnRate);
        }
    }

    private void SpawnCube()
    {
        CubeInteraction cube = _cubePool.GetFromPool();
        cube.Initialize(_initialCubeColor, GetRandomPosition());
        cube.OnReturnToPoolRequested += ReturnCubeToPool;
    }

    private void ReturnCubeToPool(CubeInteraction cube)
    {
        cube.OnReturnToPoolRequested -= ReturnCubeToPool;
        _cubePool.ReturnToPool(cube);
    }

    private Vector3 GetRandomPosition() => new Vector3(
        Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2),
        _spawnHeight,
        Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2)
    );

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(0, _spawnHeight, 0),
            new Vector3(_spawnAreaSize.x, 0.1f, _spawnAreaSize.z)
        );
    }

    private void OnDestroy()
    {
        StopSpawning();
    }
}
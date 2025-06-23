using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    private const float MIN_SPAWN_RATE = 0.1f;
    private const float MAX_SPAWN_RATE = 10f;
    private const float MIN_SPAWN_HEIGHT = 5f;
    private const float MAX_SPAWN_HEIGHT = 20f;

    [SerializeField, Range(MIN_SPAWN_RATE, MAX_SPAWN_RATE)]
    private float _spawnRate = 1f;

    [SerializeField] private Vector3 _spawnArea = new Vector3(10f, 0f, 10f);

    [SerializeField, Range(MIN_SPAWN_HEIGHT, MAX_SPAWN_HEIGHT)]
    private float _spawnHeight = 10f;

    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private ObjectPool _pool;

    private Coroutine _spawnRoutine;
    private bool _isActive;

    private void Start()
    {
        ValidateReferences();
        BeginSpawning();
    }

    private void OnValidate()
    {
        ValidateReferences();
    }

    private void ValidateReferences()
    {
        if (_pool == null)
            _pool = GetComponentInChildren<ObjectPool>(true);
    }

    public void BeginSpawning()
    {
        if (_isActive)
            return;

        _isActive = true;
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (!_isActive)
            return;

        _isActive = false;

        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        var waitTime = new WaitForSeconds(1f / _spawnRate);

        while (_isActive)
        {
            Spawn();
            yield return waitTime;
        }
    }

    private void Spawn()
    {
        if (_pool == null)
            return;

        var cube = _pool.GetCube();
        cube.Initialize(_defaultColor, GetRandomPosition());
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-_spawnArea.x / 2f, _spawnArea.x / 2f),
            _spawnHeight,
            Random.Range(-_spawnArea.z / 2f, _spawnArea.z / 2f)
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(0, _spawnHeight, 0),
            new Vector3(_spawnArea.x, 0.1f, _spawnArea.z)
        );
    }

    private void OnDestroy()
    {
        StopSpawning();
    }
}
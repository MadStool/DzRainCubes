using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private Vector3 _spawnArea = new Vector3(10f, 0f, 10f);
    [SerializeField] private float _spawnHeight = 10f;
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
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);

        _isActive = true;
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        _isActive = false;

        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isActive)
        {
            if (_pool != null)
                Spawn();

            yield return new WaitForSeconds(1f / _spawnRate);
        }
    }

    private void Spawn()
    {
        CubeInteraction cube = _pool.GetCube();
        cube.Initialize(_defaultColor, GetRandomPosition());
        cube.ReturnRequested += HandleCubeReturn;
    }

    private void HandleCubeReturn(CubeInteraction cube)
    {
        cube.ReturnRequested -= HandleCubeReturn;
        _pool.ReturnCube(cube);
    }

    private Vector3 GetRandomPosition() => new Vector3(
        Random.Range(-_spawnArea.x / 2, _spawnArea.x / 2),
        _spawnHeight,
        Random.Range(-_spawnArea.z / 2, _spawnArea.z / 2)
    );

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
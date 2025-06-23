using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private CubeInteraction _cubePrefab;
    [SerializeField] private int _initialPoolSize = 20;

    private Queue<CubeInteraction> _pooledObjects = new Queue<CubeInteraction>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNewPooledObject();
        }
    }

    private CubeInteraction CreateNewPooledObject()
    {
        CubeInteraction cube = Instantiate(_cubePrefab);
        cube.gameObject.SetActive(false);
        _pooledObjects.Enqueue(cube);
        return cube;
    }

    public CubeInteraction GetFromPool()
    {
        if (_pooledObjects.Count == 0)
            CreateNewPooledObject();

        CubeInteraction cube = _pooledObjects.Dequeue();
        cube.gameObject.SetActive(true);
        return cube;
    }

    public void ReturnToPool(CubeInteraction cube)
    {
        cube.gameObject.SetActive(false);
        _pooledObjects.Enqueue(cube);
    }
}
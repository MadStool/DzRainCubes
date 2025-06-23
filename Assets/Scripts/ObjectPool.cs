using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _initialPoolSize = 20;

    private Queue<GameObject> _pooledObjects = new Queue<GameObject>();

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

    private GameObject CreateNewPooledObject()
    {
        GameObject cube = Instantiate(_cubePrefab);
        cube.SetActive(false);
        _pooledObjects.Enqueue(cube);
        return cube;
    }

    public GameObject GetFromPool()
    {
        if (_pooledObjects.Count == 0)
        {
            CreateNewPooledObject();
        }

        GameObject cube = _pooledObjects.Dequeue();
        cube.SetActive(true);
        return cube;
    }

    public void ReturnToPool(GameObject cube)
    {
        cube.SetActive(false);
        _pooledObjects.Enqueue(cube);
    }
}
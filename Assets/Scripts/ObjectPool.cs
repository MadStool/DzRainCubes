using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private CubeInteraction _cubePrefab;
    [SerializeField] private int _initialPoolSize = 20;

    private Queue<CubeInteraction> _cubes = new Queue<CubeInteraction>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateCube();
        }
    }

    private CubeInteraction CreateCube()
    {
        CubeInteraction cube = Instantiate(_cubePrefab);
        cube.gameObject.SetActive(false);
        _cubes.Enqueue(cube);

        return cube;
    }

    public CubeInteraction GetCube()
    {
        if (_cubes.Count == 0)
            CreateCube();

        CubeInteraction cube = _cubes.Dequeue();
        cube.gameObject.SetActive(true);
        return cube;
    }

    public void ReturnCube(CubeInteraction cube)
    {
        cube.gameObject.SetActive(false);
        cube.transform.rotation = Quaternion.identity;
        _cubes.Enqueue(cube);
    }
}
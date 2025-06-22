using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private ExplodableCube _cubePrefab;

    private ColorChanger _colorChanger;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    public ExplodableCube[] SpawnChildCubes(Vector3 center, Vector3 parentScale, float parentSplitChance, Color parentColor)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        ExplodableCube[] newCubes = new ExplodableCube[count];

        for (int i = 0; i < count; i++)
        {
            newCubes[i] = Instantiate(_cubePrefab, center, Random.rotation);
            newCubes[i].transform.localScale = parentScale * 0.5f;
            newCubes[i].Initialize(parentSplitChance * 0.5f, _colorChanger.GetVariedColor(parentColor));
        }

        return newCubes;
    }
}
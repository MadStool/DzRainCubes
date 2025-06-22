using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private Exploder _exploder;

    public void HandleCubeClick(ExplodableCube cube)
    {
        if (cube == null) 
            return;

        Vector3 explosionCenter = cube.transform.position;

        if (cube.ShouldSplit())
        {
            ExplodableCube[] newCubes = _spawner.SpawnChildCubes(
                explosionCenter,
                cube.transform.localScale,
                cube.SplitChance,
                cube.CubeColor
            );
            _exploder.ApplyExplosion(newCubes, explosionCenter, true);
        }
        else
        {
            _exploder.ApplyExplosionToNearby(explosionCenter, cube);
        }

        Destroy(cube.gameObject);
    }
}
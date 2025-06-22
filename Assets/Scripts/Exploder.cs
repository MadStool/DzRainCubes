using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _defaultForce = 20f;
    [SerializeField] private float _defaultRadius = 10f;
    [SerializeField] private float _upwardsModifier = 0.5f;

    public void ApplyExplosion(ExplodableCube[] cubes, Vector3 center, bool useCubePhysics = false)
    {
        if (cubes == null) return;

        foreach (var cube in cubes)
        {
            if (cube == null) continue;
            ApplySingleExplosion(cube, center, useCubePhysics);
        }
    }

    public void ApplyExplosionToNearby(Vector3 center, ExplodableCube sourceCube = null)
    {
        Collider[] colliders = Physics.OverlapSphere(center, _defaultRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out ExplodableCube cube) && cube != sourceCube)
            {
                ApplySingleExplosion(cube, center);
            }
        }
    }

    private void ApplySingleExplosion(ExplodableCube cube, Vector3 center, bool useCubePhysics = false)
    {
        float force = useCubePhysics ? cube.ExplosionForce : _defaultForce;
        float radius = useCubePhysics ? cube.ExplosionRadius : _defaultRadius;
        float distance = Vector3.Distance(center, cube.transform.position);

        cube.CubeRigidbody.AddExplosionForce(
            force * (1 - distance / radius),
            center,
            radius,
            _upwardsModifier,
            ForceMode.Impulse
        );
    }
}
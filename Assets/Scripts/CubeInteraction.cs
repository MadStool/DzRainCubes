using UnityEngine;

[RequireComponent(typeof(CubePhysics))]
[RequireComponent(typeof(CubeVisuals))]
[RequireComponent(typeof(CubeLifetime))]
public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private Color _collisionColor = Color.red;

    private CubePhysics _physics;
    private CubeVisuals _visuals;
    private CubeLifetime _lifetime;
    private bool _hasCollided;

    private void Awake()
    {
        _physics = GetComponent<CubePhysics>();
        _visuals = GetComponent<CubeVisuals>();
        _lifetime = GetComponent<CubeLifetime>();

        _lifetime.LifetimeEnded += OnLifetimeEnded;
    }

    private void OnDestroy()
    {
        _lifetime.LifetimeEnded -= OnLifetimeEnded;
    }

    public void Initialize(Color color, Vector3 position)
    {
        _visuals.Color = color;
        transform.SetPositionAndRotation(position, Quaternion.identity);
        _physics.ResetPhysics();
        _hasCollided = false;
    }

    private void OnEnable()
    {
        _hasCollided = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided || collision.collider.TryGetComponent<Platform>(out Platform platform) == false)
            return;

        _hasCollided = true;
        _visuals.Color = _collisionColor;
        _lifetime.StartLifetimeCountdown();
    }

    private void OnLifetimeEnded(CubeLifetime cubeLifetime)
    {
        gameObject.SetActive(false);
    }
}
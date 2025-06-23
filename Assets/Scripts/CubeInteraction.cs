using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;
    [SerializeField] private Color _collisionColor = Color.red;

    private Renderer _cubeRenderer;
    private bool _hasCollided = false;
    private Coroutine _returnCoroutine;

    public event System.Action<CubeInteraction> OnReturnToPoolRequested;

    public Color Color
    {
        get => _cubeRenderer.material.color;
        set => _cubeRenderer.material.color = value;
    }

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
    }

    public void Initialize(Color color, Vector3 position)
    {
        Color = color;
        transform.position = position;
        ResetPhysics();
    }

    private void OnEnable()
    {
        _hasCollided = false;

        if (_returnCoroutine != null)
        {
            StopCoroutine(_returnCoroutine);
            _returnCoroutine = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided)
            return;

        bool hasPlatform = collision.collider.TryGetComponent<Platform>(out Platform platform);

        if (hasPlatform == false)
            return;

        _hasCollided = true;
        Color = _collisionColor;
        _returnCoroutine = StartCoroutine(RequestReturnToPoolAfterDelay(GetRandomLifetime()));
    }

    private float GetRandomLifetime() => Random.Range(_minLifetime, _maxLifetime);

    private IEnumerator RequestReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnReturnToPoolRequested?.Invoke(this);
    }

    private void ResetPhysics()
    {
        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
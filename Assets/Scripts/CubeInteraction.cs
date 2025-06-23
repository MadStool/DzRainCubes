using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;
    [SerializeField] private Color _collisionColor = Color.red;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _hasCollided;
    private Coroutine _lifetimeCoroutine;

    public event System.Action<CubeInteraction> ReturnRequested;

    public Color Color
    {
        get => _renderer.material.color;
        set => _renderer.material.color = value;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Color color, Vector3 position)
    {
        Color = color;
        transform.position = position;
        transform.rotation = Quaternion.identity;
        ResetPhysics();
    }

    private void OnEnable()
    {
        _hasCollided = false;
        ResetCoroutine();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided)
            return;

        if (collision.collider.TryGetComponent<Platform>(out Platform platform) == false)
            return;

        _hasCollided = true;
        Color = _collisionColor;
        _lifetimeCoroutine = StartCoroutine(ReturnAfterDelay(GetRandomLifetime()));
    }

    private float GetRandomLifetime() => Random.Range(_minLifetime, _maxLifetime);

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnRequested?.Invoke(this);
    }

    private void ResetPhysics()
    {
        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void ResetCoroutine()
    {
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }
    }
}
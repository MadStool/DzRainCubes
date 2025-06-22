using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class ExplodableCube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;
    [SerializeField] private float _explosionRadiusMultiplier = 2f;
    [SerializeField] private float _explosionForceMultiplier = 3f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Color _cubeColor;

    public float SplitChance => _splitChance;
    public Color CubeColor => _cubeColor;
    public Rigidbody CubeRigidbody => _rigidbody;
    public float ExplosionRadius => transform.localScale.x * _explosionRadiusMultiplier;
    public float ExplosionForce => _explosionForceMultiplier / transform.localScale.x;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(float chance, Color color)
    {
        _splitChance = chance;
        _cubeColor = color;
        _renderer.material.color = color;
    }

    public bool ShouldSplit() => Random.value <= _splitChance;
}
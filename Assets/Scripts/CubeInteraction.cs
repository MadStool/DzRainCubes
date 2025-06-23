using UnityEngine;
using System.Collections;

public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private string _platformTag = "Platform";
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;
    [SerializeField] private Color _collisionColor = Color.red;

    private Renderer _cubeRenderer;
    private ObjectPool _objectPool;
    private bool _hasCollided = false;

    public void Initialize(ObjectPool pool)
    {
        _objectPool = pool;
        _cubeRenderer = GetComponent<Renderer>();
    }

    private void OnEnable() => _hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided || !collision.gameObject.CompareTag(_platformTag))
            return;

        _hasCollided = true;
        _cubeRenderer.material.color = _collisionColor;
        StartCoroutine(ReturnToPoolAfterDelay(GetRandomLifetime()));
    }

    private float GetRandomLifetime() => Random.Range(_minLifetime, _maxLifetime + 1);

    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _objectPool.ReturnToPool(gameObject);
    }
}
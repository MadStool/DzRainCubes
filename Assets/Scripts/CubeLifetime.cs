using UnityEngine;
using System.Collections;

public class CubeLifetime : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private Coroutine _lifetimeCoroutine;

    public event System.Action<CubeLifetime> LifetimeEnded;

    public void StartLifetimeCountdown()
    {
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
        }

        _lifetimeCoroutine = StartCoroutine(LifetimeCoroutine());
    }

    private IEnumerator LifetimeCoroutine()
    {
        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        yield return new WaitForSeconds(lifetime);
        LifetimeEnded?.Invoke(this);
    }
}
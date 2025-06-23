using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeVisuals : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public Color Color
    {
        get => _renderer.material.color;
        set => _renderer.material.color = value;
    }
}
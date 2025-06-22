using UnityEngine;
using Random = UnityEngine.Random;

public class ColorChanger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color[] _baseColors;
    [SerializeField] private float _colorVariation = 10f;

    public Color GetRandomColor()
    {
        return _baseColors.Length > 0 ?
            _baseColors[Random.Range(0, _baseColors.Length)] : Random.ColorHSV();
    }

    public Color GetVariedColor(Color baseColor)
    {
        return new Color(
            Mathf.Clamp01(baseColor.r + Random.Range(-_colorVariation, _colorVariation)),
            Mathf.Clamp01(baseColor.g + Random.Range(-_colorVariation, _colorVariation)),
            Mathf.Clamp01(baseColor.b + Random.Range(-_colorVariation, _colorVariation)),
            1f
        );
    }
}
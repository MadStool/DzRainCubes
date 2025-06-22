using UnityEngine;
using Random = UnityEngine.Random;

public class CubeVisuals : MonoBehaviour
{
    [Header("color/intensity settings")]
    [SerializeField] private Color[] _availableColors;
    [SerializeField] private float _colorVariation = 10f;

    public Color GetRandomColor()
    {
        Color[] simpleColors = new Color[]
         {
             Color.red,
             Color.green,
             Color.blue,
             Color.yellow,
             Color.magenta,
             Color.cyan
          };

        return simpleColors[Random.Range(0, simpleColors.Length)];
    }

    public Color GetVariedColor(Color baseColor)
    {
        return new Color(
            GetVariedChannel(baseColor.r),
            GetVariedChannel(baseColor.g),
            GetVariedChannel(baseColor.b),
            1f
        );
    }

    private float GetVariedChannel(float channel)
    {
        return Mathf.Clamp01(channel + Random.Range(-_colorVariation, _colorVariation));
    }
}
using UnityEngine;

public static class AltitudeColorGradient
{
    private static readonly Color[] altitudeColors = new Color[]
    {
        new Color32(255,  60,   0, 255),   // 0 ft
        new Color32(255, 140,   0, 255),   // 1k
        new Color32(255, 255,   0, 255),   // 4k
        new Color32(128, 255,   0, 255),   // 8k
        new Color32(  0, 255, 128, 255),   // 15k
        new Color32(  0, 180, 255, 255),   // 25k
        new Color32(  0,  80, 255, 255),   // 30k
        new Color32(190,   0, 255, 255),   // 40k+
    };

    private static readonly float[] altitudeStops = new float[]
    {
        0f,
        1000f,
        4000f,
        8000f,
        15000f,
        25000f,
        30000f,
        40000f
    };

    public static Color Evaluate(float altitudeFt)
    {
        altitudeFt = Mathf.Clamp(altitudeFt, altitudeStops[0], altitudeStops[altitudeStops.Length - 1]);

        for (int i = 0; i < altitudeStops.Length - 1; i++)
        {
            if (altitudeFt >= altitudeStops[i] && altitudeFt <= altitudeStops[i + 1])
            {
                float t = Mathf.InverseLerp(altitudeStops[i], altitudeStops[i + 1], altitudeFt);
                return Color.Lerp(altitudeColors[i], altitudeColors[i + 1], t);
            }
        }

        return altitudeColors[altitudeColors.Length - 1];
    }
}

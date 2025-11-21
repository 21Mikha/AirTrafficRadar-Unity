using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private SpriteRenderer sr;

    [HideInInspector] public FlightState data;
    [HideInInspector] public Vector2 worldPos;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Initialize(FlightState fs, Vector2 pos)
    {
        data = fs;
        worldPos = pos;

        transform.position = worldPos;
        UpdateRotation();
        UpdateColor();
    }

    public void UpdatePlane(FlightState fs, Vector2 pos)
    {
        data = fs;
        worldPos = pos;

        transform.position = worldPos;  // instant (we add smoothing later)
        UpdateRotation();
        UpdateColor();
    }

    void UpdateRotation()
    {
        if (data.heading.HasValue)
            transform.rotation = Quaternion.Euler(0f, 0f, -data.heading.Value);
    }

    void UpdateColor()
    {
        float altitudeMeters =
            data.baroAltitude ??
            data.geoAltitude ??
            0f;  // fallback if both null

        float altitudeFt = altitudeMeters * 3.28084f;

        sr.color = AltitudeColorGradient.Evaluate(altitudeFt);
    }


}

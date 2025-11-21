using UnityEngine;

public class MapCoordinateConverter : MonoBehaviour
{
    public Rect mapRect;  // auto-filled at runtime

    [Header("Latitude range of this Mercator map (degrees)")]
    public float maxLatitude = 80f;
    public float minLatitude = -80f;

    void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        var b = sr.bounds;

        mapRect = new Rect(b.min.x, b.min.y, b.size.x, b.size.y);

        float aspect = mapRect.height / mapRect.width;   // H/W
        float M = Mathf.PI * aspect;
        maxLatitude = Mathf.Rad2Deg * (2f * Mathf.Atan(Mathf.Exp(M)) - Mathf.PI / 2f);
        minLatitude = -maxLatitude;

        Debug.Log($"Detected Mercator lat range: {minLatitude:F2} .. {maxLatitude:F2}");



    }





    public Vector2 LatLonToWorld(float lat, float lon)
    {
        // Clamp latitude to what the texture actually contains
        lat = Mathf.Clamp(lat, minLatitude, maxLatitude);

        // --- X: longitude → [0,1] ---
        float x01 = (lon + 180f) / 360f;

        // --- Y: Mercator latitude → [0,1] ---
        float latRad = lat * Mathf.Deg2Rad;
        float maxLatRad = maxLatitude * Mathf.Deg2Rad;
        float minLatRad = minLatitude * Mathf.Deg2Rad;

        float mercLat = Mathf.Log(Mathf.Tan(Mathf.PI / 4f + latRad / 2f));
        float mercLatMax = Mathf.Log(Mathf.Tan(Mathf.PI / 4f + maxLatRad / 2f));
        float mercLatMin = Mathf.Log(Mathf.Tan(Mathf.PI / 4f + minLatRad / 2f));

        // Map Mercator Y linearly into [0,1]
        float y01 = Mathf.InverseLerp(mercLatMin, mercLatMax, mercLat);

        // --- Normalize to world space ---
        float worldX = mapRect.xMin + x01 * mapRect.width;
        float worldY = mapRect.yMin + y01 * mapRect.height;

        return new Vector2(worldX, worldY);
    }
}

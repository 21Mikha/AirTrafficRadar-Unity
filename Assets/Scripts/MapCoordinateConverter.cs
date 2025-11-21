using UnityEngine;

public class MapCoordinateConverter : MonoBehaviour
{
    [Header("Map Bounds (World Space)")]
    public Rect mapRect; // x,y = bottom-left position; width,height = size in world units

    // Convert latitude/longitude to world position
    public Vector2 LatLonToWorld(float lat, float lon)
    {
        // 1. Normalize
        float x01 = (lon + 180f) / 360f;     // 0..1
        float y01 = (90f - lat) / 180f;      // 0..1

        // 2. Scale to world rect
        float worldX = mapRect.xMin + x01 * mapRect.width;
        float worldY = mapRect.yMin + y01 * mapRect.height;

        return new Vector2(worldX, worldY);
    }


    private void Start()
    {
        Debug.Log("Center: " + LatLonToWorld(0, 0));
        Debug.Log("Top: " + LatLonToWorld(90, 0));
        Debug.Log("Bottom: " + LatLonToWorld(-90, 0));
        Debug.Log("Right: " + LatLonToWorld(0, 180));
        Debug.Log("Left: " + LatLonToWorld(0, -180));
    }
}

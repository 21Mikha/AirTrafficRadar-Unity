using UnityEngine;
using System.Collections.Generic;

public class PlaneManager : MonoBehaviour
{
    public PlaneRenderer renderer;
    public MapCoordinateConverter map;
    public float planeScale = 0.25f;

    public List<PlaneRenderData> renderList = new List<PlaneRenderData>();

    public void UpdateRenderData(Dictionary<string, FlightState> flights)
    {
        renderList.Clear();

        foreach (var kv in flights)
        {
            var fs = kv.Value;
            if (!fs.latitude.HasValue || !fs.longitude.HasValue)
                continue;

            Vector2 pos = map.LatLonToWorld(fs.latitude.Value, fs.longitude.Value);

            float rot = fs.heading ?? 0f;

            float meters = fs.baroAltitude ?? fs.geoAltitude ?? 0f;
            float ft = meters * 3.28084f;
            Color col = AltitudeColorGradient.Evaluate(ft);

            renderList.Add(new PlaneRenderData
            {
                position = pos,
                rotation = -rot,
                scale = planeScale,
                color = col,
                stateRef = fs
            });
        }
    }

    void LateUpdate()
    {
        renderer.Render(renderList);
    }
}

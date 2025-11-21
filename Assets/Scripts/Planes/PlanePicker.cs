using UnityEngine;

public class PlanePicker : MonoBehaviour
{
    public PlaneManager planeManager;
    public float pickRadius = 0.2f;

    public PlaneRenderData TryPick()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PlaneRenderData best = null;
        float bestDist = float.MaxValue;

        foreach (var p in planeManager.renderList)
        {
            float d = Vector2.Distance(mouse, p.position);
            if (d < pickRadius && d < bestDist)
            {
                bestDist = d;
                best = p;
            }
        }

        return best;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = TryPick();
            if (hit != null)
            {
                Debug.Log("Clicked: " + hit.stateRef.callsign);
                // call your UI update
            }
        }
    }
}

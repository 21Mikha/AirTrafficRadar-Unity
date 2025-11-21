using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlanePicker picker;
    public FlightInfoPanel infoPanel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = picker.TryPick();
            if (hit != null)
            {
                infoPanel.Show(hit.stateRef);
            }
            else
            {
                infoPanel.Hide();
            }
        }
    }
}

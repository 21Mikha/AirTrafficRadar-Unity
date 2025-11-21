using UnityEngine;
using TMPro;
using System.Collections;

public class FlightInfoPanel : MonoBehaviour
{
    [Header("UI Fields")]
    public TextMeshProUGUI callsignText;
    public TextMeshProUGUI icaoText;
    public TextMeshProUGUI countryText;
    public TextMeshProUGUI altitudeText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI headingText;
    public TextMeshProUGUI vertRateText;
    public TextMeshProUGUI latText;
    public TextMeshProUGUI lonText;
    public TextMeshProUGUI lastContactText;
    public TextMeshProUGUI statusText;

    private RectTransform rect;
    private CanvasGroup cg;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        cg = gameObject.AddComponent<CanvasGroup>();
    }

    public void UpdateFields(FlightState fs)
    {
        gameObject.SetActive(true);

        callsignText.text = "Callsign: " + fs.callsign;
        icaoText.text = "ICAO24: " + fs.icao24;
        countryText.text = "Origin: " + fs.originCountry;

        // Convert altitude
        float altMeters = fs.baroAltitude ?? fs.geoAltitude ?? 0f;
        float altFeet = altMeters * 3.28084f;

        altitudeText.text = $"Altitude: {altFeet:0} ft";
        speedText.text = $"Speed: {fs.velocity ?? 0:0} m/s";
        headingText.text = $"Heading: {fs.heading ?? 0:0}°";
        vertRateText.text = $"Vertical Rate: {fs.verticalRate ?? 0:0} m/s";

        latText.text = $"Lat: {fs.latitude:0.0000}";
        lonText.text = $"Lon: {fs.longitude:0.0000}";

        lastContactText.text = $"Last Contact: {fs.lastContact}";
        statusText.text = fs.onGround ? "On Ground" : "Airborne";

    }

    public void Show(FlightState fs)
    {
        UpdateFields(fs);

        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(cg, 0, 1, 0.25f));
    }

    public void Hide()
    {
        if (!gameObject.activeSelf)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(cg, 1, 0, 0.25f, () =>
        {
            gameObject.SetActive(false);
        }));
    }


    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration, System.Action onEnd = null)
    {
        float t = 0f;
        cg.alpha = from;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }

        cg.alpha = to;
        onEnd?.Invoke();
    }

}

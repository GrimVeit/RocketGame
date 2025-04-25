using TMPro;
using UnityEngine;

public class AltitudeRocketView : View
{
    [SerializeField] private TextMeshProUGUI textAltitude;

    public void SetAltitude(int altitude)
    {
        textAltitude.text = $"{altitude}m";
    }
}

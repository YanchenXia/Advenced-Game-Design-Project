using UnityEngine;
using TMPro; 

public class GravityUI : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    
    
    public CustomPlayerController player; 

    void Start()
    {
        
        UpdateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        
        if (player == null || statusText == null) return;

        bool grav = player.isGravityFlipped;
        bool freeze = player.isFreezeEnabled;

        string gravState = grav ? "<color=green>ENABLED</color>" : "<color=red>DISABLED</color>";
        string freezeState = freeze ? "<color=red>DISABLED</color>" : "<color=green>ENABLED</color>";

        statusText.text = $"GRAVITY FLIP: {gravState}\nFREEZE: {freezeState}";
    }
}
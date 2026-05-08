using UnityEngine;
using TMPro; 

public class GravityUI : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    
    private bool isGravityFlipped = false;
    private bool isFreezeEnabled = false;

    void Start()
    {
        UpdateDisplay();
    }

    public void ToggleGravityText()
    {
        isGravityFlipped = !isGravityFlipped;
        UpdateDisplay();
    }

    public void ToggleFreezeText()
    {
        isFreezeEnabled = !isFreezeEnabled;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (statusText != null)
        {
            //displays gravity state
            string gravState = isGravityFlipped ? "<color=green>ENABLED</color>" : "<color=red>DISABLED</color>";
            //displays freeze state
            string freezeState = isFreezeEnabled ? "<color=red>DISABLED</color>" : "<color=green>ENABLED</color>";

            statusText.text = $"GRAVITY FLIP: {gravState}\nFREEZE: {freezeState}";
        }
    }
}
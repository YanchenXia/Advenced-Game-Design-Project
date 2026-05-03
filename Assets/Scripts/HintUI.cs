using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    public TextMeshProUGUI hintTextDisplay;
    private float hideTimer = 0f;

    void Start()
    {
        //Remove text
        if (hintTextDisplay != null) hintTextDisplay.text = "";
    }

    void Update()
    {
        //count down while timer running
        if (hideTimer > 0)
        {
            hideTimer -= Time.deltaTime;
            
            //remove text at 0
            if (hideTimer <= 0 && hintTextDisplay != null)
            {
                hintTextDisplay.text = "";
            }
        }
    }

    //show message for designated time
    public void ShowHint(string message, float displayTime = 7f)
    {
        if (hintTextDisplay != null)
        {
            hintTextDisplay.text = message;
            hideTimer = displayTime;
        }
    }
}
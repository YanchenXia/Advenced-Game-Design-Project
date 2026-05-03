using UnityEngine;
using UnityEngine.Events;

public class BlockButton : MonoBehaviour
{
    [Header("Visual Settings")]
    public Transform buttonTop; 
    public float pressDistance = 0.1f; 
    public Color activeColor = Color.green; 

    [Header("Gameplay Settings")]
    public bool staysActivatedForever = true; 

    [Header("Button Triggers")]
    //make list in inspector
    public UnityEvent onButtonPressed; 

    private int blocksOnButton = 0; 
    private Vector3 originalPosition;
    private Renderer buttonRenderer;
    private Color originalColor;
    private bool isLocked = false; 

    void Start()
    {
        if (buttonTop != null)
        {
            originalPosition = buttonTop.localPosition;
            buttonRenderer = buttonTop.GetComponent<Renderer>();
            if (buttonRenderer != null)
            {
                originalColor = buttonRenderer.material.color;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isLocked) return;
        if (other.CompareTag("Player") || other.attachedRigidbody == null) return;

        blocksOnButton++;
        if (blocksOnButton == 1)
        {
            PressButton();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isLocked) return;
        if (other.CompareTag("Player") || other.attachedRigidbody == null) return;

        blocksOnButton--;
        if (blocksOnButton <= 0)
        {
            blocksOnButton = 0; 
            ReleaseButton();
        }
    }

    void PressButton()
    {
        Debug.Log("Button PRESSED by a puzzle block!");
        
        if (buttonTop != null) buttonTop.localPosition = originalPosition - new Vector3(0, pressDistance, 0);
        if (buttonRenderer != null) buttonRenderer.material.color = activeColor;

        onButtonPressed.Invoke();

        if (staysActivatedForever) isLocked = true;
    }

    void ReleaseButton()
    {
        Debug.Log("Button RELEASED!");
        
        if (buttonTop != null) buttonTop.localPosition = originalPosition;
        if (buttonRenderer != null) buttonRenderer.material.color = originalColor;
    }
}
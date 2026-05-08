using UnityEngine;

public class InteractableHint : MonoBehaviour
{
    //typing box bigger in inspector
    [TextArea(3, 10)] 
    public string hintMessage = "Type your specific hint for this room here!";
}
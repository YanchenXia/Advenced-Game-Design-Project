using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 50f;
    public float ySensitivity = 50f;
    public Transform orientation;
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Vertical rotation
        xRotation -= mouseY * ySensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation
        transform.Rotate(Vector3.up * mouseX * xSensitivity * Time.deltaTime);
        if (orientation != null)
            orientation.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
    
    /*
     The previous issue with the camera was that it was a floaty and the mouse was not properly locking into place.
      The fixes for this were to add a proper horizontal rotation plus adding a cursor lock in input manager.
     ZS
    */

    //ui sensitivity customization
    public void ChangeSensitivity(float newSensitivity)
    {
        xSensitivity = newSensitivity;
        ySensitivity = newSensitivity;
    }
}
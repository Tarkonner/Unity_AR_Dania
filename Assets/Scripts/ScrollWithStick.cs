using UnityEngine;

public class ScrollWithStick : MonoBehaviour
{
    public Joystick joystick;  // Reference til joystick
    public float rotationSpeed = 100f; // Justerbar rotationshastighed

    void Update()
    {
        float horizontalInput = joystick.Horizontal; // Hent joystickets X-værdi
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class ScrollWithStick : MonoBehaviour
{
    public float rotationSpeed = 100f; // Justerbar rotationshastighed

    void Update()
    {
        //float horizontalInput = joystick.Horizontal; // Hent joystickets X-v�rdi
        transform.Rotate(Vector3.up * -PlayerInput.instance.stickInput.x * rotationSpeed * Time.deltaTime);
    }
}

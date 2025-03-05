using UnityEngine;

public class ScrollWithStick : MonoBehaviour
{
    public float rotationSpeed = 100f; // Justerbar rotationshastighed

    void Update()
    {
        if (ImageTracking.instance == null || ImageTracking.instance.closetObject != gameObject)
            return;

        //float horizontalInput = joystick.Horizontal; // Hent joystickets X-værdi
        transform.Rotate(Vector3.up * -PlayerInput.instance.stickInput.x * rotationSpeed * Time.deltaTime);
    }
}

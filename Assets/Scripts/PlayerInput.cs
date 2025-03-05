using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    DynamicJoystick joystick;
    public Vector2 stickInput { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;

        joystick = GetComponent<DynamicJoystick>();
         
    }

    // Update is called once per frame
    void Update()
    {
        stickInput = joystick.Direction;
    }
}

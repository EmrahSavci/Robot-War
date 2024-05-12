using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetJoystick : MonoBehaviour
{
    public static SetJoystick Instance;

    [Header("Joystick")]
    public Joystick moveControllJoystick;
    public Joystick standartFireJoystick;
    public Joystick bombZoneJoystick;
    private void Awake()
    {
        Instance = this;
    }

}

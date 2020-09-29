using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemahcineGetInputTouchAxis : MonoBehaviour
{
    public float TouchSensitivity_x;
    public float TouchSensitivity_y;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInput;
    }

    public void EnableInput()
    {
        CinemachineCore.GetInputAxis = HandleAxisInput;
    }
    public void DisableInput()
    {
        CinemachineCore.GetInputAxis = ZeroAxisInput;
    }

    public float ZeroAxisInput(string axisName)
    {
        return 0f;
    }
    private float HandleAxisInput(string axisName)
    {
        switch(axisName)
        {
            case "Mouse X":
                if (Input.touchCount > 0)
                {
                    return Input.touches[0].deltaPosition.x / TouchSensitivity_x;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    return Input.touches[0].deltaPosition.y / TouchSensitivity_y;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0;
    }



}

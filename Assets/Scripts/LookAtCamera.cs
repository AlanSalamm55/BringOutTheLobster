using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
    }

    [SerializeField] private Mode mode;

    void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookAtInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
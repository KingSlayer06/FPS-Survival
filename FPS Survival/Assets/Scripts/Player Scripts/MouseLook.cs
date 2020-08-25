using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot;
    [SerializeField] private bool invertCamera;
    [SerializeField] private bool cursorUnlock = true;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float smoothSteps = 10f;
    [SerializeField] private float smoothWeights = 0.4f;
    [SerializeField] private Vector2 lookLimits;

    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;
    private float lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookLimits = new Vector2(-70f, 80f);
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlock();
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    private void LockAndUnlock()
    {
        if(Input.GetKeyDown(Keycode.ESCAPE))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if(Input.GetKeyDown(Keycode.MOUSE_LEFTCLICK))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(Axis.MOUSE_Y), Input.GetAxis(Axis.MOUSE_X));
        lookAngles.x += currentMouseLook.x * sensitivity * (invertCamera ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensitivity;
        lookAngles.x = Mathf.Clamp(lookAngles.x, lookLimits.x, lookLimits.y);

        transform.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}

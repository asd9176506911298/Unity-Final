using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform arms;
    [SerializeField] private Transform body;
    [SerializeField] private Text mouseSensitivityText;
    private float xRot;

    private void Start()
    {
        LockCursor();
        mouseSensitivity = 550;
    }

    private void Update()
    {
        HandleMouseLook();
        mouseSensitivityText.text = "MouseSensitivity: " + mouseSensitivity.ToString();
        mouseSensitivityAddSub();
    }

    private void mouseSensitivityAddSub()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            mouseSensitivity -= 50;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            mouseSensitivity += 50;
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);
        arms.localRotation = Quaternion.Euler(new Vector3(xRot, 0, 0));
        body.Rotate(new Vector3(0, mouseX, 0));

    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}

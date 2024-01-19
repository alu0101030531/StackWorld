using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 0.01f;

    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Get the mouse position and translate a gameObject that will be followed by the cinemachine camera
    void Update()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");
        Vector3 movement = new Vector3(xMouse, yMouse, 0f);
        transform.Translate(movement * Time.deltaTime * mouseSensitivity);

        Center();
    }

    // If space is pressed the camera is centered in 0,0,0
    void Center() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}

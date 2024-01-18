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

    void Update()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");
        Vector3 movement = new Vector3(xMouse, yMouse, 0f);
        transform.Translate(movement * Time.deltaTime * mouseSensitivity);

        Center();
    }

    void Center() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}

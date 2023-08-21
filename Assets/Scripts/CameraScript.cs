using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject player;
    public float mouseSensitivity = 500;

    float mouseX;
    float mouseY;

    float xRotation = 0f;

    void Start()
    {
        player = this.gameObject;
        Camera.main.fieldOfView = 100;
        Camera.main.transform.rotation = Quaternion.Euler(0,0,0);
        transform.rotation = Quaternion.Euler(0,0,0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 2;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        transform.Rotate(0f, mouseX * 2, 0f);

        
    }
}

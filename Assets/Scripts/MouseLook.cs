using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{   
    [SerializeField] private float mouseSensitivityX = 800f;
    [SerializeField] private float mouseSensitivityY = 700f;
    [SerializeField] private Transform playerBody; //Transform of "First Person Player" Object

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//Lock Cursor in the center
    }

    void Update()
    {
        //Input.GetAxis is based on the Unity Input settings (edit -> Project Settings -> Input Manager)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Kopf darf sich nicht überschlagen


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//Y Achse rotieren; Quaternion ist für rotation
        playerBody.Rotate(Vector3.up * mouseX); //X Achse Rotieren
    }
}

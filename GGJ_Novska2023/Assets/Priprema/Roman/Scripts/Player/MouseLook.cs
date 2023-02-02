using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSens = 100f;
    public Transform PlayerHead;
    float CameraXRot;
    // Start is called before the first frame update
    void Start()
    {
     //Sakrivanje Cursor-a
     Cursor.visible= false;
     Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime);
        CameraXRot -= MousePos.y;
        CameraXRot = Mathf.Clamp(CameraXRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(CameraXRot, 0f, 0f);
        PlayerHead.Rotate(Vector3.up * MousePos.x);
    }
}

using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX; //changeable by user
    public float sensY; //changeable by user

    public Transform orientation; //child objekt playera koji samo prati orijentaciju

    float xRot;
    float yRot;

    private void Start()
    {
        //HIDE & LOCK CURSOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(!GameManager.instance.inShop)
        {
            //MOUSE INPUT
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

            yRot += mouseX;
            xRot -= mouseY;

            //LIMIT LOOKING UP & DOWN
            xRot = Mathf.Clamp(xRot, -90, 90);

            //ROTATE PLAYER AND CAMERA
            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);
        }
    }
}

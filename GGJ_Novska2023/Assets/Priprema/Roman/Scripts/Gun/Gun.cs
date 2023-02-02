using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float GunDamage;
    public Camera PlayerCamera;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) Shoot();
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hitInfo))
        {
            Debug.Log("HIT: " + hitInfo.transform.name);
        }
    }
}

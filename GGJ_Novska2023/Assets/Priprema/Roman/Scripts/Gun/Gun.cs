using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float GunDamage;
    public float GunRange;
    public Camera PlayerCamera;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) Shoot();
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hitInfo, GunRange))
        {
            //Debug.Log("HIT: " + hitInfo.transform.name);
            Enemy EnemyInfo = hitInfo.transform.GetComponent<Enemy>();
            if (EnemyInfo != null)
            {
                EnemyInfo.EnemyHit(GunDamage);
                Debug.Log("EnemyHealth: " + EnemyInfo.EnemyHealth);
            }
        }
    }
}

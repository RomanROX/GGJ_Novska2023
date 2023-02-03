using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemyHealth;

    public void EnemyHit(float GunDamage)
    {
        EnemyHealth -= GunDamage;
        if (EnemyHealth <= 0) EnemyDie();
    }
    public void EnemyDie()
    {
        Destroy(gameObject);
    }
}

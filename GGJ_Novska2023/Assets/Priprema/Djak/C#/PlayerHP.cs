using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public Slider slider;
    private void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
    }

    private void Update()
    {
        slider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //GAME OVER
        }
    }
}

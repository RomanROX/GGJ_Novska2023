using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public Image image;
    public Text text;
    float damageTimer = .3f;
    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, health / maxHealth, .2f);
        text.text = Mathf.Round(health).ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (damageTimer <= 0)
            {
                TakeDamage(10);
                if (health <= 0)
                {
                    GameManager.instance.GameOver();
                }
                damageTimer = .3f;
            }
            else damageTimer -= Time.deltaTime;
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}

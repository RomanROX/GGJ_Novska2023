using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlant : MonoBehaviour
{
    public float maxWater;
    public float maxHealth;
    [HideInInspector] public float health;
    [HideInInspector] public float water;

    public Image waterAmount, healthAmount;
    public Text waterText, healthText;
    private void Start()
    {
        water = maxWater;
        health = maxHealth;
    }

    private void LateUpdate()
    {
        if(water >= 0) 
        {
            water -= Time.deltaTime * 5f;
            waterAmount.fillAmount = Mathf.Lerp(waterAmount.fillAmount, water / maxWater, .2f);
            healthAmount.fillAmount = Mathf.Lerp(healthAmount.fillAmount, health / maxHealth, .2f);

            healthText.text = health.ToString();
            waterText.text = Mathf.Round(water).ToString();
        }
        else
        {
            GameManager.instance.GameOver();
        }
        if(health <= 0) GameManager.instance.GameOver();
    }

    public IEnumerator Regenerate()
    {
        float timer = 0;
        while (timer < 30)
        {
            if(health <= maxHealth) health += Time.deltaTime;
            timer += Time.deltaTime;
        }
        yield return null;
    }    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlant : MonoBehaviour
{
    public float maxWater;
    public float maxHealth;
    private float health;
    private float water;

    private void Start()
    {
        water = maxWater;
        health = maxHealth;
    }

    private void Update()
    {
        if(water >= 0) 
        {
            water -= Time.deltaTime * .5f;
        }
        else
        {
            //GAME OVER
        }
    }
}
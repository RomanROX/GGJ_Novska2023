using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;

    public float minSpeed = .5f;
    public float maksSpeed = 1.5f;
    public int minHealth = 60; 
    public int maksHealth = 200;
    public int minCoins = 15;
    public int maxCoins = 45;
    public float waterTakeAmount = 100f;
    public int damage = 25;

    float speed;
    float radius;
    int coinAmount;

    private Transform target;

    float distance; //DISTANCE BTWN TARGET AND SELF
    int maxHealth;
    private int health;
    private float damageTimer = 1f;
    bool chasingPlayer = false;


    private void Start()
    {
        speed = Random.Range(minSpeed, maksSpeed);
        radius = 6;
        maxHealth = Random.Range(minHealth, maksHealth);

        health = maxHealth;
        coinAmount = Random.Range(minCoins, maxCoins);
        target = GameObject.Find("Pivot").transform;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.cash += Mathf.RoundToInt(coinAmount);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);

        if (distance <= radius)
        {
            chasingPlayer = true;
            if (distance > 1)
                Chase(GameObject.Find("Player").transform);
        }
        else if (Vector3.Distance(target.position, transform.position) <= 1)
        {
            chasingPlayer = false;
            transform.LookAt(target);
        }
        else
        {
            chasingPlayer = false;
            Chase(target);
        }
    }

    private void OnTriggerStay(Collider other)
    {    
        if (other.CompareTag("Plant") && !chasingPlayer)
        {
            if (damageTimer <= 0)
            {
                other.GetComponent<MainPlant>().health -= damage;
                damageTimer = 1f;
            }
            else damageTimer -= Time.deltaTime;   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plant") && !chasingPlayer)
            other.GetComponent<MainPlant>().water -= waterTakeAmount;
    }

    public void Chase(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 3f, 0), speed * Time.deltaTime); //MOVE ENEMY TOWARDS PLAYER
        var targetRotation = Quaternion.LookRotation(target.position - transform.position); //SMOOTH ROTATION
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, .05f);
    }

    private void OnDrawGizmos() //DEBUG, EASIER TO BALANCE
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region VARIABLES
    float speed;
    float radius;
    int coinAmount;

    private Transform target;

    float distance; //DISTANCE BTWN TARGET AND SELF
    int maxHealth;
    private int health;
    private float damageTimer = .3f;
    bool chasingPlayer = false;
    #endregion

    private void Start()
    {
        speed = Random.Range(.5f, 1.5f);
        radius = 6;
        maxHealth = Random.Range(95, 135);

        health = maxHealth;
        coinAmount = Random.Range(15, 45);
        target = GameObject.Find("Pivot").transform;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.cash += Mathf.RoundToInt(coinAmount);
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);

        if (distance <= radius)
        {
            chasingPlayer = true;
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
                other.GetComponent<MainPlant>().health -= 25;
                damageTimer = .3f;
            }
            else damageTimer -= Time.deltaTime;   
        }
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
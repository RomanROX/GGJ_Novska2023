using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region VARIABLES
    public float speed;
    public float radius;
    int coinAmount;

    private Transform target;

    float distance; //DISTANCE BTWN TARGET AND SELF
    public int maxHealth;
    private int health;
    #endregion

    private void Start()
    {
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
        distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position); //CALCULATE DISTANCE BETWEEN TARGET & SELF

        if (distance <= radius) //CHASE IF DISTANCE <= RADIUS, OTHERWISE PATROLL
        {
            Chase(GameObject.Find("Player").transform);
        }
        else if(Vector3.Distance(target.position, transform.position) <= 1)
        {
            transform.LookAt(target);
        }
        else Chase(target);
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
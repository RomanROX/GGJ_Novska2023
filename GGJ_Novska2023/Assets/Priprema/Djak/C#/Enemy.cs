using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region VARIABLES
    public float speed;
    public float radius;
    [HideInInspector] public float timeBtwnAttacks = .5f; //ONLY USEABLE IF CANSHOOT IS TRUE

    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool hasAttacked; //FOR DELAY
    bool walkPointSet;
    private Transform target;

    Vector3 walkPoint;
    public LayerMask ground;

    float distance; //DISTANCE BTWN TARGET AND SELF

    [HideInInspector] public float health;
    public float maxHealth;
    #endregion

    private void Start()
    {
        target = GameObject.Find("MainPlant").transform;
        //transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); //SCALES THE ENEMY
    }

    public void Update()
    {
        distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position); //CALCULATE DISTANCE BETWEEN TARGET & SELF

        if (distance <= radius) //CHASE IF DISTANCE <= RADIUS, OTHERWISE PATROLL
        {
            Chase(GameObject.Find("Player").transform);
            if (canShoot)
            {
                Attack();
            }
        }
        else
        {
            Chase(target);
        }
    }

    public void SetWalkPoint() //GET RANDOM POSITION IN THE SCENE TO GO, STAY ON GROUND
    {
        float randX = Random.Range(-10, 10);
        float randZ = Random.Range(-10, 10);

        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground)) walkPointSet = true;
    }

    public void Chase(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime); //MOVE ENEMY TOWARDS PLAYER

        var targetRotation = Quaternion.LookRotation(target.position - transform.position); //SMOOTH ROTATION
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .05f);
    }

    public void Patroll()
    {
        if (!walkPointSet) SetWalkPoint(); //SET POSITION TO GO
        if (walkPointSet) //IF POSITION HAS BEEN SET, GO THERE
        {
            var targetRotation = Quaternion.LookRotation(walkPoint - transform.position); //SMOOTH ROTATION
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, walkPoint, speed * Time.deltaTime), Quaternion.Slerp(transform.rotation, targetRotation, .05f)); //MOVE AND SMOOTHLY ROTATE ENEMY

            Vector3 distanceToPoint = transform.position - walkPoint; //CALCULATE DISTANCE TO WALK POINT
            if (distanceToPoint.magnitude < 5f) //ENEMY CAME TO WALKPOINT = SHOULD SET NEW ONE
            {
                walkPointSet = false;
            }
        }
    }

    void Attack()
    {
        if (!hasAttacked)
        {
            //attack code

            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBtwnAttacks); //ATTACK DELAY
        }
    }

    void ResetAttack()
    {
        hasAttacked = false;
    }

    private void OnDrawGizmos() //DEBUG, EASIER TO BALANCE
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void DoRagdoll(bool isRagdoll)
    { 
    
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //FindObjectOfType<Wave_Manager>().SpawnWave();
            Destroy(gameObject);
        }
    }
}

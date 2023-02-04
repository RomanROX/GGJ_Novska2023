using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public LayerMask enemyLayer;
    private Rigidbody rb;
    public GameObject explosion;

    [Header("Stats")]
    public bool useGravity;
    [Range(0, 1f)] public float bounciness;

    [Header("Damage & Lifetime")]
    public float explosionRange;
    public float lifeTime;
    public int maxCollisions;
    public bool explodeOnTouch;
    public float damage = 10;

    int collisions;
    PhysicMaterial pm;

    private void Start()
    {
        Setup();
    }
    void Setup()
    {
        rb = GetComponent<Rigidbody>();
        pm = new PhysicMaterial
        {
            bounciness = bounciness,
            frictionCombine = PhysicMaterialCombine.Minimum,
            bounceCombine = PhysicMaterialCombine.Maximum
        };
        GetComponent<SphereCollider>().material = pm;

        rb.useGravity = useGravity;
    }

    private void Update()
    {
        if(useGravity) rb.AddForce(Vector3.up); //weaker gravity
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Explode();
        if (collisions >= maxCollisions) Explode();
    }

    void Explode()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        Invoke(nameof(Destroy2), .05f);
    }

    void Destroy2()
    {
        Destroy(gameObject);    
    }

    private void OnTriggerEnter(Collider other)
    {
        collisions++;

        if (other.CompareTag("Enemy") && explodeOnTouch)
        {
            other.GetComponent<Enemy>().TakeDamage(Mathf.RoundToInt(damage * GameManager.instance.damageMultiplier));
            Explode();
        }
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

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
    public float damage;

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

            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (var item in colliders)
            {
                if (item.CompareTag("Enemy"))
                {
                    item.GetComponent<Enemy>().DoRagdoll(true);
                    Rigidbody[] rbs = item.GetComponentsInChildren<Rigidbody>();
                    foreach (var rb in rbs)
                        rb.AddExplosionForce(15f / rb.mass, transform.position, 2f);
                }
                else
                {
                    if (item.TryGetComponent<Rigidbody>(out var rb)) rb.AddExplosionForce(800f / rb.mass, transform.position, 2f);
                }
            }
        }
        Invoke(nameof(Destroy2), .05f);
    }

    void Destroy2()
    {
        Destroy(gameObject);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;

        if (collision.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            collision.collider.GetComponent<Enemy>().TakeDamage(damage);
            Explode();
        }
            
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

using UnityEngine;

public class SpellProectile : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 1000f;
    public float speed = 10f;
    public bool isHoming = false;
    public float homingStrength = 5f;
    public string[] targetTags = { "Enemy" };

    [Header("Debug")]
    [SerializeField] private bool showDebug = true;
    
    private Transform target;

    void Start()
    {
        if(showDebug) Debug.Log("Projectile spawned", this);
        Destroy(gameObject, 3f); // Auto-destroy after 3 seconds
        
        if(isHoming) FindNearestEnemy();
    }

    public void Initialize(float newDamage, float newSpeed, bool newHoming, float newHomingStrength)
    {
        damage = newDamage;
        speed = newSpeed;
        isHoming = newHoming;
        homingStrength = newHomingStrength;

        if(showDebug) Debug.Log($"Initialized: {damage} damage, Homing: {isHoming}");
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if(isHoming && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                homingStrength * Time.deltaTime
            );
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy;
        if(showDebug) Debug.Log(target ? $"Tracking: {target.name}" : "No enemies found");
    }

    void OnTriggerEnter(Collider other)
    {
        foreach(string tag in targetTags)
        {
            if(other.CompareTag(tag))
            {
                if(showDebug) Debug.Log($"Hit {other.name}", other);
                
                // Try all health components in hierarchy
                Health[] healthComponents = other.GetComponentsInParent<Health>();
                foreach(Health health in healthComponents)
                {
                    health.TakeDamage(damage);
                    if(showDebug) Debug.Log($"Dealt {damage} to {health.name}", health);
                }
                
                Destroy(gameObject);
                return;
            }
        }
    }

    void OnDrawGizmos()
    {
        if(showDebug && isHoming && target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
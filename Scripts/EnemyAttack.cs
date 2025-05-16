using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time > lastAttackTime + attackCooldown && 
            Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        if (player.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damage);
        }
    }
}

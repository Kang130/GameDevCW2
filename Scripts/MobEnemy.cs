// using UnityEngine;

// public class MobEnemy : MonoBehaviour
// {
//     public int maxHealth = 100;
//     public int damage = 10;
//     public float moveSpeed = 3f;
//     public int expReward = 20;
//     public int manaCoreChance = 10; // 10% chance to drop mana core

//     [Header("References")]
//     public Health health;
//     public PlayerStats playerStats;
    
//     void Start()
//     {
//         health = GetComponent<Health>();
//         health.Initialize(maxHealth);
//         playerStats = FindObjectOfType<PlayerStats>();
        
//         health.OnDeath += OnEnemyDeath;
//     }

//     void Update()
//     {
//         // Basic AI - move toward player
//         Transform player = GameObject.FindGameObjectWithTag("Player").transform;
//         transform.position = Vector3.MoveTowards(
//             transform.position,
//             player.position,
//             moveSpeed * Time.deltaTime
//         );
//     }

//     void OnEnemyDeath()
//     {
//         // Reward player
//         playerStats.GainExp(expReward);
        
//         // Chance to drop mana core
//         if(Random.Range(0, 100) < manaCoreChance)
//         {
//             playerStats.GainManaCore();
//         }
        
//         Destroy(gameObject);
//     }

//     void OnCollisionEnter(Collision collision)
//     {
//         if(collision.gameObject.CompareTag("Player"))
//         {
//             collision.gameObject.GetComponent<Health>().TakeDamage(damage);
//         }
//     }
// }

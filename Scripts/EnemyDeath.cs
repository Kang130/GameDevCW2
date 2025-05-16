using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject lootDrop;
    [SerializeField] private float lootChance = 0.2f;


    void Start()
    {
        health.OnDeath += HandleEnemyDeath;
    }

    void HandleEnemyDeath () {
        GetComponent<Animator>().SetTrigger("Die");

        // GetComponent<EnemyAI>().enabled =false;
        GetComponent<Collider>().enabled = false;

        if (UnityEngine.Random.value <= lootChance) {
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 10f);
    }

}

using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private float deathDelay = 0f;
    [SerializeField] private bool isInvincible = false;

    public event Action OnTakeDamage;
    public event Action OnDeath;
    public event Action OnHeal;
    public event Action<float> OnHealthChanged;

    private float currentHealth;
    private bool isDead = false;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => isDead;
    public bool IsFullHealth => Mathf.Approximately(currentHealth, maxHealth);
    public float HealthPercentage => currentHealth / maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        if (isDead || isInvincible) return;
        damage = Mathf.Abs(damage);

        currentHealth = Mathf.Max(0, currentHealth - damage);

        OnTakeDamage?.Invoke();
        OnHealthChanged?.Invoke(HealthPercentage);

        if (currentHealth <= 0) {
            Die();
        }

    }

    private void Die() {
        isDead = true;
        OnDeath?.Invoke();
    }

    public void Heal(float amount) {
        if (isDead) return;
        currentHealth = Mathf.Min(MaxHealth, currentHealth + amount);

        OnHeal?.Invoke();
        OnHealthChanged?.Invoke(HealthPercentage);
    }

    public void RefreshHeal()
    {
        float currentPercentage = HealthPercentage;
        float newMax = MaxHealth;
        currentHealth = newMax * currentPercentage;
    }


}

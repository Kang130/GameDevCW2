using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{

    public Health health;
    public ManaSystem mana;
    
    public Slider healthBar;
    public Slider manaBar;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = health.MaxHealth;
            healthBar.value = health.CurrentHealth;
        }

        if (manaBar != null)
        {
            manaBar.maxValue = mana.MaxMana;
            manaBar.value = mana.CurrentMana;
        }

    
        health.OnHealthChanged += UpdateHealthBar;
    }

    void Update()
    {
        if (manaBar != null)
        {
            manaBar.value = mana.CurrentMana;
        }
    }

    private void UpdateHealthBar(float percentage)
    {
        if (healthBar != null)
        {
            healthBar.value = percentage * health.MaxHealth;
        }
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= UpdateHealthBar;
    }
}
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public PlayerStats stats;

    private float currentMana;

    public float CurrentMana => currentMana;  // <-- Make CurrentMana return the actual current mana

    public float MaxMana => stats.MaxMana;
    public float ManaPercentage => currentMana / MaxMana;

    public void Init(PlayerStats stats)
    {
        this.stats = stats;
        currentMana = stats.MaxMana;  // initialize currentMana here
    }

    void Update()
    {
        if (currentMana < MaxMana)
        {
            currentMana = Mathf.Min(MaxMana, currentMana + (2f + stats.intelligence * 0.25f) * Time.deltaTime);
        }
    }

    public bool EnoughmMana(float baseCost)
    {
        float actualCost = baseCost * (1f - stats.ManaCostReduction);

        if (currentMana >= actualCost)
        {
            currentMana -= actualCost;
            return true;
        }
        return false;
    }

    public void RefreshMana()
    {
        float currentManaPercentage = ManaPercentage;
        currentMana = MaxMana * currentManaPercentage;
    }
}

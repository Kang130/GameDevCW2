using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    public string spellName = "New spell";
    public GameObject projectilePrefab;
    public Sprite icon;
    public ParticleSystem castEffect;

    public float baseCooldown = 2f;
    public float baseManaCost = 10f;
    public float baseDamage = 15f;
    public float castRange = 8f;
    public float projectileSpeed = 10f;

    public bool canCrit = true;
    public int manaCoresRequired = 0;

    public bool autoCastAllowed = true;
    public bool isHoming = false;
    public float homingStrength = 5f;

    public virtual float CalculateDamage(PlayerStats stats)
    {
        float damage = baseDamage * stats.SpellDamage;
        if (canCrit && Random.value < stats.CritChance / 100f)
            damage *= stats.CritDamage;
        return damage * stats.ManaCoreBonus;
    }
    
}

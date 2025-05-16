using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public SpellCaster spellCaster;
    public ManaSystem manaSystem;

    private PlayerStats stats;

    void Awake()
    {
        stats = new PlayerStats();
        stats.spellCaster = spellCaster;

        spellCaster.stats = stats;
        manaSystem.Init(stats);
    }

    public PlayerStats GetStats()
    {
        return stats;
    }
}
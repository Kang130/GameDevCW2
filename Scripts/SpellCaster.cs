using UnityEngine;
using System.Collections.Generic;

public class SpellCaster : MonoBehaviour
{
    public Transform castPoint;
    public ManaSystem mana;
    public PlayerStats stats;
    public AudioSource audioSource;

    public Spell[] activeSpells = new Spell[5];
    private float[] activeCooldowns;
    public List<Spell> autoSpellLibrary = new List<Spell>();
    private List<Spell> activeAutoSpells = new List<Spell>();
    private float[] autoCooldowns;

    void Start()
    {
        activeCooldowns = new float[activeSpells.Length];
        UpdateAutoSpells();
        mana = GetComponent<ManaSystem>();
        stats = GetComponent<PlayerStats>();
        if (mana != null && stats != null)
        {
            mana.Init(stats);
        }
        else
        {
            Debug.LogError("Missing ManaSystem or PlayerStats component");
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E KEY PRESSED - Input detected");
            TryCastActive(0);
        }
        HandleActiveInput();
        UpdateAutoCasts();
        UpdateCooldowns();
    }
    void UpdateCooldowns()
{
    for (int i = 0; i < activeCooldowns.Length; i++)
    {
        if (activeCooldowns[i] > 0)
            activeCooldowns[i] -= Time.deltaTime;
    }
}

    void UpdateAutoSpells()
    {
        activeAutoSpells.Clear();
        foreach(Spell spell in autoSpellLibrary)
        {
            if(spell.autoCastAllowed && stats.manaCores >= spell.manaCoresRequired)
            {
                activeAutoSpells.Add(spell);
            }
        }
        autoCooldowns = new float[activeAutoSpells.Count];
    }

    void HandleActiveInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) TryCastActive(0);
        if(Input.GetKeyDown(KeyCode.Alpha2)) TryCastActive(1);
        if(Input.GetKeyDown(KeyCode.Alpha3)) TryCastActive(2);
        if(Input.GetKeyDown(KeyCode.Alpha4)) TryCastActive(3);
        if(Input.GetKeyDown(KeyCode.Alpha5)) TryCastActive(4);
    }

    void TryCastActive(int slot)
    {
        Debug.Log($"Attempting to cast slot {slot}");
        if (slot >= activeSpells.Length)
    {
        Debug.LogError($"Slot {slot} is out of bounds Array size - {activeSpells.Length}");
        return;
    }
     if (activeSpells[slot] == null)
    {
        Debug.LogError($"No spell assigned to slot {slot}!");
        return;
    }

    Spell spell = activeSpells[slot];
    Debug.Log($"Spell found - {spell.spellName}");
        
        if (activeCooldowns[slot] > 0)
        {
            Debug.Log($"Spell '{spell.spellName}' on cooldown: {activeCooldowns[slot]:F1}s remaining");
            return;
        }

        // Debug 3: Check mana
        if (!mana.EnoughmMana(spell.baseManaCost))
        {
            Debug.Log($"Not enough mana for '{spell.spellName}' (Cost: {spell.baseManaCost}, Current: {mana.CurrentMana})");
            return;
        }

        Debug.Log($"Casting '{spell.spellName}' successfully");
        CastSpell(spell);
        activeCooldowns[slot] = spell.baseCooldown * (1 - stats.CooldownReduction);
    }
        

    void UpdateAutoCasts()
    {
        for(int i = 0; i < activeAutoSpells.Count; i++)
        {
            if(autoCooldowns[i] <= 0)
            {
                if(mana.EnoughmMana(activeAutoSpells[i].baseManaCost))
                {
                    CastSpell(activeAutoSpells[i]);
                    autoCooldowns[i] = activeAutoSpells[i].baseCooldown * (1 - stats.CooldownReduction);
                }
            }
            else
            {
                autoCooldowns[i] -= Time.deltaTime;
            }
        }
    }

    void CastSpell(Spell spell)
    {

        Debug.Log($"Attempting to cast {spell.spellName}");
        Debug.Log($"Current: {mana.CurrentMana})");

        if (spell.projectilePrefab == null)
        {
            Debug.LogError("No projectile prefab assigned to spell");
            return;
        }
        if (castPoint == null)
        {
            Debug.LogError("No castPoint assigned");
            return;
        }
        Debug.Log($"Spawning projectile at {castPoint.position}");
        if (spell.castEffect != null)
            Instantiate(spell.castEffect, castPoint.position, castPoint.rotation);


        GameObject projectile = Instantiate(
            spell.projectilePrefab,
            castPoint.position,
            castPoint.rotation
        );

        SpellProectile proj = projectile.GetComponent<SpellProectile>();
        proj.Initialize(
            spell.CalculateDamage(stats),
            spell.projectileSpeed,
            spell.isHoming,
            spell.homingStrength
        );
        Debug.Log($"Casting {spell.spellName} with {proj.damage} damage");
    }

    public void OnManaCoreGained()
    {
        UpdateAutoSpells();
    }
}
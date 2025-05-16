using UnityEngine;
using System;


[System.Serializable]
public class PlayerStats 
{

    public SpellCaster spellCaster;

    // Base stats for character not sure why i have strength maybe affect certain spells 
    public int level = 1;
    public int vitality = 5;
    public int strength = 5;
    public int agility = 5;
    public int intelligence = 5;
    public int luck = 5;
    public int manaCores = 1;
    // What my stats change
    public float MaxHealth => 1 + (vitality * 0.3f) + (strength * 0.1f);
    public float HealthRegen => 2 + (vitality * 0.4f) + (strength * 0.1f);
    public float Defence => (vitality * 0.5f) + (strength * 0.2f);
    public float Power => strength * 2f;
    public float CritChance => (agility * 0.5f) + (luck * 0.25f);
    public float CritDamage => 1.5f + (agility * 0.5f) + (luck * 0.5f); // BUY AN IEEEEE
    public float SpellDamage => (intelligence * 2f) + (strength * 0.2f);
    public float MaxMana => 75 + (intelligence * 5) + (manaCores * 100);
    public float ManaRegen => 2 + (intelligence * 1f) + (manaCores * 2f);
    public float ManaCostReduction => intelligence * 0.01f + manaCores * 0.05f;
    public float CooldownReduction => intelligence * 0.05f + manaCores * 0.01f;
    public float DropChanceMulti => 1 + (luck * 0.05f);
    public int AutoCastSlots => 1 + Mathf.FloorToInt(manaCores);
    public float ManaCoreBonus => 1 + (manaCores * 0.15f);

    public void LevelUp(int pointsToAllocate)
    {
        level++;
    }

    public void GainManaCore()
    {
        manaCores = (manaCores + 1);
        spellCaster.OnManaCoreGained();
    }





}

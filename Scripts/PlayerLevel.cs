using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    public PlayerStats stats;
    public Health health;
    public ManaSystem mana;

    public int currentExp;
    public int expToNextLevel = 100;
    public float expNeedGrowthRate = 1.5f;
    public int statPointsPerLevel = 3;
    public int availableStatPoints;
    public float lUCKSTATBONUSCHANCE = 0.005f;
    public int MaxBonus = 100;

    public UnityEvent onLevelUp;
    public UnityEvent onStatPointUsed;
    public UnityEvent onManaCoreGained;
    public UnityEvent onBonusStatPoint;
    public UnityEvent onRandomStatPoint;
    public UnityEvent onStatPointAllocated;

    void Start()
    {
        UpdateExpReq();
    }

    public void GainExperience(int amount)
    {
        currentExp += Mathf.RoundToInt(amount * stats.DropChanceMulti);
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }

    }

    void LevelUp()
    {
        currentExp -= expToNextLevel;
        stats.level++;
        availableStatPoints += statPointsPerLevel;
        UpdateExpReq();
        LUCK888();
        mana.RefreshMana();
        onLevelUp.Invoke();
        ShowLevelUpUI();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            GainExperience(50);
        }
    }

    void LUCK888()
{
    // Bonus stat points to allocate
    float bonusChance = lUCKSTATBONUSCHANCE + (stats.luck * 0.05f);
    int NumofBonuses = 0;
    
    while (Random.value < bonusChance)
    {
        availableStatPoints++;
        NumofBonuses++;
        bonusChance *= 0.6f;
    }
    
    if (NumofBonuses > 0)
    {
        onBonusStatPoint.Invoke();
        Debug.Log($"LUCK888 granted {NumofBonuses} bonus stat points to allocate!");
    }

    bonusChance = lUCKSTATBONUSCHANCE + (stats.luck * 0.03f);
    if (Random.value < bonusChance)
    {
        int pointsToAdd = Random.Range(1, Mathf.FloorToInt(1 + stats.luck));
        for (int i = 0; i < pointsToAdd; i++)
        {
            int randomStat = Random.Range(0, 5);
            switch (randomStat)
            {
                case 0:
                    stats.vitality++;
                    break;
                case 1:
                    stats.agility++;
                    break;
                case 2:
                    stats.intelligence++;
                    mana.RefreshMana();
                    break;
                case 3:
                    stats.strength++;
                    break;
                case 4:
                    stats.luck++;
                    break;
            }
            onRandomStatPoint.Invoke();
            Debug.Log($"Luck granted +1 to stat {randomStat}!");
        }
        Debug.Log($"Total random stats granted: {pointsToAdd}");
    }
}

public void AllocateStatPoint(string statType)
    {
        if (availableStatPoints <= 0) return;
        
        switch(statType)
        {
            case "Vitality":
                stats.vitality++;
                break;
            case "Strength":
                stats.strength++;
                break;
            case "Agility":
                stats.agility++;
                break;
            case "Intelligence":
                stats.intelligence++;
                mana.RefreshMana();
                break;
            case "Luck":
                stats.luck++;
                break;
        }
        
        availableStatPoints--;
        onStatPointAllocated.Invoke();
    }

    public void OnBossDefeated()
    {
        stats.GainManaCore();
        mana.RefreshMana();
        onManaCoreGained.Invoke();
    }

    void UpdateExpReq()
{
    expToNextLevel = Mathf.RoundToInt(expToNextLevel * expNeedGrowthRate);
}

    void ShowLevelUpUI()
    {
        
        Debug.Log($"Level Up, now level -  {stats.level} Points to allocate - {availableStatPoints}");
    }
}




using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public PlayerLevel playerLevel;

    public Text availablePointsText;
    public Text vitalityText;
    public Text strengthText;
    public Text agilityText;
    public Text intelligenceText;
    public Text luckText;

    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
        playerLevel.onLevelUp.AddListener(ShowUI);
        playerLevel.onStatPointAllocated.AddListener(UpdateUI);
    }

    void ShowUI()
    {
        UpdateUI();
        panel.SetActive(true);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideUI()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AllocateStat(string statType)
    {
        playerLevel.AllocateStatPoint(statType);
        UpdateUI();
    }

    void UpdateUI()
    {
        availablePointsText.text = "Available Points -  " + playerLevel.availableStatPoints;
        vitalityText.text = "Vitality-  " + playerLevel.stats.vitality;
        strengthText.text = "Strength-  " + playerLevel.stats.strength;
        agilityText.text = "Agility-  " + playerLevel.stats.agility;
        intelligenceText.text = "Intelligence -  " + playerLevel.stats.intelligence;
        luckText.text = "Luck - " + playerLevel.stats.luck;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaNumberUI : MonoBehaviour
{
    public ManaSystem manaSystem; 
    public TextMeshProUGUI manaText;        

    void Update()
    {
        if (manaSystem != null && manaText != null)
        {
            
            
            manaText.text = $"Mana: {manaSystem.CurrentMana:F0}";
        }
    }
}
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectSingleUI : MonoBehaviour
{
    [Serializable]
    public struct StatTypesColors
    {
        public StatTypes statTypes;

        public Color color;

    }

    [Serializable]
    public struct DamageTypesColors
    {
        public DamageTypes damageType;
        public Color color;
    }

    [SerializeField] private List<StatTypesColors> statTypesColors;
    [SerializeField] private List<DamageTypesColors> damageTypesColors;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Image background;

    public void SetUp(StatusEffects statusEffect)
    {
        SetTextUp(statusEffect);
        SetBackgroundUp(statusEffect);
    
    }

    private void SetTextUp(StatusEffects statusEffect)
    {
        textMeshProUGUI.text = statusEffect.GetStatusEffectName();

        switch (statusEffect)
        {
            case BuffDebuffEffects buffDebuffEffect:
                if (buffDebuffEffect.GetIsBuff())
                {
                    textMeshProUGUI.color = Color.white;
                }else
                {
                    textMeshProUGUI.color = Color.black;
                }

                break;
            
            default:

            textMeshProUGUI.color = Color.purple;

            break;
        }
    }

    private void SetBackgroundUp(StatusEffects statusEffect)
    {
        switch (statusEffect)
        {
            case BuffDebuffEffects buffDebuffEffect:
                StatTypes statTypes = buffDebuffEffect.GetStatType();
                background.color = GetBuffDebuffColors(statTypes);

                break;
                
            case DoTEffects doTEffects:
                DamageTypes damageType = doTEffects.GetDamageType();
                background.color = GetDoTColor(damageType);

                break;

            default:
                break;

        }
    }

    private Color GetBuffDebuffColors(StatTypes statType)
    {
        foreach (StatTypesColors item in statTypesColors)
        {
            if (item.statTypes == statType)
            {
                return item.color;
            }
        }
        Debug.LogError("Color missing" + statType);
        return Color.white;
    }

    private Color GetDoTColor(DamageTypes damageType)
    {
        foreach (DamageTypesColors item in damageTypesColors)
        {
            if (item.damageType == damageType)
            {
                return item.color;
            }
        }
        Debug.LogError("Color missing" + damageType);
        return Color.white;
    }
}

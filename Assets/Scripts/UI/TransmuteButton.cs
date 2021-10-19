using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class TransmuteButton : MonoBehaviour
{
    [SerializeField] private TransmuteRuntimeSet negativeTransmutes = default(TransmuteRuntimeSet); 
    [SerializeField] private TransmuteRuntimeSet positiveTransmutes = default(TransmuteRuntimeSet); 
    [SerializeField] private AbilitiesRuntimeSet playerAbilities = default(AbilitiesRuntimeSet); 
    [SerializeField] private PlayerStats playerStats = default(PlayerStats); 
    [SerializeField] private BoolReference turnTransition = default(BoolReference); 
    [SerializeField] private GameEvent transmuteStepClear = default(GameEvent); 
    [SerializeField] private TextMeshProUGUI nameLabel = default(TextMeshProUGUI); 
    [SerializeField] private TextMeshProUGUI firstEffectLabel = default(TextMeshProUGUI); 
    [SerializeField] private TextMeshProUGUI secondEffectLabel = default(TextMeshProUGUI); 
    private bool haveToChangeAbility;
    private bool targetAbilityStatus;
    private int targetAbilityIndex;
    private float firstChangeValue;
    private float secondChangeValue;
    private Global.PenaltyOrImprove firstChangeCategory;
    private Global.PenaltyOrImprove secondChangeCategory;

    public void SetupButton(int index, Global.TransmuteType type)
    {
        TransmuteCard actualCard;
        if (type.Equals(Global.TransmuteType.Positive))
        {
            actualCard = positiveTransmutes[index];
            targetAbilityStatus = true;
        }
        else
        {
            actualCard = negativeTransmutes[index];
            targetAbilityStatus = false;
        }

        nameLabel.text = actualCard.transmuteName;

        if (actualCard.isAbilityChange)
        {
            haveToChangeAbility = true;
            targetAbilityIndex = actualCard.abilityIndex;
            firstEffectLabel.text = $"{playerAbilities[targetAbilityIndex].abilityName}";
            string symbolToPut = (targetAbilityStatus) ? "++" : "--";
            secondEffectLabel.text = $"{playerAbilities[targetAbilityIndex].elementalType}{symbolToPut}";
        }
        else
        {
            haveToChangeAbility = false;
            firstChangeCategory = actualCard.changes[0].penalty;
            firstChangeValue = actualCard.changes[0].percentage;
            secondChangeCategory = actualCard.changes[1].penalty;
            secondChangeValue = actualCard.changes[1].percentage;
            firstEffectLabel.text = $"{firstChangeCategory} {firstChangeValue * 100}";
            secondEffectLabel.text = $"{secondChangeCategory} {secondChangeValue * 100}";
        }
    }

    public void SetupTransmute()
    {
        if (turnTransition.Value) return;
        if (haveToChangeAbility)
        {
            playerAbilities[targetAbilityIndex].abilityActive = targetAbilityStatus;
        }
        else
        {
            SetValue(firstChangeCategory, firstChangeValue);
            SetValue(secondChangeCategory, secondChangeValue);
        }
        print("transmuting");
        transmuteStepClear.Raise();
    }

    private void SetValue(Global.PenaltyOrImprove toChange, float value)
    {
        switch (toChange)
        {
            case Global.PenaltyOrImprove.Life:
                if (value < 0)
                    playerStats.maxLife -= (int)(playerStats.maxLife * Mathf.Abs(value));
                else
                    playerStats.maxLife += (int)(playerStats.maxLife * Mathf.Abs(value));
                break;
            case Global.PenaltyOrImprove.PhysicalAttack:
                if (value < 0)
                    playerStats.physicalAttack -= (int)(playerStats.physicalAttack * Mathf.Abs(value));
                else
                    playerStats.physicalAttack += (int)(playerStats.physicalAttack * Mathf.Abs(value));
                break;
            case Global.PenaltyOrImprove.PhysicalDefense:
                if (value < 0)
                    playerStats.physicalDefense -= (int)(playerStats.physicalDefense * Mathf.Abs(value));
                else
                    playerStats.physicalDefense += (int)(playerStats.physicalDefense * Mathf.Abs(value));
                break;
            case Global.PenaltyOrImprove.AbilityAttack:
                if (value < 0)
                    playerStats.abilityAttack -= (int)(playerStats.abilityAttack * Mathf.Abs(value));
                else
                    playerStats.abilityAttack += (int)(playerStats.abilityAttack * Mathf.Abs(value));
                break;
            case Global.PenaltyOrImprove.AbilityDefense:
                if (value < 0)
                    playerStats.abilityDefense -= (int)(playerStats.abilityDefense * Mathf.Abs(value));
                else
                    playerStats.abilityDefense += (int)(playerStats.abilityDefense * Mathf.Abs(value));
                break;
        }
    }
}

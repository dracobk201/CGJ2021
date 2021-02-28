using UnityEngine;
using TMPro;

public class TransmuteButton : MonoBehaviour
{
    [SerializeField] private TransmuteRuntimeSet negativeTransmutes = null;
    [SerializeField] private TransmuteRuntimeSet positiveTransmutes = null;
    [SerializeField] private AbilitiesRuntimeSet playerAbilities = null;
    [SerializeField] private PlayerStats playerStats = null;
    [SerializeField] private BoolReference turnTransition = null;
    [SerializeField] private GameEvent transmuteStepClear = null;
    [SerializeField] private TextMeshProUGUI nameLabel = null;
    [SerializeField] private TextMeshProUGUI firstEffectLabel = null;
    [SerializeField] private TextMeshProUGUI secondEffectLabel = null;
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
            actualCard = positiveTransmutes.Items[index];
            targetAbilityStatus = true;
        }
        else
        {
            actualCard = negativeTransmutes.Items[index];
            targetAbilityStatus = false;
        }

        nameLabel.text = actualCard.transmuteName;

        if (actualCard.isAbilityChange)
        {
            haveToChangeAbility = true;
            targetAbilityIndex = actualCard.abilityIndex;
            firstEffectLabel.text = $"{playerAbilities.Items[targetAbilityIndex].abilityName}";
            string symbolToPut = (targetAbilityStatus) ? "++" : "--";
            secondEffectLabel.text = $"{playerAbilities.Items[targetAbilityIndex].elementalType}{symbolToPut}";
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
            playerAbilities.Items[targetAbilityIndex].abilityActive = targetAbilityStatus;
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

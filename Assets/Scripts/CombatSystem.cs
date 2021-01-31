using System;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats = null;
    [SerializeField] private EnemyStats enemyStats = null;
    [SerializeField] private IntReference playerLife = null;
    [SerializeField] private IntReference enemyLife = null;
    [SerializeField] private IntReference playerBaseDamage = null;
    [SerializeField] private IntReference enemyBaseDamage = null;
    [SerializeField] private StringReference enemyElementalAttackType = null;
    [SerializeField] private StringReference playerElementalAttackType = null;
    [SerializeField] private GameEvent turnFinished = null;
    private bool playerDefending;
    private bool enemyDefending;

    private void Start()
    {
        playerLife.Value = playerStats.maxLife;
        enemyLife.Value = enemyStats.maxLife;
    }

    public void EnemyAttackDeclared()
    {
        playerLife.Value -= (int) CalculatePhysicalDamage(
            enemyStats.physicalAttack, 
            playerStats.physicalDefense, 
            playerStats.physicalDefenseConstant, 
            playerDefending);
        turnFinished.Raise();
    }

    public void EnemyAbilityDeclared()
    {
        Global.ElementalType elementalType;
        Enum.TryParse(enemyElementalAttackType.Value, out elementalType);
        playerLife.Value -= (int) CalculateAbilityDamage(
            enemyStats.abilityAttack,
            enemyBaseDamage.Value,
            playerStats.abilityDefense, 
            playerStats.abilityDefenseConstant,
            elementalType,
            playerStats.elementalType,
            playerDefending);
        turnFinished.Raise();
    }

    public void PlayerAttackDeclared()
    {
        enemyLife.Value -= (int) CalculatePhysicalDamage(
            playerStats.physicalAttack, 
            enemyStats.physicalDefense, 
            enemyStats.physicalDefenseConstant, 
            enemyDefending);
        turnFinished.Raise();

    }

    public void PlayerAbilityDeclared()
    {
        Global.ElementalType elementalType;
        Enum.TryParse(playerElementalAttackType.Value, out elementalType);
        enemyLife.Value -= (int) CalculateAbilityDamage(
            playerStats.abilityAttack,
            playerBaseDamage.Value,
            enemyStats.abilityDefense, 
            enemyStats.abilityDefenseConstant,
            elementalType,
            enemyStats.elementalType, 
            enemyDefending);
        turnFinished.Raise();
    }

    public void PlayerDefendDeclared()
    {
        playerDefending = true;
        turnFinished.Raise();
    }

    public void EnemyDefendDeclared()
    {
        enemyDefending = true;
        turnFinished.Raise();
    }

    private float CalculatePhysicalDamage(int attack, int defense, float defenseConstant, bool isDefending)
    {
        float blockFactor = (isDefending) ? 0.5f : 1;
        float defenseResistance = defense / (defenseConstant + defense);
        return attack * (1f- defenseResistance) * blockFactor;
    }

    private float CalculateAbilityDamage
        (int attack, int baseDamage, int defense, float defenseConstant, Global.ElementalType attackingType, Global.ElementalType defendingType, bool isDefending)
    {
        float blockFactor = (isDefending) ? 0.5f : 1;
        float elementalFactor = CalculateElementalFactor(attackingType, defendingType);
        float defenseResistance = defense / (defenseConstant + defense);
        return baseDamage + (attack * (1f - defenseResistance) * blockFactor * elementalFactor);
    }

    private float CalculateElementalFactor(Global.ElementalType attackingType, Global.ElementalType defendingType)
    {
        float factor = 1;
        switch (attackingType)
        {
            case Global.ElementalType.Fire:
                if (defendingType.Equals(Global.ElementalType.Fire))
                    factor = 1f;
                else if (defendingType.Equals(Global.ElementalType.Water))
                    factor = 0.5f;
                else if (defendingType.Equals(Global.ElementalType.Thunder))
                    factor = 2f;
                break;
            case Global.ElementalType.Water:
                if (defendingType.Equals(Global.ElementalType.Fire))
                    factor = 2f;
                else if (defendingType.Equals(Global.ElementalType.Water))
                    factor = 1f;
                else if (defendingType.Equals(Global.ElementalType.Thunder))
                    factor = 0.5f;
                break;
            case Global.ElementalType.Thunder:
                if (defendingType.Equals(Global.ElementalType.Fire))
                    factor = 0.5f;
                else if (defendingType.Equals(Global.ElementalType.Water))
                    factor = 2f;
                else if (defendingType.Equals(Global.ElementalType.Thunder))
                    factor = 1f;
                break;
        }
        return factor;
    }
}

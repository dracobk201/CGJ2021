using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AbilitiesRuntimeSet abilities = null; 
    [SerializeField] private EnemyStats stats = null;
    [SerializeField] private PlayerStats playerStats = null;
    [SerializeField] private IntReference actualLife = null;
    [SerializeField] private IntReference baseDamage = null;
    [SerializeField] private IntReference turnsToMutate = null;
    [SerializeField] private StringReference actualAbility = null;
    [SerializeField] private StringReference elementalAttackType = null;
    [SerializeField] private GameEvent enemyAttackDeclared = null;
    [SerializeField] private GameEvent enemyAbilityDeclared = null;
    [SerializeField] private GameEvent turnFinished = null;

    private int _actualTurns;

    private void Start()
    {
        Mutate(false);
    }

    public void Choose()
    {
        _actualTurns--;
        if (_actualTurns <= 0)
        {
            Mutate(true);
            return;
        }

        float attackRandomValue = Random.value;
        if (attackRandomValue < stats.abilityPosibility)
        {
            print("solo ataco");
            enemyAttackDeclared.Raise();
        }
        else
        {
            float abilityRandomValue = Random.value;
            if (abilityRandomValue > 0.8f)
            {
                switch (playerStats.elementalType)
                {
                    case Global.ElementalType.Fire:
                        //SetupAbility(abilities.Items.FindIndex(x => x.elementalType.Equals(Global.ElementalType.Water)));
                        print("ataco agua");
                        SetupAbility(1);
                        break;
                    case Global.ElementalType.Water:
                        //SetupAbility(abilities.Items.FindIndex(x => x.elementalType.Equals(Global.ElementalType.Thunder)));
                        print("ataco trueno");
                        SetupAbility(2);
                        break;
                    case Global.ElementalType.Thunder:
                        //SetupAbility(abilities.Items.FindIndex(x => x.elementalType.Equals(Global.ElementalType.Fire)));
                        print("ataco fuego");
                        SetupAbility(0);
                        break;
                }
                enemyAbilityDeclared.Raise();
            }
            else if (abilityRandomValue < 0.2f)
            {
                print("me curo");
                Heal(0.3f);
                turnFinished.Raise();
            }
            else
            {
                print("hago lo que sea");
                SetupAbility(Random.Range(0, abilities.Items.Count));
                enemyAbilityDeclared.Raise();
            }
        }
        
    }

    private void Heal(float percentage)
    {
        actualAbility.Value = "Heal";
        actualLife.Value += (int) (stats.maxLife * percentage);

    }

    private void Mutate(bool isChooseMethod)
    {
        if (isChooseMethod)
            Heal(0.1f);
        float option = Random.value;
        if (option < 0.4f)
        {
            stats.physicalAttack = 255;
            stats.physicalDefense = 20;
            stats.physicalDefenseConstant = 15;
            stats.abilityAttack = 170;
            stats.abilityDefense = 200;
            stats.abilityDefenseConstant = 10;
            stats.elementalType = Global.ElementalType.Fire;
        }
        else if (option < 0.7f)
        {
            stats.physicalAttack = 158;
            stats.physicalDefense = 10;
            stats.physicalDefenseConstant = 10;
            stats.abilityAttack = 200;
            stats.abilityDefense = 190;
            stats.abilityDefenseConstant = 15;
            stats.elementalType = Global.ElementalType.Water;
        }
        else
        {
            stats.physicalAttack = 190;
            stats.physicalDefense = 200;
            stats.physicalDefenseConstant = 15;
            stats.abilityAttack = 255;
            stats.abilityDefense = 115;
            stats.abilityDefenseConstant = 20;
            stats.elementalType = Global.ElementalType.Thunder;
        }
        actualAbility.Value = "Mutate";
        _actualTurns = turnsToMutate.Value;
        print("Mutate!");
        if (isChooseMethod)
            turnFinished.Raise();
    }

    private void SetupAbility(int index)
    { 
        baseDamage.Value = abilities.Items[index].baseDamage;
        actualAbility.Value = abilities.Items[index].abilityName;
        elementalAttackType.Value = abilities.Items[index].elementalType.ToString();
    }
}

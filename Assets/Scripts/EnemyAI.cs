using ScriptableObjectArchitecture;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AbilitiesRuntimeSet abilities = default(AbilitiesRuntimeSet); 
    [SerializeField] private EnemyStats stats = default(EnemyStats); 
    [SerializeField] private PlayerStats playerStats = default(PlayerStats); 
    [SerializeField] private IntReference actualLife = default(IntReference); 
    [SerializeField] private IntReference baseDamage = default(IntReference); 
    [SerializeField] private IntReference turnsToMutate = default(IntReference); 
    [SerializeField] private StringReference actualAbility = default(StringReference); 
    [SerializeField] private StringReference elementalAttackType = default(StringReference); 
    [SerializeField] private GameEvent enemyAttackDeclared = default(GameEvent); 
    [SerializeField] private GameEvent enemyAbilityDeclared = default(GameEvent); 
    [SerializeField] private GameEvent turnFinished = default(GameEvent); 

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
                SetupAbility(Random.Range(0, abilities.Count));
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
        baseDamage.Value = abilities[index].baseDamage;
        actualAbility.Value = abilities[index].abilityName;
        elementalAttackType.Value = abilities[index].elementalType.ToString();
    }
}

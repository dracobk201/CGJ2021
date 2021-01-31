using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public string playerName;
    public int maxLife;
    public int physicalAttack;
    public int physicalDefense;
    public int physicalDefenseConstant;
    public int abilityAttack;
    public int abilityDefense;
    public int abilityDefenseConstant;
    public Global.ElementalType elementalType;
}

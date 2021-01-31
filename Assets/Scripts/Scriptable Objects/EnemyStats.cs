using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public int maxLife;
    public int physicalAttack;
    public int physicalDefense;
    public float physicalDefenseConstant;
    public int abilityAttack;
    public int abilityDefense;
    public float abilityDefenseConstant;
    public float abilityPosibility;
    public Global.ElementalType elementalType;
}

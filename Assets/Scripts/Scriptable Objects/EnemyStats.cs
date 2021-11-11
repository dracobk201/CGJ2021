using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [BoxGroup("Stats")]
    public string enemyName;
    [BoxGroup("Stats")] [Range(0, 9999)] [GUIColor(0.1f, 0.9f, 0.1f, 1f)] 
    public int maxLife;
    [BoxGroup("Stats/Physical")] [Range(0, 255)] 
    public int physicalAttack;
    [BoxGroup("Stats/Physical")] [Range(0, 255)] 
    public int physicalDefense;
    [BoxGroup("Stats/Physical")] [Range(0, 50)] 
    public float physicalDefenseConstant;
    [BoxGroup("Stats/Ability")] [Range(0, 255)] 
    public int abilityAttack;
    [BoxGroup("Stats/Ability")] [Range(0, 255)] 
    public int abilityDefense;
    [BoxGroup("Stats/Ability")] [Range(0, 50)] 
    public float abilityDefenseConstant;
    [BoxGroup("Stats/Ability")] [Range(0, 1)] 
    public float abilityPosibility;
    [BoxGroup("Stats")] 
    public Global.ElementalType elementalType;
}

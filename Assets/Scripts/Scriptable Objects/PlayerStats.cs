using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [BoxGroup("Stats")] 
    public string playerName;
    [BoxGroup("Stats")] [Range(0,9999)] [GUIColor(0.1f, 0.9f, 0.1f, 1f)]
    public int maxLife;
    [BoxGroup("Stats/Physical")] [Range(0, 255)]
    public int physicalAttack;
    [BoxGroup("Stats/Physical")] [Range(0, 255)]
    public int physicalDefense;
    [BoxGroup("Stats/Physical")] [Range(0, 50)]
    public int physicalDefenseConstant;
    [BoxGroup("Stats/Ability")] [Range(0, 255)]
    public int abilityAttack;
    [BoxGroup("Stats/Ability")] [Range(0, 255)]
    public int abilityDefense;
    [BoxGroup("Stats/Ability")] [Range(0, 50)]
    public int abilityDefenseConstant;
    [BoxGroup("Stats")] 
    public Global.ElementalType elementalType;
}

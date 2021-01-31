using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/New Ability")]
public class Ability : ScriptableObject
{
    public bool abilityActive;
    public string abilityName;
    public int baseDamage;
    public Global.ElementalType elementalType;
    public Global.SideType side;
}

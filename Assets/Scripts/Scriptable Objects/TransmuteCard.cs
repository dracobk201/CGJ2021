using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Transmute Card")]
public class TransmuteCard : ScriptableObject
{
    public string transmuteName;
    public bool isAbilityChange;
    public List<PlayerChange> changes;
    public int abilityIndex;
}

[System.Serializable]
public class PlayerChange
{
    public Global.PenaltyOrImprove penalty;
    public float percentage;
}
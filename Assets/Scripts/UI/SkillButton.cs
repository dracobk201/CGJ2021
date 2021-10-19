using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private IntReference playerBaseDamage = default(IntReference); 
    [SerializeField] private StringReference playerElementalAttackType = default(StringReference); 
    [SerializeField] private StringReference actualAbility = default(StringReference); 
    [SerializeField] private TextMeshProUGUI nameLabel = default(TextMeshProUGUI); 
    [SerializeField] private TextMeshProUGUI effectLabel = default(TextMeshProUGUI); 
    [SerializeField] private GameEvent playerAbilityDeclared = default(GameEvent); 

    private string _actualAbilityName = null;
    private Global.ElementalType _playerElementalAttackType;
    private int _playerBaseDamage;

    public void SetupButton(string name, string effect, Global.ElementalType elementalType, int baseDamage)
    {
        nameLabel.text = name;
        _actualAbilityName = name;
        effectLabel.text = effect;
        _playerElementalAttackType = elementalType;
        _playerBaseDamage = baseDamage;
    }

    public void SetupAbility()
    {
        playerElementalAttackType.Value = _playerElementalAttackType.ToString();
        playerBaseDamage.Value = _playerBaseDamage;
        actualAbility.Value = _actualAbilityName;
        playerAbilityDeclared.Raise();
    }
}

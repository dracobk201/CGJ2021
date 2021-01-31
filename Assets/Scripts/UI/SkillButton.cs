
using UnityEngine;
using TMPro;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private IntReference playerBaseDamage = null;
    [SerializeField] private StringReference playerElementalAttackType = null;
    [SerializeField] private StringReference actualAbility = null;
    [SerializeField] private TextMeshProUGUI nameLabel = null;
    [SerializeField] private TextMeshProUGUI effectLabel = null;
    [SerializeField] private GameEvent playerAbilityDeclared = null;

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

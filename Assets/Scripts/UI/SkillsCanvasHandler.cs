using UnityEngine;

public class SkillsCanvasHandler : MonoBehaviour
{
    [SerializeField] private AbilitiesRuntimeSet playerAbilities = null;
    [SerializeField] private Transform buttonHolder = null;
    [SerializeField] private GameObject buttonPrefab = null;

    private void OnEnable()
    {
        DeleteOldPrefabs();
        InstantiatePrefabs();
    }

    private void DeleteOldPrefabs()
    {
        for (var i = 0; i < buttonHolder.childCount; i++)
            Destroy(buttonHolder.GetChild(i).gameObject);
    }

    private void InstantiatePrefabs()
    {
        foreach (var ability in playerAbilities.Items)
        {
            if (ability.abilityActive)
            {
                var newButton = Instantiate(buttonPrefab, buttonHolder);
                string effect = $"{ability.baseDamage} - {ability.elementalType}";
                newButton.GetComponent<SkillButton>().SetupButton(ability.abilityName, effect, ability.elementalType, ability.baseDamage);
            }
        }
    }
}

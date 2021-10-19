using UnityEngine;

public class SkillsCanvasHandler : MonoBehaviour
{
    [SerializeField] private AbilitiesRuntimeSet playerAbilities = default(AbilitiesRuntimeSet);
    [SerializeField] private Transform buttonHolder = default(Transform);
    [SerializeField] private GameObject buttonPrefab = default(GameObject);

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
        foreach (var ability in playerAbilities)
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

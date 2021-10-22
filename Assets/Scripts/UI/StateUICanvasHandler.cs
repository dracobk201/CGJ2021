using UnityEngine;
using TMPro;

public class StateUICanvasHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionLabel = default(TextMeshProUGUI);

    public void ShowActionMessage(string message)
    {
        actionLabel.text = message;
    }

    public void CleaningActionLabel()
    {
        actionLabel.text = string.Empty;
    }
}

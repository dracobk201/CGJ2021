using UnityEngine;
using TMPro;

public class StateUICanvasHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionLabel = default(TextMeshProUGUI);
    [SerializeField] private Animator panelAnimator = default(Animator);

    public void ShowActionMessage(string message)
    {
        actionLabel.text = message;
    }

    public void CleaningActionLabel()
    {
        actionLabel.text = string.Empty;
    }

    public void TriggerIntroAnimation ()
    {
        panelAnimator.SetTrigger("Intro");
    }
}

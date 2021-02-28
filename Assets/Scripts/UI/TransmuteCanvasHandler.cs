using UnityEngine;

public class TransmuteCanvasHandler : MonoBehaviour
{
    [SerializeField] private GameEvent turnFinished = null;
    [SerializeField] private Transform negativeHolder = null;
    [SerializeField] private Transform positiveHolder = null;
    [SerializeField] private GameObject backButton = null;
    [SerializeField] private GameObject buttonPrefab = null;
    private int actualPhase;
     
    private void OnEnable()
    {
        DeleteOldPrefabs();
        InstantiatePrefabs();
        actualPhase = 0;
    }

    private void DeleteOldPrefabs()
    {
        for (var i = 0; i < negativeHolder.childCount; i++)
            Destroy(negativeHolder.GetChild(i).gameObject);
        for (var i = 0; i < positiveHolder.childCount; i++)
            Destroy(positiveHolder.GetChild(i).gameObject);
    }

    private void InstantiatePrefabs()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 8);
            var newButton = Instantiate(buttonPrefab, negativeHolder);
            newButton.GetComponent<TransmuteButton>().SetupButton(index, Global.TransmuteType.Negative);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 8);
            var newButton = Instantiate(buttonPrefab, positiveHolder);
            newButton.GetComponent<TransmuteButton>().SetupButton(index, Global.TransmuteType.Positive);
        }
    }

    public void CheckTransmuteStep()
    {
        switch (actualPhase)
        {
            case 1:
                ShowDiscoverPanel();
                backButton.SetActive(false);
                actualPhase = 2;
                break;
            case 2:
                turnFinished.Raise();
                break;
        }
    }

    public void ShowPurgePanel()
    {
        negativeHolder.gameObject.SetActive(true);
        positiveHolder.gameObject.SetActive(false);
        backButton.SetActive(true);
        actualPhase = 1;
    }

    private void ShowDiscoverPanel()
    {
        negativeHolder.gameObject.SetActive(false);
        positiveHolder.gameObject.SetActive(true);
    }
}

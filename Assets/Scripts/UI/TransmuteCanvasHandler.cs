using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmuteCanvasHandler : MonoBehaviour
{
    [SerializeField] private TransmuteRuntimeSet negativeTransmutes = null;
    [SerializeField] private TransmuteRuntimeSet positiveTransmutes = null;
    [SerializeField] private Transform negativeHolder = null;
    [SerializeField] private Transform positiveHolder = null;
    [SerializeField] private GameObject buttonPrefab = null;
     
    private void OnEnable()
    {
        DeleteOldPrefabs();
        InstantiatePrefabs();
    }

    private void DeleteOldPrefabs()
    {
        for (var i = 0; i < negativeHolder.childCount; i++)
            Destroy(negativeHolder.GetChild(i).gameObject);
    }

    private void InstantiatePrefabs()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 8);
            var newButton = Instantiate(buttonPrefab, negativeHolder);
            newButton.GetComponent<TransmuteButton>().SetupButton();
        }
    }
}

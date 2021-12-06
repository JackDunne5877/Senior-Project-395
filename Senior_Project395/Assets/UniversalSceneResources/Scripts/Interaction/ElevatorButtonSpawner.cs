using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButtonSpawner : MonoBehaviour
{
    public GameObject elevatorButtonPrefab;
    public UnlockButton elevatorUnlockListener;
    public bool debugging = true;

    void Start()
    {
        int chosenButtonIndex = Random.Range(0, gameObject.transform.childCount);
        int loopCounter = 0;

        foreach(Transform buttonOption in gameObject.transform)
        {
            if(loopCounter == chosenButtonIndex)
            {
                //we've chosen this button
                buttonOption.gameObject.SetActive(true);
                elevatorUnlockListener.InteractableButton = buttonOption.gameObject;

                if (debugging)
                {
                    Debug.Log($"chose button at {buttonOption.position}");
                }
            }
            else
            {
                buttonOption.gameObject.SetActive(false);
            }
            loopCounter++;
        }

        
    }
}

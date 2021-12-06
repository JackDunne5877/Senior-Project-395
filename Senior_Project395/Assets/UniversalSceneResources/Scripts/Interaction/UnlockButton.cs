using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour
{
    public GameObject interactableButton;
    public GameObject doorLockObj;
    public List<LockedDoor> doors;
    private Interactable_new interactable;
    public bool doorTouched = false;
    public bool doorOpen = false;
    public float countdownReset;
    private float countdown;

    public GameObject InteractableButton { 
        get => interactableButton;
        set { 
            interactableButton = value;
            interactable = interactableButton.GetComponent<Interactable_new>();
            doorLockObj.SetActive(true);
            Debug.Log("set the Elevator's interactable button");
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (interactable != null && interactable.isBeingInteracted)
        {
            //assign the variable locked in LockedDoor script to be false, allowing it to be triggered to open
            foreach (LockedDoor lockedDoor in doors)
            {
                lockedDoor.Locked = false;
            }
            //deactivate the lock on the doors
            if(doorLockObj != null)
            {
                doorLockObj.SetActive(false);
            }
            Debug.Log("unlocked doors");
        }

    }
}

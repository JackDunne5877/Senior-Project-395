using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour
{
    public GameObject objWithInteractable;
    private Interactable_new interactable;
    public bool doorTouched = false;
    public bool doorOpen = false;
    public float countdownReset;
    private float countdown;
    // Start is called before the first frame update
    void Start()
    {
        interactable = objWithInteractable.GetComponent<Interactable_new>();
    }

    // Update is called once per frame
    void Update()
    {

        if (interactable.isBeingInteracted)
        {
            //assign the variable locked in LockedDoor script to be false, allowing it to be triggered to open
            GameObject.FindGameObjectWithTag("LockedDoor").GetComponent<LockedDoor>().Locked = false;
            Debug.Log("unlock button set lockeddoor locked to false");
        }

    }
}

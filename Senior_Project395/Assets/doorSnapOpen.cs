using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSnapOpen : MonoBehaviour
{
    private Interactable interactable;
    public bool doorOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponentInChildren<Interactable>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!doorOpened && interactable.isBeingInteracted)
        {
            doorOpened = true;
        }
        else if(doorOpened && interactable.isBeingInteracted)
        {
            doorOpened = false;
        }

        if (doorOpened)
        {
            this.transform.localRotation *= Quaternion.Euler(0, 90, 0);
        }
        else
        {
            this.transform.localRotation *= Quaternion.Euler(0, -90, 0);
        }

    }
}

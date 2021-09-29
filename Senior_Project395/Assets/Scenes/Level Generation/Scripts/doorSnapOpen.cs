using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSnapOpen : MonoBehaviour
{
    public GameObject objWithInteractable;
    private Interactable interactable;
    public bool doorTouched = false;
    public bool doorOpen = false;
    public float countdownReset;
    private float countdown;
    // Start is called before the first frame update
    void Start()
    {
        interactable = objWithInteractable.GetComponentInChildren<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!doorTouched && interactable.isBeingInteracted)
        {
            doorTouched = true;
            Debug.Log("Door has been touched");
            if (doorOpen)
            {
                Debug.Log("Tried to close");
                //close
                this.transform.localRotation *= Quaternion.Euler(0, 0, 90);
            }
            else
            {
                Debug.Log("Tried to open");
                //open
                this.transform.localRotation *= Quaternion.Euler(0, 0, -90);
            }
            interactable.isBeingInteracted = false;
            doorOpen = !doorOpen;
        }

        if(doorTouched && countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = countdownReset;
            doorTouched = false;
        }
    }
}

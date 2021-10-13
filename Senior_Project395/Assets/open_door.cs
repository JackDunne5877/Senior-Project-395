using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_door : MonoBehaviour
{
    public GameObject door;
    public Interactable interactable;
    public bool pressed = false;
    private float countdownReset = 5.0f;
    public float countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = countdownReset;
        interactable = GetComponentInChildren<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressed && interactable.isBeingInteracted) {
            pressed = true;
            door.SetActive(false);
        }

        if(pressed && countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else if(pressed && countdown < 0)
        {
            countdown = countdownReset;
            pressed = false;
            door.SetActive(true);
        }
    }
}

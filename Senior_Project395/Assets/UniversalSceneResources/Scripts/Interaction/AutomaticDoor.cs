using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{

    public Vector3 doorEndPosition;
    public float doorSpeed = 1.0f;

    private bool moving = false;
    private bool opening = true;
    private bool closing = false;

    private Vector3 doorStartPosition;
    private float delay = 0.0f;
    private float delay2 = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        doorStartPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if(moving)
        {
            if (opening)
            {
                //Debug.Log("Moving doors to open position");
                MoveDoor(doorEndPosition);

            }

            if (closing)
            {
                //Debug.Log("Moving the doors to og position");
                MoveDoor(doorStartPosition);
            }
        }
    }

    void MoveDoor(Vector3 goalPosition)
    {
        float distance = Vector3.Distance(transform.localPosition, goalPosition);

        //open door at rate of doorSpeed
        if (distance > .1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, goalPosition, doorSpeed * Time.deltaTime);
            
        }

        //when done moving
        else
        {
            moving = false;
            //if door is opening
            
            if (opening)
            {
                //closing = false;
                delay += Time.deltaTime;

               
                opening = false;
                closing = true;

                
            }

            else if (closing)
            {
                Debug.Log("Done closing");
                opening = true;
                closing = false;   
            }

        }

        
    }

    public bool Moving
    {
        get { return moving; }
        set { moving = value; }
    }

    public bool Opening
    {
        get { return opening; }
        set { opening = value; }
    }

    public bool Closing
    {
        get { return closing; }
        set { closing = value; }
    }
}

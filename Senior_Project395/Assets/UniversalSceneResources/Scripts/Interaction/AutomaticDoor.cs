using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{

    public Vector3 doorEndPosition;
    public float doorSpeed = 1.0f;

    private bool moving = false;
    private bool opening = true;

    private Vector3 doorStartPosition;
    private float delay = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        doorStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (opening)
            {
                MoveDoor(doorEndPosition);
            }
            else
            {
                MoveDoor(doorStartPosition);
            }
        }
    }

    void MoveDoor(Vector3 goalPosition)
    {
        float distance = Vector3.Distance(transform.position, goalPosition);

        //open door at rate of doorSpeed
        if(distance > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, goalPosition, doorSpeed * Time.deltaTime);
        }

        //when done opening
        else
        {
            //if door is opening, hold open for 1.5 seconds
            if (opening)
            {
                delay += Time.deltaTime;

                if(delay > 1.5f)
                {
                    opening = false;
                }
            }

            //closing door
            else
            {
                moving = false;
                opening = true;
            }
        }
    }

    public bool Moving
    {
        get { return moving; }
        set { moving = value; }
    }
}

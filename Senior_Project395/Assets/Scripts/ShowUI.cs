using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public static GameObject uiObject;
    public static GameObject uiObjectDesc;
    public static bool showTxt;
    public static bool showDesc;

    // Start is called before the first frame update
    void Start()
    {
        showTxt = false;
        showDesc = false;
        uiObject = GameObject.Find("InteractionTextBckgrnd");
        uiObjectDesc = GameObject.Find("InteractionDescBckgrnd");
        //Debug.Log("set object to var" + uiObjectChild);
        uiObject.SetActive(false);
        uiObjectDesc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (showDesc)
        {
            uiObjectChild.SetActive(true);
            //set parent false while this set active
            Debug.Log("should be showing txt here forehead");
        }
        uiObjectChild.SetActive(false); */
    }

    //method ran in interactable when player is in range of object, setting the UI object active ("press f to inspect")
    public static void InteractChecker()
    {
        if (showTxt)
        {
            uiObject.SetActive(true);
            Debug.Log("Actually fucking got here wow");

        }
        else {
            uiObject.SetActive(false);

        }
    }

    //method to set object description to show up
    public static void descriptionChecker()
    {
        if (showDesc)
        {
            //disable uiObject and enable description object
            //uiObject.SetActive(false);
            showTxt = false;
            Debug.Log("Ur really close mate");
            uiObjectDesc.SetActive(true);
        }
        uiObjectDesc.SetActive(false);
        uiObject.SetActive(true);
    }
}

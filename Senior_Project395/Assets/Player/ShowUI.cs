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
        uiObject.SetActive(false);
        uiObjectDesc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //method ran in interactable when player is in range of object, setting the UI object active ("press f to inspect")
    public static void InteractChecker()
    {
        if (showTxt)
        {
            uiObject.SetActive(true);
        }
        else
        {
            uiObject.SetActive(false);
            uiObjectDesc.SetActive(false);

        }
    }

    //method to set object description to show up
    public static void descriptionChecker()
    {
        if (showDesc)
        {
            showTxt = false;
            uiObjectDesc.SetActive(true);
            uiObject.SetActive(false);
        }
    }
}

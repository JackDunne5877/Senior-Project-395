using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public static GameObject uiObject;
    //public static GameObject uiObjectChild;
    public static bool showTxt;
    //public static bool showDesc;

    // Start is called before the first frame update
    void Start()
    {
        showTxt = false;
        //showDesc = false;
        uiObject = GameObject.Find("InteractionTextBckgrnd");
        //uiObjectChild = GameObject.Find("DescriptionBckgrnd");
        //Debug.Log("set object to var" + uiObjectChild);
        uiObject.SetActive(false);
        //uiObjectChild.SetActive(false);
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
}

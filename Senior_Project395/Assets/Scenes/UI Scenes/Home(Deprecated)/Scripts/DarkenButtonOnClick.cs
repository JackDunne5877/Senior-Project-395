using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The purpose of this script is to change the color of a button while it is selected
/// </summary>
public class DarkenButtonOnClick : MonoBehaviour
{
    private Button button;


    void Start()
    {
        // Set the button
        button = this.gameObject.GetComponent<Button>();

        // Set the button's behavior
        button.onClick.AddListener(DarkenButton);
    }


    // Darken the selected button and lighten all other buttons
    void DarkenButton()
    {
        // Set the parent gameobject
        GameObject parentObj = button.transform.parent.gameObject;

        // Make all buttons light
        for (int i = 0; i < parentObj.transform.childCount; i++)
        {
            GameObject siblingButton = parentObj.transform.GetChild(i).gameObject;
            Image siblingButtonImage = siblingButton.GetComponent<Image>();
            siblingButtonImage.color = new Color32(191, 180, 182, 255);
        }

        // Make this particular button dark
        GameObject buttonAsObj = this.gameObject;
        Image thisButtonImage = buttonAsObj.GetComponent<Image>();
        thisButtonImage.color = new Color32(161, 150, 152, 225);
    }
}

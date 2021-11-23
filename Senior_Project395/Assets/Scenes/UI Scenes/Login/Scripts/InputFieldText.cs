using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldText : MonoBehaviour
{
    public GameObject defaultText; // The name of the input field
    public GameObject inputText; // The current text input by the user


    void Update()
    {
        // Make the default text disappear if the input field is selected or contains text
        if (EventSystem.current.currentSelectedGameObject == this.gameObject || inputText.GetComponent<Text>().text != "")
        {
            defaultText.SetActive(false);
        }

        // Make the default text reappear if the input field isn't selected and is empty
        if (EventSystem.current.currentSelectedGameObject != this.gameObject && inputText.GetComponent<Text>().text == "")
        {
            defaultText.SetActive(true);
        }
    }
}

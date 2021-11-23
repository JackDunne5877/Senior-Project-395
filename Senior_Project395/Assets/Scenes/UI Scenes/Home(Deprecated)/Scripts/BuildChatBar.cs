using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
///  The purpose of this script is to build the messages menu UI that scrolls down when the Messages button is selected
/// </summary>
public class BuildChatBar : MonoBehaviour
{
    public GameObject buttonPrefab; // A button to represent each ongoing conversation
    
    /* THIS WILL CHANGE once there is database access */
    public string[] profiles; // A list of profiles that users are conversing with

    private GameObject parentCanvas; // The canvas that all buttons will be placed in
    private RectTransform canvasRT; // parentCanvas' RectTransform
    private float chatBarHeight; // The height of the total chat bar after all buttons have been added
    private float buttonHeight; // The height of a single button
    private bool drop; // Boolean that toggles if parentCanvas is active
    private Vector3 upPos; // The position of chat bar when it's retracted
    private Vector3 downPos; // The position of chat bar when it's dropped down
    
    // Variables that affect the speed of the chat bar dropdown
    private float acc;
    private float currSpeed;
    private Vector3 endPos;


    void Start()
    {
        // Set the parent and its Rect Transform
        parentCanvas = this.gameObject;
        parentCanvas.SetActive(false);
        canvasRT = parentCanvas.GetComponent<RectTransform>();

        // Set the two possible positions of the chat bar
        upPos = new Vector3(parentCanvas.transform.position.x, parentCanvas.transform.position.y, 0);
        downPos = new Vector3(parentCanvas.transform.position.x, parentCanvas.transform.position.y - chatBarHeight - buttonHeight, 0);

        buildChatBar();
    }


    // Build the chat menu
    void buildChatBar()
    {
        // The position to instantiate the button at
        float yPos = 0;

        // The height of the button
        buttonHeight = canvasRT.rect.height;

        // Instantiate the buttons
        for (int i = 0; i < profiles.Length; i++)
        {
            // Calculate the position of the new button
            if (i != 0)
            {
                yPos += buttonHeight;
            }

            // Instantiate the new button
            GameObject newButtonInstance = Instantiate(buttonPrefab);
            RectTransform buttonInstanceRT = newButtonInstance.GetComponent<RectTransform>();
            buttonInstanceRT.SetParent(canvasRT);
            buttonInstanceRT.sizeDelta = Vector2.one;
            buttonInstanceRT.localScale = Vector2.one;
            buttonInstanceRT.localPosition = Vector2.zero;
            buttonInstanceRT.localPosition = new Vector2(0, yPos);

            // Set the button name
            setName(newButtonInstance, i);
        }

        // Set the height of the chat bar
        chatBarHeight = yPos;
    }


    // Set the name associated with each profile on the chat button
    void setName(GameObject buttonInstance, int num)
    {
        // Get the text component
        GameObject tmp = buttonInstance.transform.Find("Text (TMP)").gameObject;

        // Change the text
        tmp.GetComponent<TextMeshProUGUI>().text = profiles[num];
    }


    // Drop the chat menu when Messages is clicked
    // Called in ButtonBehavior.cs
    public void dropChatBar()
    {
        // Reset the position of the chat bar (in case a new button is pushed mid-animation)
        parentCanvas.transform.position = upPos;

        // Set important variables
        acc = -100;
        currSpeed = 2000;
        endPos = new Vector3(parentCanvas.transform.position.x, parentCanvas.transform.position.y - chatBarHeight - buttonHeight, 0);
        drop = true;

        // Make the buttons visible
        parentCanvas.SetActive(drop);

        // Move the chat menu down
        StartCoroutine(moveChatBar());
    }


    // Retract the chat menu when Connect is clicked
    // Called in ButtonBehavior.cs
    public void retractChatBar()
    {
        // Reset the position of the chat bar (in case a new button is pushed mid-animation)
        parentCanvas.transform.position = downPos;

        // Set important variables
        acc = -100;
        currSpeed = 2000;
        endPos = new Vector3(parentCanvas.transform.position.x, parentCanvas.transform.position.y + chatBarHeight + buttonHeight, 0);
        drop = false;

        // Move the chat menu down
        StartCoroutine(moveChatBar());
    }


    // Move the chat menu up or down
    IEnumerator moveChatBar()
    {
        while (parentCanvas.transform.position != endPos)
        {
            // Continually decrease the speed
            if (currSpeed > 0)
            {
                currSpeed += acc * Time.deltaTime;
            }

            parentCanvas.transform.position = Vector3.MoveTowards(parentCanvas.transform.position, endPos, currSpeed * Time.deltaTime);

            yield return null;
        }

        // Make the chat bar inactive when messages isn't selected
        parentCanvas.SetActive(drop);
    }


    // Create a scroll bar when the number of chats exceeds
    void createScrollBar()
    {

    }
}

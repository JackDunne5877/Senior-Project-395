using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonBehavior : MonoBehaviour
{
    public Button connectButton;
    public Button messagesButton;
    public BuildChatBar chatBarCanvas;

    // Images that make the buttons darker when selected
    private Image connectButtonSelected;
    private Image messagesButtonSelected;

    // Text displayed within each button
    private TextMeshProUGUI connectText;
    private TextMeshProUGUI messagesText;


    void Start()
    {
        // Set up the buttons
        connectButton.onClick.AddListener(SwitchToConnect);
        messagesButton.onClick.AddListener(SwitchToMessages);

        // Set up the images
        connectButtonSelected = connectButton.GetComponent<Image>();
        messagesButtonSelected = messagesButton.GetComponent<Image>();

        // Set up the text
        connectText = connectButton.GetComponentInChildren<TextMeshProUGUI>();
        messagesText = messagesButton.GetComponentInChildren<TextMeshProUGUI>();

        // Connect is selected by default
        connectButtonSelected.enabled = true;
        SwitchToConnect();
    }


    // Switch from the Connect tab to the Messages tab
    void SwitchToMessages()
    {
        // Deselect the Connect tab
        connectButtonSelected.enabled = false;
        connectText.color = new Color32(111, 111, 111, 255);

        // Disable the Connect tab displays
        Canvas gamesCanvas = GameObject.Find("Games Display Canvas").GetComponent<Canvas>();
        gamesCanvas.enabled = false;
        Canvas connectTextCanvas = GameObject.Find("Connect Text Canvas").GetComponent<Canvas>();
        connectTextCanvas.enabled = false;

        // Show the chat drop down
        if (!messagesButtonSelected.enabled)
        {
            chatBarCanvas.dropChatBar();
        }

        // Select the Messages tab
        messagesButtonSelected.enabled = true;
        messagesText.color = new Color32(85, 85, 85, 225);

        // Enable the Messages tab displays
        Canvas messagesTextCanvas = GameObject.Find("Messages Text Canvas").GetComponent<Canvas>();
        messagesTextCanvas.enabled = true;
    }


    // Switch from the Messages tab to the Connect tab
    void SwitchToConnect()
    {
        // Deselect the Messages tab
        messagesButtonSelected.enabled = false;
        messagesText.color = new Color32(111, 111, 111, 255);

        // Disable the Messages tab displays
        Canvas messagesTextCanvas = GameObject.Find("Messages Text Canvas").GetComponent<Canvas>();
        messagesTextCanvas.enabled = false;

        // Hide the chat drop down
        if (!connectButtonSelected.enabled)
        {
            chatBarCanvas.retractChatBar();
        }

        // Select the Connect tab
        connectButtonSelected.enabled = true;
        connectText.color = new Color32(85, 85, 85, 255);

        // Enable the Connect tab displays
        Canvas gamesCanvas = GameObject.Find("Games Display Canvas").GetComponent<Canvas>();
        gamesCanvas.enabled = true;
        Canvas connectTextCanvas = GameObject.Find("Connect Text Canvas").GetComponent<Canvas>();
        connectTextCanvas.enabled = true;
    }
}

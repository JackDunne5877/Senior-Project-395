using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonBehavior : MonoBehaviour
{
    public Button connectButton;
    public Button messagesButton;

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
        SwitchToConnect();
    }


    // Switch from the Connect tab to the Messages tab
    void SwitchToMessages()
    {
        // Deselect the Connect tab
        connectButtonSelected.enabled = false;
        connectText.color = new Color32(111, 111, 111, 255);

        // Select the Messages tab
        messagesButtonSelected.enabled = true;
        messagesText.color = new Color32(85, 85, 85, 225);
    }


    // Switch from the Messages tab to the Connect tab
    void SwitchToConnect()
    {
        // Deselect the Messages tab
        messagesButtonSelected.enabled = false;
        messagesText.color = new Color32(111, 111, 111, 255);

        // Select the Connect tab
        connectButtonSelected.enabled = true;
        connectText.color = new Color32(85, 85, 85, 255);
    }
}

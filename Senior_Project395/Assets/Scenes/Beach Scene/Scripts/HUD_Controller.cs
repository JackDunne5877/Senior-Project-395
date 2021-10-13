using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Orion.MP;

public class HUD_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PointsTextObj;
    public GameObject HealthBarObj;
    public GameObject AmmoTextObj;

    public Color ammoCountColor;
    public Color ammoEmptyColor;


    private PlayerNetworkManager PlayerNetManager;
    private Text pointsText;
    private Text ammoText;
    private int Points = 0;

    public GameObject interactPromptObj;
    public GameObject interactionDescriptionObj;
    public bool showInteractPrompt = false;
    public bool showInteractionDesc = false;

    void Start()
    {
        PlayerNetManager = GetComponentInParent<PlayerNetworkManager>();
        pointsText = PointsTextObj.GetComponent<Text>();
        ammoText = AmmoTextObj.GetComponent<Text>();

        interactPromptObj.SetActive(false);
        interactionDescriptionObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int pts)
    {
        Debug.Log("Adding points");
        Points += pts;
        pointsText.text = Points.ToString();
    }

    public void updateAmmoCount(int ammo)
    {
        Debug.Log("Updating Ammo Count");
        ammoText.text = ammo.ToString();
        if(ammo == 0)
        {
            ammoText.color = ammoEmptyColor;
        }
        else
        {
            ammoText.color = ammoCountColor;
        }
    }

    public void CheckInteractPromptVisibility()
    {
        if (showInteractPrompt)
        {
            interactPromptObj.SetActive(true);
        }
        else
        {
            interactPromptObj.SetActive(false);
            interactionDescriptionObj.SetActive(false);
        }
    }

    //method to set object description to show up
    public void CheckDescriptionTextVisibility()
    {
        if (showInteractionDesc)
        {
            showInteractPrompt = false;
            interactionDescriptionObj.SetActive(true);
            interactPromptObj.SetActive(false);
        }
    }
}

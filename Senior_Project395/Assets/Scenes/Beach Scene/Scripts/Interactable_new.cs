using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactable_new : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public bool isBeingInteracted = false;

    //Testing out dynamic text box to change description for each object
    public string objectDescription;
    public GameObject playerInRange;
    private HUD_Controller playerInRangeHUD;

    private void Update()
    {
        //if a player is in range to interact, and interact key is pressed
        if (playerInRange != null && playerInRange.GetComponent<StarterAssetsInputs>().interact)
        {
            if (!isBeingInteracted)
            {
                isBeingInteracted = true;
                playerInRangeHUD.interactionDescriptionObj.GetComponentInChildren<Text>().text = objectDescription;
                playerInRangeHUD.showInteractionDesc = true;
                playerInRangeHUD.CheckDescriptionTextVisibility();
            }
        }
        
    }

    //when entering/colliding with 3d physics sphere
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = other.gameObject;
            playerInRangeHUD = playerInRange.GetComponent<HUD_Controller>();
            playerInRangeHUD.showInteractPrompt = true;
            playerInRangeHUD.CheckInteractPromptVisibility();
        }
    }

    private void StopInteractingWithPlayer(GameObject player)
    {
        HUD_Controller HUD = player.GetComponent<HUD_Controller>();
        HUD.showInteractPrompt = false;
        HUD.CheckInteractPromptVisibility();
        HUD.showInteractionDesc = false;
        HUD.CheckDescriptionTextVisibility();
        HUD.interactionDescriptionObj.GetComponentInChildren<Text>().text = "";
        if(playerInRange == player)
        {
            isBeingInteracted = false;
            playerInRange = null;
        }
    }

    //when exiting range of 3d physics sphere
    private void OnTriggerExit(Collider other)
    {
        StopInteractingWithPlayer(other.gameObject);
    }
}

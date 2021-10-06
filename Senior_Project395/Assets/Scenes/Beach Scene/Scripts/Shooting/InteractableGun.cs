using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class InteractableGun : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public static bool isInRange;
    public bool isBeingInteracted = false;
    public bool isInRangeOfThisObject = false;
    public bool isEquipped = false;
    public GameObject gunObj;
    private GameObject player;

    private Interactable_new interactable;

    private void Start()
    {
        interactable = this.GetComponent<Interactable_new>();
        gunObj.SetActive(true);
    }


    private void Update()
    {
        //if we interact
        if (interactable.isBeingInteracted)
        {
            player = interactable.playerInRange;
            //add gun to inventory
            Debug.Log("adding weapon to inventory");
            player.GetComponentInChildren<GunsMenu>().addWeapon(gunObj);
            isEquipped = false;

            //tell interactable it is being held so triggers and text no longer work
            interactable.ConvertToHeldItem();
        }

    }
}

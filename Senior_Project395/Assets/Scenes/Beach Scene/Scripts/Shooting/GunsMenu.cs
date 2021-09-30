using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunsMenu : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject[] Guns;
    int currentGun = 0;

    void Start()
    {
        //Don't know if this does anything, not sure what Buttons does
        Buttons = new GameObject();

        //disable all other guns that aren't the first weapon
        for(int i = 0; i < Guns.Length; i++)
        {
            if(i > 0)
            {
                Guns[i].SetActive(false);
                Guns[i].GetComponentInChildren<InteractableGun>().isEquipped = true;
            }
        }

        //equip the first weapon
        Guns[0].SetActive(true);
    }

    //cycle through to next weapon in Guns[]
    public void NextGun()
    {
        Guns[currentGun].SetActive(false);
        currentGun++;
        if (currentGun >= Guns.Length)
            currentGun = 0;
        Guns[currentGun].SetActive(true);
    }

    //cycle through to previous weapon in Guns[]
    public void PreviousGun()
    {
        Guns[currentGun].SetActive(false);
        currentGun--;
        if (currentGun < 0)
            currentGun = Guns.Length - 1;
        Guns[currentGun].SetActive(true);
    }


    //used when picking up a weapon or item, adds weapon to Guns[]
    public void addWeapon(GameObject weapon)
    {
        int tmpLength = Guns.Length + 1;
        Array.Resize(ref Guns, tmpLength);
        Guns[tmpLength - 1] = weapon;
    }


    //function triggered when switching between weapons
    public void switchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NextGun();
        }
    }


    private void Update()
    {
        switchWeapon();
        if ((Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            Buttons.SetActive(false);
        }
        else if(Input.touchCount == 0 && !Input.GetMouseButton(0))
        {
            Buttons.SetActive(true);
        }
    }
}

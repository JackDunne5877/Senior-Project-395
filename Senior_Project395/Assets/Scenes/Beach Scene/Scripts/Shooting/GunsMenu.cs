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
    public KeyCode switchGun;

    void Start()
    {
        //Guns = new GameObject[4];
        Buttons = new GameObject();

        //disable all other guns that aren't the first weapon
        for(int i = 0; i < Guns.Length; i++)
        {
            if(i > 0)
            {
                Guns[i].SetActive(false);
            }
        }

        //equip the first weapon
        Guns[0].SetActive(true);
    }

    public void NextGun()
    {
        Guns[currentGun].SetActive(false);
        currentGun++;
        if (currentGun >= Guns.Length)
            currentGun = 0;
        Guns[currentGun].SetActive(true);
    }
    public void PreviousGun()
    {
        Guns[currentGun].SetActive(false);
        currentGun--;
        if (currentGun < 0)
            currentGun = Guns.Length - 1;
        Guns[currentGun].SetActive(true);
    }


    //used when picking up a weapon or item
    public void addWeapon(GameObject weapon)
    {
        for(int i = 0; i < Guns.Length; i++)
        {
            if(Guns[i] == null)
            {
                //add new gun to end of guns list
                Guns.SetValue(weapon, i);
            }
        }
    }


    //function triggered when switching between weapons
    public void switchWeapon()
    {
        /*
        if (StarterAssetsInputs.switchWeapon)
        {
            Debug.Log("Switching to another weapon!");
            NextGun();
        } */

        //check for input & make sure there is a next gun available
        if (Input.GetKeyDown(KeyCode.Alpha2) /*&& Guns[currentGun++] != null */)
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

using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;



public class GunsMenu : MonoBehaviourPun
{
    public GameObject playerRoot;
    public List<GameObject> Guns = new List<GameObject>();
    public Camera myfpsCam;
    int currentGun = 0;

    private PhotonView pv;

    void Start()
    {

        pv = PhotonView.Get(this);
        foreach (Transform child in transform)
        {
            Guns.Add(child.gameObject);
        }

        
        //disable all other guns that aren't the first weapon
        for (int i = 0; i < Guns.Count; i++)
        {
            if(i > 0)
            {
                Guns[i].SetActive(false);
                Guns[i].GetComponentInChildren<InteractableGun>().isEquipped = false;
                Guns[i].GetComponent<Com.Orion.MP.RaycastShootComplete>().fpsCam = myfpsCam;
            }
        }

        //equip the first weapon
        Guns[0].SetActive(true);
        Guns[0].GetComponentInChildren<InteractableGun>().isEquipped = true;
        Guns[0].GetComponent<Com.Orion.MP.RaycastShootComplete>().fpsCam = myfpsCam;
    }

    //cycle through to next weapon in Guns[]
    public void NextGun()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Guns[currentGun].SetActive(false);
        Guns[currentGun].GetComponentInChildren<InteractableGun>().isEquipped = false;
        currentGun++;
        if (currentGun >= Guns.Count)
            currentGun = 0;
        Guns[currentGun].SetActive(true);
        Guns[currentGun].GetComponentInChildren<InteractableGun>().isEquipped = true;
    }

    //cycle through to previous weapon in Guns[]
    public void PreviousGun()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Guns[currentGun].SetActive(false);
        currentGun--;
        if (currentGun < 0)
            currentGun = Guns.Count - 1;
        Guns[currentGun].SetActive(true);
    }


    //used when picking up a weapon or item, adds weapon to Guns[]
    public void addWeapon(GameObject weapon)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //int tmpLength = Guns.Count + 1;
        //Array.Resize(ref Guns, tmpLength);
        //Guns[tmpLength - 1] = weapon;
        Guns.Add(weapon);

        //instantiate currentGun as child of WeaponHolder, and reset positioning
        weapon.transform.parent = gameObject.transform;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.transform.localPosition = Vector3.zero;
        weapon.SetActive(false);
        weapon.GetComponent<Com.Orion.MP.RaycastShootComplete>().fpsCam = myfpsCam;
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

        foreach(Transform child in transform){
            Camera childFpsCam = child.gameObject.GetComponent<Com.Orion.MP.RaycastShootComplete>().fpsCam;
            if (childFpsCam == null)
            {
                childFpsCam = myfpsCam;
            }
        }
    }
}

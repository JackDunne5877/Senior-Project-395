using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //list of items in inventory
    public GameObject[] items;


    private void Start()
    {
        InitVariables();
    }


    //function to initialize inventory variables
    private void InitVariables()
    {
        items = new GameObject[4];
    }
}

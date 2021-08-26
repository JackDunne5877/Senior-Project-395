using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    //boolean to determine if an item is in interactable range of player
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
}

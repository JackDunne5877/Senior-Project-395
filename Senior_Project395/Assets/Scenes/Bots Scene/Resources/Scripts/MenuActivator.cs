using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    //private bool isPaused = false;
    private bool toggle = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustCursorState();

        if (!toggle && Input.GetButtonDown("Cancel")) {
            toggle = true;
            menu.SetActive(!menu.activeInHierarchy);
            //FlipCursorState();

            Debug.Log("Paused");
        }
        if (Input.GetButtonUp("Cancel")) {
            toggle = false;
        }
        
    }

    void AdjustCursorState() {
        if (menu.activeInHierarchy) { 
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

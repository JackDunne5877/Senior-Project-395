using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    //private bool isPaused = false;
    private bool toggle = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!toggle && Input.GetButtonDown("Cancel")) {
            toggle = true;
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            FlipCursorState();

            Debug.Log("Paused");
        }
        if (Input.GetButtonUp("Cancel")) {
            toggle = false;
        }
        
    }

    void FlipCursorState() {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }
}

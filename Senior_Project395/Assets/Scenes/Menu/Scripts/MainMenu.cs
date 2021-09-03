using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void QuitGame()
    {
        Debug.Log("quitting...");
        Application.Quit();
    }
}

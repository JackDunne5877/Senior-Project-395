using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneZero : MonoBehaviour
{
    //this script will just play the menu scene if singleton manager is not set up
    public bool bypass = false;
    void Awake()
    {
        if (SingletonManager.Instance == null && !bypass)
        {
            SceneManager.LoadScene("Home");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dating_Platform;


/* The purpose of SingletonManager is to manage singletons, which are similar to public static variables, except 
 * they are accessible from the inspector.
 * 
 * Initialize some value "value" in this script. Reference that value from other scripts with this line:
 * SingletonManager.Instance.value;
 */
public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance { get; private set; }
    
    //Player things:
    public User Player { 
        get => _player;
        set {
            _player = value;
            Debug.Log("Singleton Player Set");
        }
    }

    public string viewingConnectionPlayerId;

    public GameLibrary gameLibrary;

    public int maxPlayerHealth = 5;
    public float roundTime = 0;
    private User _player;


    //Profile Constants:
    public const string PROFILE_CONST_HOST_GENDER = "gender";
    public const string PROFILE_CONST_GENDER_PREF = "genderpref";
    public enum GenderOption { Male, Female, NonBinary };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameLibrary = this.GetComponent<GameLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

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
    public Player Player { 
        get => _player;
        set {
            _player = value;
            Debug.Log("Singleton Player Set");
        }
    }

    public int maxPlayerHealth = 5;
    public float roundTime = 0;
    private Player _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

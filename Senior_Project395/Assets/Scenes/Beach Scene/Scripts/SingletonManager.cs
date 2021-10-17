using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* The purpose of SingletonManager is to manage singletons, which are similar to public static variables, except 
 * they are accessible from the inspector.
 * 
 * Initialize some value "value" in this script. Reference that value from other scripts with this line:
 * SingletonManager.Instance.value;
 */
public class SingletonManager : MonoBehaviour
{

    public static SingletonManager Instance { get; private set; }

    public int maxPlayerHealth;


    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform { 

public class UserLogoutViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogOutClicked()
    {
        DatabaseConnection.logoutClicked();
        //TODO take player back to login page
    }
}
}
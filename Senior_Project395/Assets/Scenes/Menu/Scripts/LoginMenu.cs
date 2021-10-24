using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    // error messages to user
    public GameObject invalidCredentials;

    // Start is called before the first frame update
    void Start()
    {
        // store all values form input field
        var username_field = transform.Find("InputField (1)").GetComponent<InputField>();
        var password_field = transform.Find("InputField (2)").GetComponent<InputField>();

        // on button press check inputs and create new user
        transform.Find("loginBtn").GetComponent<Button>().onClick.AddListener(
            () => {
                // reset error messages to be hidden
                invalidCredentials.SetActive(false);


                string username = username_field.text;
                string password = password_field.text;

                User.HttpResponse response = User.loginUser(username, password);

                if (response.statusCode >= 400)
                {
                    // let user know that input credentials were invalid
                    invalidCredentials.SetActive(true);
                }
            }
        );
    }

    // Update is called once per frame
    void Update()
    {

    }
}

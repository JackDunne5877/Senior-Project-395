using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUserMenu : MonoBehaviour
{
    // error messages to user
    public GameObject invalidUsername;
    public GameObject invalidDate;

    // Start is called before the first frame update
    void Start()
    {
        // store all values form input field
        var username_field = transform.Find("InputField (1)").GetComponent<InputField>();
        var password_field = transform.Find("InputField (2)").GetComponent<InputField>();
        var first_name_field = transform.Find("InputField (3)").GetComponent<InputField>();
        var last_name_field = transform.Find("InputField (4)").GetComponent<InputField>();
        var month_field = transform.Find("Dropdown").GetComponent<Dropdown>();
        var day_field = transform.Find("InputField (5)").GetComponent<InputField>();
        var year_field = transform.Find("InputField (6)").GetComponent<InputField>();

        // on button press check inputs and create new user
        transform.Find("createUserBtn").GetComponent<Button>().onClick.AddListener(
            () => {
                // reset error messages to be hidden
                invalidUsername.SetActive(false);
                invalidDate.SetActive(false);

                string username = username_field.text;
                string password = password_field.text;
                string first_name = first_name_field.text;
                string last_name = last_name_field.text;
                int month = month_field.value + 1;
                string day = day_field.text;
                string year = year_field.text;
                // make birthday from inputs
                string birthday = System.String.Format("{0}-{1}-{2}",year,month,day);

                // let user know that date input was invalid
                if (!isDate(birthday))
                {
                    invalidDate.SetActive(true);
                }
                else
                {
                    // check response from server for valid username input
                    User.HttpResponse response = User.createUser(username, password, first_name, last_name, birthday);
                    if (response.statusCode >= 400)
                    {
                        invalidUsername.SetActive(true);
                    }
                }
            }
        );
    }

    // checking if input is valid date
    bool isDate(string date)
    {
        // creating DateTime as required input for TryParse
        System.DateTime x = new System.DateTime();
        return System.DateTime.TryParse(date, out x);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

//LoginMenu.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Dating_Platform
{
    public class LoginMenu : MonoBehaviour
    {
        // error messages to user
        public Text usernameValidMsg;
        public Text usernameTxt;
        public Text pwValidMsg;
        public Text pwTxt;
        public Button SubmitBtn;

        // Start is called before the first frame update
        void Start()
        {
            usernameValidMsg.text = "";
            pwValidMsg.text = "";
            SubmitBtn.interactable = false;
        }

        bool unValid;

        public void checkUsernameValidity(string newUsername)
        {
            if (newUsername == "")
            {
                usernameValidMsg.text = "";
                unValid = false;
                SubmitBtn.interactable = false;
                return;
            }

            Debug.Log("checking username validity");
            //check
            unValid = Validators.isUsernameValid(newUsername);

            //show ok or error
            usernameValidMsg.text = $"username {(unValid ? "valid" : "invalid")}";
            if (unValid)
            {
                //enable change btn if valid
                usernameValidMsg.color = Validators.validColor;

                if(pwValid)
                    SubmitBtn.interactable = true;
            }
            else
            {
                usernameValidMsg.color = Validators.invalidColor;
                SubmitBtn.interactable = false;
            }

        }

        bool pwValid;

        public void checkPasswordValidity(string newPassword)
        {
            if (newPassword == "")
            {
                pwValidMsg.text = "";
                pwValid = false;
                SubmitBtn.interactable = false;
                return;
            }

            Debug.Log("checking password validity");
            //check
            pwValid = Validators.isPasswordValid(newPassword);

            //show ok or error
            pwValidMsg.text = $"password {(pwValid ? "valid" : "invalid")}";
            if (pwValid)
            {
                //enable change btn if valid
                pwValidMsg.color = Validators.validColor;

                if (unValid)
                    SubmitBtn.interactable = true;
            }
            else
            {
                pwValidMsg.color = Validators.invalidColor;
                SubmitBtn.interactable = false;
            }
        }

        public void submitLoginInfo()
        {
            string un = usernameTxt.text;
            string pw = pwTxt.text;
            (bool result, int statuscode, string responseMsg) = DatabaseConnection.login(un,pw);
            if (result)
            {
                SceneManager.LoadScene("Menu");
            }
            else 
            {
                //stay here and wipe everything
                //TODO could change msg based on retrieved msg^
                usernameValidMsg.text = "Login failed: username or password invalid";
                usernameValidMsg.color = Validators.invalidColor;
                pwValidMsg.text = "";
                usernameTxt.text = "";
                pwTxt.text = "";
                SubmitBtn.interactable = false;
            }

        }
    }
}
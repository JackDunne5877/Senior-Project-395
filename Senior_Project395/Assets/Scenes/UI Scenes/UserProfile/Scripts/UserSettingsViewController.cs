using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dating_Platform
{
    public class UserSettingsViewController : MonoBehaviour
    {
        public Button changeEmailBtn;
        public Button changePasswordBtn;
        public Text emailValidMsg;
        public Text newEmailTxt;
        public Text pwValidMsg;
        public Text newPwTxt;
        public Color invalidColor;
        public Color validColor;
        public GameObject disableAccountToggleContainer;
        public List<Toggle> disableAccountToggles = new List<Toggle>();

        // Start is called before the first frame update
        void Start()
        {
            disableAccountToggleContainer.SetActive(false);
            changeEmailBtn.interactable = false;
            changePasswordBtn.interactable = false;
            emailValidMsg.text = "";
            pwValidMsg.text = "";
            foreach(Transform child in disableAccountToggleContainer.transform)
            {
                disableAccountToggles.Add(child.GetComponent<Toggle>());
                disableAccountToggles[disableAccountToggles.Count - 1].isOn = false;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void checkNewEmailValidity(string newEmail)
        {
            if (newEmail == "")
            {
                emailValidMsg.text = "";
                changeEmailBtn.interactable = false;
                return;
            }

            Debug.Log("checking email validity");
            //check
            bool emValid = newEmail.Contains("@");//TODO check validity
                                                  //show ok or error
            emailValidMsg.text = $"new email address {(emValid ? "valid" : "invalid")}";
            if (emValid)
            {
                //enable change btn if valid
                emailValidMsg.color = validColor;
                changeEmailBtn.interactable = true;
            }
            else
            {
                emailValidMsg.color = invalidColor;
                changeEmailBtn.interactable = false;
            }

        }

        public void checkNewPasswordValidity(string newPassword)
        {
            if (newPassword == "")
            {
                pwValidMsg.text = "";
                changePasswordBtn.interactable = false;
                return;
            }

            Debug.Log("checking password validity");
            //check
            bool pwValid = newPassword.Length > 4;//TODO check validity
                                                  //show ok or error
            pwValidMsg.text = $"new password {(pwValid ? "valid" : "invalid")}";
            if (pwValid)
            {
                //enable change btn if valid
                pwValidMsg.color = validColor;
                changePasswordBtn.interactable = true;
            }
            else
            {
                pwValidMsg.color = invalidColor;
                changePasswordBtn.interactable = false;
            }
        }

        public void setNewEmail()
        {
            string newValidEmail = newEmailTxt.text;
            DatabaseConnection.setNewAccountEmail(SingletonManager.Instance.Player.PlayerID, "12345", newValidEmail); //TODO get player pw
        }

        public void setNetPassword()
        {
            string newValidPassword = newPwTxt.text;
            DatabaseConnection.setNewAccountPassword(SingletonManager.Instance.Player.PlayerID, "12345", newValidPassword); //TODO get player pw
        }

        public void disableAccountClicked()
        {
            disableAccountToggleContainer.gameObject.SetActive(true);
            foreach (Toggle t in disableAccountToggles)
            {
                if (!t.isOn)
                {
                    t.isOn = true;
                    return;
                }
            }
            //all were on
            bool success = DatabaseConnection.disableAccount(SingletonManager.Instance.Player.PlayerID, "12345");//TODO get player pw
            if (success)
            {
                //TODO show that we've been signed out and return to login page
            }
        }
    }
}
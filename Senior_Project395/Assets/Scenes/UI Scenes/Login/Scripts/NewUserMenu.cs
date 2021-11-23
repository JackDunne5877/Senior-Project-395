//NewUserMenu.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Dating_Platform
{
    public class NewUserMenu : MonoBehaviour
    {
        public Text usernameValidMsg;
        public Text usernameTxt;

        public Text pwValidMsg;
        public Text pwTxt;

        public Text FNameTxt;
        public Text LNameTxt;
        public Text nameValidMsg;

        public Text BDDayText;
        public Text BDMonthText;
        public Text BDYearText;
        public Text BDValidMsg;

        public Button SubmitBtn;

        public GameObject ConfirmationView;

        public void Start()
        {
            pwValidMsg.text = "";
            usernameValidMsg.text = "";
            nameValidMsg.text = "";
            BDValidMsg.text = "";
        }


        bool _unValid = false;
        public bool UnValid
        {
            get => _unValid; set
            {
                _unValid = value;
                checkSubmitBtnAvailability();
            }
        }

        public void checkUsernameValidity(string newUsername)
        {
            if (newUsername == "")
            {
                usernameValidMsg.text = "";
                UnValid = false;
                return;
            }

            //check
            UnValid = Validators.isUsernameValid(newUsername);

            //show ok or error
            usernameValidMsg.text = $"username {(UnValid ? "valid" : "invalid")}";
            if (UnValid)
            {
                //enable change btn if valid
                usernameValidMsg.color = Validators.validColor;

            }
            else
            {
                usernameValidMsg.color = Validators.invalidColor;
            }

        }

        bool _pwValid = false;
        public bool PwValid { get => _pwValid; set
            {
                _pwValid = value;
                checkSubmitBtnAvailability();
            }
        }

        public void checkPasswordValidity(string newPassword)
        {
            if (newPassword == "")
            {
                pwValidMsg.text = "";
                PwValid = false;
                return;
            }

            //check
            PwValid = Validators.isPasswordValid(newPassword);

            //show ok or error
            pwValidMsg.text = $"password {(PwValid ? "valid" : "invalid")}";
            if (_pwValid)
            {
                //enable change btn if valid
                pwValidMsg.color = Validators.validColor;

            }
            else
            {
                pwValidMsg.color = Validators.invalidColor;
            }
        }

        bool _bdValid = false;

        public bool BDValid { 
            get => _bdValid; 
            set { _bdValid = value;
                checkSubmitBtnAvailability();
            } 
        }

        public void checkSubmitBtnAvailability()
        {
            SubmitBtn.interactable = (PwValid && UnValid && BDValid && NameValid);
        }

        string BDParsed;

        public void checkBDValidity(string input)
        {
            if (BDDayText.text == "" &&
                BDMonthText.text == "" &&
                BDYearText.text == "")
            {
                BDValidMsg.text = "";
                BDValid= false;
                return;
            }

            //check
            BDParsed = System.String.Format("{0}-{1}-{2}", BDYearText.text, BDMonthText.text, BDDayText.text);
            BDValid = Validators.isDateValid(BDParsed);

            //show ok or error
            BDValidMsg.text = $"birthdate {(BDValid ? "valid" : "invalid")}";
            if (BDValid)
            {
                //enable change btn if valid
                BDValidMsg.color = Validators.validColor;
            }
            else
            {
                BDValidMsg.color = Validators.invalidColor;
            }
        }


        bool _nameValid = false;

        public bool NameValid
        {
            get => _nameValid;
            set
            {
                _nameValid = value;
                checkSubmitBtnAvailability();
            }
        }

        public void checkNameValidity(string input)
        {
            //if any field has no input yet
            if (FNameTxt.text == "" ||
                LNameTxt.text == "")
            {
                nameValidMsg.text = "";
                NameValid = false;
                return;
            }

            //check
            NameValid = Validators.isNameValid(FNameTxt.text, LNameTxt.text);

            //show ok or error
            nameValidMsg.text = $"name {(NameValid ? "valid" : "invalid")}";
            if (NameValid)
            {
                //enable change btn if valid
                nameValidMsg.color = Validators.validColor;
            }
            else
            {
                nameValidMsg.color = Validators.invalidColor;
            }
        }

        public void submitNewAccountInfo()
        {
            string un = usernameTxt.text;
            string pw = pwTxt.text;
            string fn = FNameTxt.text;
            string ln = LNameTxt.text;
            string bd = BDParsed;


            (bool result, int statusCode, string reason) = DatabaseConnection.createNewUser(un, pw,fn,ln,bd);

            if (result)
            {
                ConfirmationView.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {
                //stay here and wipe everything
                Debug.LogWarning($"Account creation failed: {statusCode}: {reason}");
                usernameValidMsg.text = $"Account creation failed: {reason.Substring(0,50)}";
                usernameTxt.text = "";
                usernameValidMsg.color = Validators.invalidColor;
            }

        }


    }
}
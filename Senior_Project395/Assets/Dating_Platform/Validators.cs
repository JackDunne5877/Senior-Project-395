using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform { 
    public static class Validators
    {
        public static Color invalidColor = new Color(178,0,0,255);
        public static Color validColor= new Color(0,140,36,255);
        public static bool isPasswordValid(string password)
        {
            return password.Length > 4;//TODO check validity
        }

        public static bool isUsernameValid(string username)
        {
            return username.Length>5;//TODO check validity
        }

        public static bool isDateValid(string date)
        {
            // creating DateTime as required input for TryParse
            System.DateTime x = new System.DateTime();
            return System.DateTime.TryParse(date, out x);
        }

        public static bool isNameValid(string fName, string lName)
        {
            return (fName.Length > 2 && lName.Length > 2);
        }
    }
}
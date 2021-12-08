using UnityEngine;


namespace Dating_Platform
{
    //public enum GenderOption { Male, Female, NonBinary};
    //public enum GenderPrefsCombos { M, F, N, MFN, MN, MF, FN};

    public class User
    {
        //TODO probably makes sense to make most of these API calls just so you always have up-to-date info
        public string PlayerID;
        public string DisplayName;
        public Sprite ProfileImg;
        public Sprite[] PlayerImages;
        public SingletonManager.GenderOption genderIdentity;
        public SingletonManager.GenderOption[] genderPreferences;
        public int age;
        public string bio;
        public string[] connectionIds;

        // used to make user objects to convert to JSON
        public class NewUser
        {
            public string username { get; set; }
            public string password { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string birthday { get; set; }

            public NewUser(string username, string password, string first_name, string last_name, string birthday)
            {
                this.username = username;
                this.password = password;
                this.first_name = first_name;
                this.last_name = last_name;
                this.birthday = birthday;
            }
        }
        // used to make login user objects to convert to JSON
        public class LoginUser
        {
            public string username { get; set; }
            public string password { get; set; }

            public LoginUser(string username, string password)
            {
                this.username = username;
                this.password = password;
            }
        }
        // used to make new email objects to convert to JSON
        public class NewEmail
        {
            public string password { get; set; }
            public string newEmail { get; set; }

            public NewEmail(string password, string newEmail)
            {
                this.password = password;
                this.newEmail = newEmail;
            }
        }

        // used to make objects to convert to JSON with password and ID
        public class ConfirmPlayerPassword
        {
            public string password { get; set; }

            public ConfirmPlayerPassword(string password)
            {
                this.password = password;
            }
        }
    }
}


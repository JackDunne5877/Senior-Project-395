using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    //public enum GenderOption { Male, Female, NonBinary};
    //public enum GenderPrefsCombos { M, F, N, MFN, MN, MF, FN};
    
    public class Player
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


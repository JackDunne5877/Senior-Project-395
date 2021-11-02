using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    public enum GenderOption { Male, Female, NonBinary};
    
    public class Player : MonoBehaviour
    {
        public string PlayerID;
        public string DisplayName;
        public Sprite ProfileImg;
        public Sprite[] PlayerImages;
        public GenderOption genderIdentity;
        public GenderOption[] genderPreferences;
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


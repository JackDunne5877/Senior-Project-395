using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform {
    public class TestPlayerLoading : MonoBehaviour
    {
        public Sprite sampleProfilePic;
        public Sprite[] samplePlayerImages;
        // Start is called before the first frame update
        void Start()
        {
            SingletonManager.Instance.player = new Player()
            {
                age = 18,
                bio = "This is a sample player bio",
                DisplayName = "SamplePlayerDisplayName",
                PlayerID = "abc",
                ProfileImg = sampleProfilePic,
                genderIdentity = GenderOption.Male,
                genderPreferences = new GenderOption[] { GenderOption.Male, GenderOption.Female, GenderOption.NonBinary },
                connectionIds = new string[] { "xyz" },
                PlayerImages = samplePlayerImages,
            };
            Debug.Log("tried to give singleton a player");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

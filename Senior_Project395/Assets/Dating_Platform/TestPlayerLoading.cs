using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform {
    public class TestPlayerLoading : MonoBehaviour
    {
        
        // Start is called before the first frame update
        public int _age = 18;
        public string _bio = "This is a sample player bio";
        public string _DisplayName = "SamplePlayerDisplayName";
        public string _PlayerID = "abc";
        public Sprite _sampleProfilePic;
        public SingletonManager.GenderOption _genderIdentity = SingletonManager.GenderOption.Female;
        public SingletonManager.GenderOption[] _genderPreferences = new SingletonManager.GenderOption[] { SingletonManager.GenderOption.Male, SingletonManager.GenderOption.Female, SingletonManager.GenderOption.NonBinary};
        public string[] _connectionIds = new string[] { "conn1", "conn2", "conn3"};
        public Sprite[] _samplePlayerImages;
        void Start()
        {
            SingletonManager.Instance.Player = new User()
            {
                age = _age,
                bio = _bio,
                DisplayName = _DisplayName,
                PlayerID = _PlayerID,
                ProfileImg = _sampleProfilePic,
                genderIdentity = _genderIdentity,
                genderPreferences = _genderPreferences,
                connectionIds = _connectionIds,
                PlayerImages = _samplePlayerImages,
            };
            Debug.Log("tried to give singleton a player");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

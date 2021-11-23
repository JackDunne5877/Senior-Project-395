using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    public class SampleConnections : MonoBehaviour
    {
        // Start is called before the first frame update
        public List<Sprite> sampleImages;
        public List<User> sampleUsers;
        void Start()
        {
            sampleUsers = new List<User>() {
                new User(){
                    DisplayName = "sampleConnection1",
                    ProfileImg = sampleImages[0],
                    PlayerImages = sampleImages.ToArray(),
                    age = 45,
                    bio = "I am a sample player 1",
                    genderIdentity = SingletonManager.GenderOption.Male,
                    genderPreferences = new SingletonManager.GenderOption[] { SingletonManager.GenderOption.Male, SingletonManager.GenderOption.NonBinary },
                    PlayerID = "conn1",
                },
                new User(){
                    DisplayName = "sampleConnection2",
                    ProfileImg = sampleImages[1],
                    PlayerImages = sampleImages.ToArray(),
                    age = 21,
                    bio = "I am a sample player 2",
                    genderIdentity = SingletonManager.GenderOption.Female,
                    genderPreferences = new SingletonManager.GenderOption[] {SingletonManager.GenderOption.NonBinary },
                    PlayerID = "conn2",
                },
                new User(){
                    DisplayName = "sampleConnection3",
                    ProfileImg = sampleImages[2],
                    PlayerImages = sampleImages.ToArray(),
                    age = 33,
                    bio = "I am a sample player 3",
                    genderIdentity = SingletonManager.GenderOption.NonBinary,
                    genderPreferences = new SingletonManager.GenderOption[] { SingletonManager.GenderOption.Male, SingletonManager.GenderOption.Female},
                    PlayerID = "conn3",
                },
        };

            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Dating_Platform
{
    public class ConnectionItem : MonoBehaviour
    {
        Sprite profileImg;
        string displayName;
        string playerId;
        public GameObject profileImageObj;
        public Text displayNameTextObj;
        public Button msgButton;
        public Button inviteBtn;
        public Player _player;

        public Player player
        {
            get { return _player; }
            set { _player = value;
                populatePlayerInfo();
            }
        }


        void populatePlayerInfo()
        {
            profileImg = player.ProfileImg;
            displayName = player.DisplayName;
            playerId = player.PlayerID;
            profileImageObj.GetComponent<Image>().sprite = profileImg;
            displayNameTextObj.text = displayName;
            //msgButton.onClick = Home>ConnectionSelected>Conversation(PlayerId=playerId)
            //inviteBtn.onClick = Home>ConnectionSelected>InviteToMinigame(PlayerId=playerId)
        }

        public void openConnectionProfile()
        {
            SingletonManager.Instance.viewingConnectionPlayerId = playerId;
            SceneManager.LoadScene("ConnectionProfile");
        }

    }
}

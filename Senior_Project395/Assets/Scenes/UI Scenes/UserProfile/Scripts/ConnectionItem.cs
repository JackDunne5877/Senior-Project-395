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
        public RectTransform InnerContainerRect;
        string displayName;
        string playerId;
        public GameObject profileImageObj;
        public Text displayNameTextObj;
        public Button chatBtn;
        public Button inviteBtn;
        public User _player;

        public User player
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
            InnerContainerRect.offsetMin = Vector2.zero;
            InnerContainerRect.offsetMax = Vector2.zero;
            displayNameTextObj.text = displayName;
            //msgButton.onClick = Home>ConnectionSelected>Conversation(PlayerId=playerId)
            //inviteBtn.onClick = Home>ConnectionSelected>InviteToMinigame(PlayerId=playerId)
        }

        public void openConnectionProfile()
        {
            SingletonManager.Instance.viewingConnectionPlayer = player;
            SceneManager.LoadScene("ConnectionProfile");
        }

        public void chatClicked()
        {
            SingletonManager.Instance.viewingConnectionPlayer = player;
            if(SceneManager.GetActiveScene().name != "Home")
                SceneManager.LoadScene("Home");
        }

        public void inviteClicked()
        {
            SingletonManager.Instance.viewingConnectionPlayer = player;
            if (SceneManager.GetActiveScene().name != "Home")
                SceneManager.LoadScene("Home");
        }

    }
}

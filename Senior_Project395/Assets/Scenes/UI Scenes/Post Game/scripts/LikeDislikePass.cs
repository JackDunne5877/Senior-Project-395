using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dating_Platform
{
    public class LikeDislikePass : MonoBehaviour
    {
        public GameObject otherPlayerProfileImgObj;
        public Text otherPlayerName;
        // Start is called before the first frame update
        void Start()
        {

            if (SingletonManager.Instance.playingWithOtherPlayer == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                otherPlayerProfileImgObj.GetComponent<Image>().sprite = SingletonManager.Instance.playingWithOtherPlayer.ProfileImg;
                otherPlayerName.text = SingletonManager.Instance.playingWithOtherPlayer.DisplayName;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LikePlayer()
        {
            DatabaseConnection.likePlayer(SingletonManager.Instance.playingWithOtherPlayer);
        }

        public void DislikePlayer()
        {
            DatabaseConnection.dislikePlayer(SingletonManager.Instance.playingWithOtherPlayer);
        }

        public void PassOnPlayer()
        {
            //TODO just show button selected and leave as is
        }

        public void ReportPlayer()
        {
            DatabaseConnection.reportPlayer(SingletonManager.Instance.playingWithOtherPlayer);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    public class LikeDislikePass : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

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
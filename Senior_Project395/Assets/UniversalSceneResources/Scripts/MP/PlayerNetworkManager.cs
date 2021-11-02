using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;

namespace Com.Orion.MP
{
    public class PlayerNetworkManager : MonoBehaviourPunCallbacks//, IPunObservable
    {

        /*
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                //stream.SendNext(IsFiring);
                //stream.SendNext(Health);
            }
            else
            {
                // Network player, receive data
                //this.IsFiring = (bool)stream.ReceiveNext();
               // this.Health = (float)stream.ReceiveNext();
            }
        }*/

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;


        [Tooltip("Camera so that only clients camera is activated")]
        [SerializeField]
        private GameObject cam;

        void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerNetworkManager.LocalPlayerInstance = this.gameObject;
                SetCameraActive(true);
            }
            else {
                SetCameraActive(false);
            }

            DontDestroyOnLoad(this.gameObject);   
        }

        void SetCameraActive(bool state)
        {
            cam.GetComponent<Camera>().enabled = state;
            cam.GetComponent<AudioListener>().enabled = state;
            cam.GetComponent<Cinemachine.CinemachineBrain>().enabled = state;
            cam.transform.Find("Canvas").gameObject.SetActive(state);
        }

        void Update()
        {
        
        }
    }
}
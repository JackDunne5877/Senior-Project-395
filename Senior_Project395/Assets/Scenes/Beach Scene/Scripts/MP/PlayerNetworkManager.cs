using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Voice;
using System.Collections;

namespace Com.Orion.MP
{
    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerNetworkManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region IPunObservable implementation


        //this allows us to communicate with clients and let them know we shot something
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            /*
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(IsFiring);
            }
            else
            {
                // Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
            }
            */
        }


        #endregion

        #region Public Fields
        //[Tooltip("The current Health of our player")]
        //public float Health = 1f;
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        #endregion

        #region Private Fields

        [Tooltip("Camera so that only clients camera is activated")]
        [SerializeField]
        private GameObject cam;

        public bool IsFiring;

        [SerializeField]
        private GameObject voiceNetwork;

        #endregion

        #region MonoBehaviour CallBacks



        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerNetworkManager.LocalPlayerInstance = this.gameObject;
                SetCameraActive(true);

                if (PhotonNetwork.IsMasterClient)
                {
                    voiceNetwork.SetActive(true);
                }
                else {
                    voiceNetwork.SetActive(false);
                }
            }   
            else {
                SetCameraActive(false);
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
            
        }
        void Update()
        {
            if (photonView.IsMine)
            {
                //    photonView.RPC("Shoot", RpcTarget.All);
                
            }
        }

        #endregion

        #region Custom

        void SetCameraActive(bool state) {
            cam.GetComponent<Camera>().enabled = state;
            cam.GetComponent<AudioListener>().enabled = state;
            cam.GetComponent<Cinemachine.CinemachineBrain>().enabled = state;
            cam.transform.Find("Canvas").gameObject.SetActive(state);
        }


        /// <summary>
        /// Processes the inputs. Maintain a flag representing when the user is pressing Fire.
        /// </summary>
        void ProcessInputs()
        {
            /*
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }*/
        }

        #endregion
    }
}
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;

namespace Com.Orion.MP
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        //[Tooltip("The prefab to use for setting up voice chat")]
        //[SerializeField]
        //private GameObject voicePrefab;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        public static NetworkManager Instance;

        [Tooltip("Spawn point and angle for player")]
        [SerializeField]
        private Transform spawnPointObj;

        void Start()
        {

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerNetworkManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    //PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPointObj.position, Quaternion.Euler(spawnPointObj.localRotation), 0);
                    PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPointObj.position, spawnPointObj.localRotation, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
            /*
            //delete any extraneous voice prefabs before joining
            if (!PhotonNetwork.IsMasterClient) {
                GameObject[] voicePrefabs = GameObject.FindGameObjectsWithTag("Voice");
                foreach (var v in voicePrefabs) {
                    GameObject.Destroy(v);
                }
            }
            */
            Instance = this;
        }
        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            //PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel(SceneManagerHelper.ActiveSceneName);
        }

        #endregion

        #region Photon Callbacks
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
            SingletonManager.Instance.roundTime = 0;

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                                                                                                         //LoadArena();

            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
            SingletonManager.Instance.roundTime = 0;
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Home");
        }
        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            //indicate that the player is no longer in a game,
            //deactivate current game controller
            SingletonManager.Instance.gameObject.SendMessage($"deactivate{SingletonManager.Instance.currentPlayingGame.GameControllerType}", SendMessageOptions.DontRequireReceiver);
            //store that we aren't in any game
            SingletonManager.Instance.currentPlayingGame = null;
        }

        #endregion
    }
}
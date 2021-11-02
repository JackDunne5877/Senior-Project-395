using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Com.Orion.MP
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public const string MAP_PROP_KEY = "map";

        #region Private Serializable Fields


        //[Tooltip("The level that loads when play is clicked")]
        //[SerializeField]
        private int levelIndex = 1;

        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        [Tooltip("The UI Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;

        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        #endregion


        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";
        bool isConnecting;

        #endregion


        #region MonoBehaviour CallBacks

        void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;

        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        #endregion

        #region Private Methods

        //in the future, modify this function to add more room options, 
        //example, if user is male, create a room that accepts females only, etc...
        //or set settings of room to have info: ex: (is male, into females, age min 18, age max 25)
        //then filter rooms in "JoinRandomRoom" to be within those parameters
        private Hashtable CreateRoomOptionsHashTable() {
            return new Hashtable { { MAP_PROP_KEY, (byte)levelIndex } };
        }
        private void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.CustomRoomPropertiesForLobby = new string[1]{ MAP_PROP_KEY };
            roomOptions.CustomRoomProperties = CreateRoomOptionsHashTable();
            roomOptions.MaxPlayers = maxPlayersPerRoom;
            PhotonNetwork.CreateRoom(null, roomOptions, null);
        }

        private void JoinRandomRoom()
        {
            //right now this is set up to match the create room, but can be easily changed to be the opposite, or to match 
            //the info on available room
            Hashtable expectedCustomRoomProperties = CreateRoomOptionsHashTable();
            PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayersPerRoom);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect(int level)
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            levelIndex = level;
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.IsConnected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                //PhotonNetwork.JoinRandomRoom();
                JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }
        #endregion

        #region MonoBehaviourPunCallbacks Callbacks
        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
                // PhotonNetwork.JoinRandomRoom();
                JoinRandomRoom();
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            if (progressLabel != null && controlPanel != null)
            {
                progressLabel.SetActive(false);
                controlPanel.SetActive(true);
            }
            Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            CreateRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("Load level index: " + levelIndex);

                // #Critical
                // Load the Level.
                PhotonNetwork.LoadLevel(levelIndex);
            }
        }

        #endregion
    }
}
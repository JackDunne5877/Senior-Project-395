using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    public class ConnectionsViewController : MonoBehaviour
    {
        private Player player;
        public GameObject connectionsList;
        public GameObject connectionItemPrefab;
        // Start is called before the first frame update
        void Start()
        {
            player = SingletonManager.Instance.Player;
            foreach(string connectionId in player.connectionIds)
            {
                Player connectionPlayer = DatabaseConnection.getConnectionPlayerInfo(player, "12345", connectionId);
                GameObject connectionItem = GameObject.Instantiate(connectionItemPrefab);
                connectionItem.GetComponent<ConnectionItem>().player = connectionPlayer;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

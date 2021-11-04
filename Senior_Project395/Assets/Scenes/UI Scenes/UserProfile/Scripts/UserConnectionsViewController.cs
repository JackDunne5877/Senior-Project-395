using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    public class UserConnectionsViewController : MonoBehaviour
    {
        Player player;
        public GameObject connectionsList;
        public GameObject connectionItemPrefab;
        // Start is called before the first frame update
        void Start()
        {
            player = SingletonManager.Instance.Player;
            foreach(string connectionId in player.connectionIds)
            {
                Debug.Log($"trying to get data for player {connectionId}");
                Player connectionPlayer = DatabaseConnection.getConnectionPlayerInfo(player, "12345", connectionId);
                GameObject connectionItem = Instantiate(connectionItemPrefab);
                connectionItem.transform.SetParent(connectionsList.transform);
                connectionItem.transform.localScale = Vector3.one;
                connectionItem.GetComponent<ConnectionItem>().player = connectionPlayer;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

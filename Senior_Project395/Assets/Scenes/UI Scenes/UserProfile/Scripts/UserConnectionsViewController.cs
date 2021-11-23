using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dating_Platform
{
    public class UserConnectionsViewController : MonoBehaviour
    {
        User player;
        public GameObject connectionsList;
        public GameObject connectionItemPrefab;
        // Start is called before the first frame update
        void Start()
        {
            player = SingletonManager.Instance.Player;
            foreach(string connectionId in player.connectionIds)
            {
                Debug.Log($"trying to get data for player {connectionId}");
                User connectionPlayer = DatabaseConnection.getConnectionPlayerInfo(player, "12345", connectionId);
                GameObject connectionItem = Instantiate(connectionItemPrefab);
                connectionItem.transform.SetParent(connectionsList.transform, false);
                connectionItem.GetComponent<ConnectionItem>().player = connectionPlayer;
            }

            SendMessageUpwards("updateHomeConnectionListButtonOnClicks", SendMessageOptions.DontRequireReceiver);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

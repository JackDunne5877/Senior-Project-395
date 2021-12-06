using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BotScripts
{
    public class EnemySpawner : MonoBehaviourPun//, IPunObservable
    { 
        [SerializeField]
        [Tooltip("Number Of Enemies to exist on the map at one time")]
        public int maxInstantiatedEnemies = 3;

        [SerializeField]
        [Tooltip("Number Of avaliable enemies to spawn in this scene")]
        public int enemyStockCount = 10;

        [SerializeField]
        [Tooltip("Time Between Spawns")]
        private float timeBetweenSpawns = 5f;

        private List<Transform> spawnPoints = new List<Transform>();
        private int spawnIndex;

        public int numberOfEnemies = 0;

        // Start is called before the first frame update
        void Start()
        {
            foreach(Transform point in this.gameObject.transform)
            {
                spawnPoints.Add(point);
            }

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("enemy spawner is spawning...");
                InvokeRepeating("SpawnEnemies", 1.0f, timeBetweenSpawns);
            }
            else
            {
                Debug.Log("enemy spawner not spawning: not master client");
            }
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        private void IncrementSpawnIndex() {
            if (spawnIndex + 1 < spawnPoints.Count)
            {
                spawnIndex++;
            }
            else {
                spawnIndex = 0;
            }
        }

        private int enemyStockUsed = 0;
        private void SpawnEnemies() {
            Debug.Log("spawning enemy...");
            if(enemyStockUsed < enemyStockCount || enemyStockCount == -1) //stock count of -1 is infinite
            {
                numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
                if (numberOfEnemies < maxInstantiatedEnemies)
                {
                    PhotonNetwork.Instantiate("Bot", spawnPoints[spawnIndex].position, Quaternion.identity);
                    IncrementSpawnIndex();
                    enemyStockUsed++;
                }
            }
            
        }
    }
}
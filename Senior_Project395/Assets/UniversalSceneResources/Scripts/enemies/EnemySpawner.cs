using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BotScripts
{
    public class EnemySpawner : MonoBehaviourPun//, IPunObservable
    { [SerializeField]
        [Tooltip("Number Of Enemies to be maintained on the map")]
        private int maxEnemies = 3;

        [SerializeField]
        [Tooltip("Time Between Spawns")]
        private float timeBetweenSpawns = 5f;

        [SerializeField]
        [Tooltip("Empty game objects to represent where you want the enemies to spawn")]
        private Transform[] spawnPoints;
        private int spawnIndex;

        public int numberOfEnemies = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                InvokeRepeating("SpawnEnemies", 1.0f, timeBetweenSpawns);
            }
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        private void IncrementSpawnIndex() {
            if (spawnIndex + 1 < spawnPoints.Length)
            {
                spawnIndex++;
            }
            else {
                spawnIndex = 0;
            }
        }

        private void SpawnEnemies() {
            numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (numberOfEnemies < maxEnemies) {
                PhotonNetwork.Instantiate("Bot", spawnPoints[spawnIndex].position, Quaternion.identity);
                IncrementSpawnIndex();
            }
        }
    }
}
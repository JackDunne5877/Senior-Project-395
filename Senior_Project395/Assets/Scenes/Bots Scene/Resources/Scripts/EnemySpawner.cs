using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BotScripts
{
    public class EnemySpawner : MonoBehaviourPun
    {
        [SerializeField]
        private GameObject enemyPrefab;

       // [SerializeField]
        //private int numberOfEnemies = 3;

        [SerializeField]
        private Transform[] spawnPoints;


        // Start is called before the first frame update
        void Start()
        {
            foreach (var s in spawnPoints) {
                PhotonNetwork.Instantiate(enemyPrefab.name, s.position, Quaternion.identity);       
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
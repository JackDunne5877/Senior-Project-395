using Com.Orion.MP;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using BotScripts;
using Dating_Platform;

public class ZombieGameController : MonoBehaviour
{
    //game controller, must have a activate<className> method to be activated by singleton
    public bool activated = false;
    public Game parentGame;
    public bool demoJustOneLevel = false;
    public List<Level> levelSeries = new List<Level>();
    public List<int> levelEnemyCounts = new List<int>();
    private int currentLevel;
    private string GameNameCheck = "Zombies";
    private int playersPerRoom = 2;

    public int levelOneZombieCount = 5;
    public int levelZombieIncrease = 5;
    public bool infiniteLastLevel = false;

    public int maxEnemiesAtOneTime = 5;

    private bool debugging = false;
    public void activateZombieGameController()
    {
        Debug.Log("zombie game controller activated");
        activated = true;
        //load player into a matchmaking scene
        SceneManager.LoadScene("Matchmaking");

        SceneManager.sceneLoaded += OnSceneLoaded;
        parentGame = SingletonManager.Instance.currentPlayingGame;

        //Add first
        levelSeries.Add(parentGame.levels[0]);

        //shuffle any middle levels and add them
        if (!demoJustOneLevel)
        {
            levelSeries.AddRange(ShuffleLevels(parentGame.levels.GetRange(1, parentGame.levels.Count - 2)));

            //Add last
            levelSeries.Add(parentGame.levels[parentGame.levels.Count - 1]);
        }

        //Compute and store levelEnemyCounts
        for ( int i = 0; i < levelSeries.Count; i++)
        {
            if(i == levelSeries.Count - 1 && infiniteLastLevel) //this is the last level
            {
                levelEnemyCounts.Add(-1);
            }
            else
            {
                levelEnemyCounts.Add(levelOneZombieCount + (levelZombieIncrease * i));
            }
        }


        if (debugging)
        {
            Debug.Log("ZombiesGameController initialized levels to:");
            foreach (Level l in levelSeries)
            {
                Debug.Log($"{l.sceneName}, ");
            }
        }

        //TODO wait here for other players to be found / to show them in the matchmaking screen

        currentLevel = -1; //none loaded yet
        Launcher launcher = GetComponent<Launcher>();

        if(launcher != null)
        {
            Debug.Log($"Tried to launch into {levelSeries[0].sceneNumber}");
            launcher.Connect(levelSeries[0].sceneNumber);
        }


        //GetComponent<Launcher>().Connect(levelSeries[0].sceneNumber);
        //Debug.Log($"tried to launch into {levelSeries[0].sceneNumber}");

        SingletonManager.Instance.playingWithOtherPlayer =
            DatabaseConnection.getConnectionPlayerInfo(SingletonManager.Instance.Player, "12345", SingletonManager.Instance.Player.connectionIds[0]);
        //^^^ SIMULATION for demo
    }

    private static List<Level> ShuffleLevels(List<Level> inputList)
    {
        List<Level> outputLevels = new List<Level>();

        while(inputList.Count > 0)
        {
            int levelNum = Random.Range(0, inputList.Count);
            outputLevels.Add(inputList[levelNum]);
            inputList.RemoveAt(levelNum);
        }

        return outputLevels;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int level = scene.buildIndex;
        //check if level is the next one we're looking for
        if(activated && level == levelSeries[currentLevel+1].sceneNumber)
        {
            //set up level trigger to go to the next level
            LevelLoaderTrigger trigger = Component.FindObjectOfType<LevelLoaderTrigger>();
            if (level == levelSeries.Last<Level>().sceneNumber)
            {
                trigger.levelName = "PostGame";  //set the loader to point to the post-game screen
                trigger.isLoadingUIScene = true;
            }
            else
            {
                currentLevel++;
                trigger.levelName = levelSeries[currentLevel + 1].sceneName;  //set the loader to point to our next level
                trigger.isLoadingUIScene = false;
                Debug.Log($"set the level loader to load {levelSeries[currentLevel + 1].sceneName}");

                //set up zombie spawner to the correct values
                EnemySpawner zombieSpawner = Component.FindObjectOfType<EnemySpawner>();
                if (zombieSpawner != null)
                {
                    zombieSpawner.enemyStockCount = levelEnemyCounts[currentLevel];
                    zombieSpawner.maxInstantiatedEnemies = maxEnemiesAtOneTime;
                }
            }

            
        }
    }

    void Update()
    {
        if (activated && parentGame != null && parentGame.name != GameNameCheck)
        {
            Debug.LogError("ZombieGameController somehow got activated incorrectly");
            activated = false;
            return;
        }
    }

    void deactivateZombieGameController()
    {
        Debug.Log("zombiegamecontroller deactivated");
        activated = false;
    }

}

using Com.Orion.MP;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieGameController : MonoBehaviour
{
    //game controller, must have a activate<className> method to be activated by singleton
    public bool activated = false;
    public Game parentGame;
    public List<Level> levelSeries = new List<Level>();
    private int currentLevel;
    private string GameNameCheck = "Zombies";
    public void activateZombieGameController()
    {
        activated = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
        parentGame = SingletonManager.Instance.currentPlayingGame;

        //Add first
        levelSeries.Add(parentGame.levels[0]);

        //shuffle any middle levels and add them
        levelSeries.AddRange(ShuffleLevels(parentGame.levels.GetRange(1, parentGame.levels.Count - 2)));

        //Add last
        levelSeries.Add(parentGame.levels[parentGame.levels.Count - 1]);

        Debug.Log("ZombiesGameController initialized levels to:");
        foreach(Level l in levelSeries)
        {
            Debug.Log($"{l.sceneName}, ");
        }

        currentLevel = -1; //none loaded yet

        GameObject.Find("Multiplayer Launcher").GetComponent<Launcher>().Connect(levelSeries[0].sceneNumber);
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
            }
        }

        void Update()
        {
            if (parentGame.name != GameNameCheck)
            {
                Debug.LogError("ZombieGameController somehow got activated incorrectly");
                activated = false;
                return;
            }
        }
    }

}

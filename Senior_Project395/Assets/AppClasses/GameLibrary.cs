using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLibrary : MonoBehaviour
{
    public Sprite BotsCoverImg;
    public Sprite OfficeCoverImg;
    public Sprite ReceptionAtriumCoverImg;
    public List<Game> games;
    // Start is called before the first frame update
    void Start()
    {
        games = new List<Game>() {
        new Game
        {
            name="Zombies",
            GameControllerType = "ZombieGameController",
            coverImage = ReceptionAtriumCoverImg,
            desc = "welcome to our innovative zombie filled company",
            levels = new List<Level>(){
                new Level{
                    sceneNumber = 7,
                    sceneName = "Level1",
                },
                new Level{
                    sceneNumber = 10,
                    sceneName = "OfficeLevel",
                },
                new Level{
                    sceneNumber = 12,
                    sceneName = "CafeteriaLevel",
                },
                new Level{
                    sceneNumber = 14,
                    sceneName = "ServersLevel",
                },
                new Level{
                    sceneNumber = 4,
                    sceneName = "LabLevel",
                },
            }

        }



    };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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
        new Game{
            sceneNumber = 2, //index in build settings
            sceneName = "Bots", //actual name of the scene object in the project
            name="Bots", //a name just for display
            coverImage = BotsCoverImg, //img to represent the game in menus
            desc = "fight some zombie bots" //description just for display
        },
        new Game{
            sceneNumber = 10,
            sceneName = "OfficeLevel",
            name="Office",
            coverImage = OfficeCoverImg,
            desc = "battle some zombies and have those reports on my desk by the end of the day"
        },
        new Game
        {
            sceneNumber = 7,
            sceneName = "Level1",
            name="Reception/Atrium",
            coverImage = ReceptionAtriumCoverImg,
            desc = "welcome to our innovative zombie filled company"
        }



    };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

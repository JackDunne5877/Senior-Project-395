using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLibrary : MonoBehaviour
{
    public Sprite BotsCoverImg;
    public List<Game> games;
    // Start is called before the first frame update
    void Start()
    {
        games = new List<Game>() {
        new Game{
            sceneNumber = 2,
            name="Bots",
            coverImage = BotsCoverImg,
            desc = "fight some zombie bots"
        },

    };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

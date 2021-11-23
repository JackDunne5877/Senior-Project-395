using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameListing : MonoBehaviour
{
    private Game game;
    public GameObject gameCoverImg;
    public GameObject gameTitle;

    public Game Game { 
        get => game;
        set {
            game = value;
            populateGameInfo(); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void populateGameInfo()
    {
        if(game != null)
        {
            Debug.Log("populating game Info into a gamelisting obj");
            Debug.Log(game.name);
            var obj = gameTitle.GetComponent<Text>().text;
            if(obj == null)
            {
                Debug.LogError("could not get gameTitle obj in gamelisting");
                return;
            }
            gameTitle.GetComponent<Text>().text = game.name;
            gameCoverImg.GetComponent<Image>().sprite = game.coverImage;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Orion.MP;

public class MinigameSelectionViewController : MonoBehaviour
{
    public GameObject gameDrawerObj;
    public GameObject focusedGameImg;
    public GameObject focusedGameDesc;
    public GameObject focusedGameName;
    public Button playFocusedGameBtn;
    public GameObject gameListingPrefab;
    public Image profileImage;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Game game in SingletonManager.Instance.gameLibrary.games)
        {
            GameObject gameListing = Instantiate(gameListingPrefab);
            gameListing.GetComponent<GameListing>().Game = game;
            gameListing.GetComponent<Button>().onClick.AddListener(delegate { focusGame(game); });
            gameListing.transform.SetParent(gameDrawerObj.transform, false);
        }
        profileImage.sprite = SingletonManager.Instance.Player.ProfileImg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void focusGame(Game gameToFocus)
    {
        focusedGameDesc.GetComponent<Text>().text = gameToFocus.desc;
        focusedGameImg.GetComponent<Image>().sprite = gameToFocus.coverImage;
        focusedGameName.GetComponent<Text>().text = gameToFocus.name;
        playFocusedGameBtn.GetComponentInChildren<Text>().text = $"Play {gameToFocus.name}";
        playFocusedGameBtn.onClick.AddListener(delegate { 
            SingletonManager.Instance.currentPlayingGame = gameToFocus;
            SingletonManager.Instance.gameObject.SendMessage($"activate{gameToFocus.GameControllerType}");

            //GameObject.Find("Multiplayer Launcher").GetComponent<Launcher>().Connect(gameToFocus.levels[0].sceneNumber);
            //this should now be called in the game's controller
        });
    }

}

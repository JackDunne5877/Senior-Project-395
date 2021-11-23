using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Orion.MP;

public class LoadGamesIntoGrid : MonoBehaviour
{
    public GameObject gameDrawerObj;
    public GameObject gameListingPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Instance == null || SingletonManager.Instance.gameLibrary == null)
        {
            Debug.LogWarning("no games found in gameLibary to load into grid");
            return;
        }
            


        foreach (Game game in SingletonManager.Instance.gameLibrary.games)
        {
            GameObject gameListing = Instantiate(gameListingPrefab);
            gameListing.GetComponent<GameListing>().Game = game;
            gameListing.GetComponent<Button>().onClick.AddListener(delegate {
                SingletonManager.Instance.currentPlayingGame = game;
                SingletonManager.Instance.gameObject.SendMessage($"activate{game.GameControllerType}");
            });
            gameListing.transform.SetParent(gameDrawerObj.transform, false);
        }

        Debug.Log($"Loaded {SingletonManager.Instance.gameLibrary.games.Count} games into grid through script");
    }

}

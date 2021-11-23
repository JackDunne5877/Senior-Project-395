using Com.Orion.MP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class PostGameController : MonoBehaviour
{
    [SerializeField]
    private Button homeBtn;
    [SerializeField]
    private Button playAgainBtn;
    [SerializeField]
    private Button playWithNewBtn;

    // Start is called before the first frame update
    void Start()
    {
        bool singletonHasGame = (SingletonManager.Instance != null && SingletonManager.Instance.currentPlayingGame != null);
        homeBtn.interactable = singletonHasGame;
        playAgainBtn.interactable =
            singletonHasGame
            && (false); //TODO add check if the other player is still in the room
        playWithNewBtn.interactable = singletonHasGame;
    }

    public void goHomeClicked()
    {
        NetworkManager netMan = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        netMan.LeaveRoom();
        SceneManager.LoadScene("Menu");
    }

    public void playAgainClicked()
    {
        //Stay in room, load the level again
        //TODO add confirmation that the other player wants to play again
        //PhotonNetwork.LoadLevel(SingletonManager.Instance.currentPlayingGame.);//fix with new game definition
    }

    public void playWithNewClicked()
    {
        NetworkManager netMan = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => netMan.LeaveRoom());

        //TODO send player to matchmaking, with this game still selected
    }
}

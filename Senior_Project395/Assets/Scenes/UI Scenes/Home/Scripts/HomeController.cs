using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dating_Platform;
using TMPro;

public class HomeController : MonoBehaviour
{
    public User thisPlayer;
    public Image profileImgObj;
    public bool isConnectionSelected;
    public GameObject selectedConnectionToolbar;
    public ConnectionItem selectedConnectionToolbarItem;
    private User focusedConnection;

    public GameObject invitingCancelBtn;

    public GameObject microphoneIndicator;

    public Game selectedGame;
    public TabGroup homeTabs;

    public GameObject connectionList;
    private bool invitingToGame;
    private bool chatting;

    private string inviteBtnStartingMsg = "";
    private string chatBtnStartingMsg = "";
    public string inviteBtnActiveMsg = "";
    public string chatBtnActiveMsg = "";
    private Color buttonStartingColor;
    public Color activeBtnColor;

    public User FocusedConnection {
        get => focusedConnection;
        set {
            selectedConnectionToolbar.SetActive(value != null);
            focusedConnection = value;

            if (value != null)
                selectedConnectionToolbarItem.player = value;
        }
    }

    public bool Chatting { 
        get => chatting;
        set {
            chatting = value;
            microphoneIndicator.SetActive(chatting);
            selectedConnectionToolbarItem.chatBtn.gameObject.GetComponentInChildren<Image>().color = (chatting ? activeBtnColor : buttonStartingColor);
            selectedConnectionToolbarItem.chatBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (chatting ? chatBtnActiveMsg : chatBtnStartingMsg);

            selectedConnectionToolbarItem.chatBtn.onClick.RemoveAllListeners();
            if (chatting)
            {
                selectedConnectionToolbarItem.chatBtn.onClick.AddListener(stopVoiceChat);
            }
            else
            {
                selectedConnectionToolbarItem.chatBtn.onClick.AddListener(joinVoiceChatWithFocusedPlayer);
            }
        }
    }

    public bool InvitingToGame { 
        get => invitingToGame;
        set { 
            invitingToGame = value;
            invitingCancelBtn.SetActive(invitingToGame);
            selectedConnectionToolbarItem.inviteBtn.interactable = !invitingToGame;
            selectedConnectionToolbarItem.inviteBtn.gameObject.GetComponentInChildren<Image>().color = (invitingToGame ? activeBtnColor : buttonStartingColor);
            selectedConnectionToolbarItem.inviteBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (invitingToGame ? inviteBtnActiveMsg : inviteBtnStartingMsg);

            selectedConnectionToolbarItem.inviteBtn.onClick.RemoveAllListeners();
            if (invitingToGame)
            {
                selectedConnectionToolbarItem.inviteBtn.onClick.AddListener(stopInviting);
            }
            else
            {
                selectedConnectionToolbarItem.inviteBtn.onClick.AddListener(inviteFocusedPlayerToGame);
            }
        } 
    }

    public void Update()
    {
        //check if focused player has switched
        if (FocusedConnection != null && FocusedConnection.PlayerID != SingletonManager.Instance.viewingConnectionPlayer.PlayerID)
            FocusedConnection = SingletonManager.Instance.viewingConnectionPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        inviteBtnStartingMsg = selectedConnectionToolbarItem.inviteBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        chatBtnStartingMsg = selectedConnectionToolbarItem.chatBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        buttonStartingColor = selectedConnectionToolbarItem.chatBtn.GetComponentInChildren<Image>().color;

        thisPlayer = SingletonManager.Instance.Player;

        profileImgObj.sprite = thisPlayer.ProfileImg;
        FocusedConnection = SingletonManager.Instance.viewingConnectionPlayer;

        InvitingToGame = false;
        Chatting = false;
    }

    public void updateHomeConnectionListButtonOnClicks(){
        //hook up all buttons

        foreach (Transform t in connectionList.transform)
        {
            ConnectionItem conn = t.gameObject.GetComponent<ConnectionItem>();
            conn.chatBtn.onClick.AddListener(delegate {
                joinVoiceChatClicked(conn.player);
            });
            conn.inviteBtn.onClick.AddListener(delegate {
                InviteToGameClicked(conn.player);
            });
        }

        Debug.Log("hooked up connectionListButtons");
    }


    void SelectPlayer(User otherPlayer)
    {
        if(FocusedConnection == null || FocusedConnection.PlayerID != otherPlayer.PlayerID)
        {
            FocusedConnection = otherPlayer;
            InvitingToGame = false;
            Chatting = false;
        }
    }

    void InviteToGameClicked(User otherPlayer)
    {
        //"invite to game" clicked on selectedConnectionToolbarItem or in a connectionItem in the list

        //make sure the player corresponding to the calling item is selected
        SelectPlayer(otherPlayer);
        inviteFocusedPlayerToGame();
    }

    void joinVoiceChatClicked(User otherPlayer)
    {
        //"join voice chat" clicked on selectedConnectionToolbarItem or in a connectionItem in the list

        //make sure the player corresponding to the calling item is selected 
        SelectPlayer(otherPlayer);
        joinVoiceChatWithFocusedPlayer();
    }

    public void inviteFocusedPlayerToGame()
    {
        InvitingToGame = true;
        //open the game panel, 
        homeTabs.setTabIndex(0);//strongly coupled to the order of views in tabGroup

    }

    public void stopInviting()
    {
        InvitingToGame = false;
    }

    public void joinVoiceChatWithFocusedPlayer()
    {
        Chatting = true;
        Debug.Log($"chatting with {focusedConnection.DisplayName}");
    }

    public void stopVoiceChat()
    {
        Chatting = false;
    }
}

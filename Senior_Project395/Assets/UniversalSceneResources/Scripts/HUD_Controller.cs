using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Com.Orion.MP;
using StarterAssets;

public class HUD_Controller : MonoBehaviourPun
{
    // Start is called before the first frame update
    public Color ammoCountColor;
    public Color ammoEmptyColor;

    private PlayerNetworkManager PlayerNetManager;
    public Text pointsText;
    public Text ammoText;
    public Text timerText;
    public Text respawnText;
    private int Points = 0;
    private bool isDead = false;
    private Animator playerAnim;

    public GameObject interactPromptObj;
    public GameObject interactionDescriptionObj;
    public bool showInteractPrompt = false;
    public bool showInteractionDesc = false;

    public PhotonView pv;

    void Start()
    {
        PlayerNetManager = GetComponentInParent<PlayerNetworkManager>();

        playerAnim = GetComponentInChildren<Animator>();

        interactPromptObj.SetActive(false);
        interactionDescriptionObj.SetActive(false);
        respawnText.transform.parent.gameObject.SetActive(false);
        pv = PhotonView.Get(this);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(0f);

        if (isDead && GetComponent<StarterAssetsInputs>().reload)
        {
            playerRespawn();
        }
    }

    //adds a leading zero if the number is single digit
    private string ToStringForTimeValues(int num) 
    {
        if (num / 10 >= 1)
        {
            return num.ToString();
        }
        else {
            return "0" + num.ToString();
        }
    }
    //must be less than 1 hour
    private string ConvertTimeToString(float time) 
    {
        int min = (int)(time / 60);
        int sec = (int)(time - ((float)min * 60));
        int hundrethsec = (int)((time - (min * 60 + sec)) * 100);

        string minutes = ToStringForTimeValues(min);
        string seconds = ToStringForTimeValues(sec);
        string hundreths = ToStringForTimeValues(hundrethsec);
        return minutes + ":" + seconds + ":" + hundreths;
    }

    //by default just updates the timer based on the how long the round has gone on, optional
    //argument for changing the time, like you could call this with -5 and it would take off five seconds
    //on the clock, or +5 and it would add five seconds
    public void UpdateTimer(float timeChangeInSeconds) 
    {
        SingletonManager.Instance.roundTime += Time.deltaTime + timeChangeInSeconds;
        timerText.text = ConvertTimeToString(SingletonManager.Instance.roundTime);
    }
    
    public void AddPoints(int pts)
    {
        Debug.Log("Adding points");
        Points += pts;
        pointsText.text = Points.ToString();
    }

    public void updateAmmoCount(int ammo)
    {
        //Debug.Log("Updating Ammo Count");
        ammoText.text = ammo.ToString();
        if(ammo == 0)
        {
            ammoText.color = ammoEmptyColor;
        }
        else
        {
            ammoText.color = ammoCountColor;
        }
    }

    public void CheckInteractPromptVisibility()
    {
        if (showInteractPrompt)
        {
            interactPromptObj.SetActive(true);
        }
        else
        {
            interactPromptObj.SetActive(false);
            interactionDescriptionObj.SetActive(false);
        }
    }

    //method to set object description to show up
    public void CheckDescriptionTextVisibility()
    {
        if (showInteractionDesc)
        {
            showInteractPrompt = false;
            interactionDescriptionObj.SetActive(true);
            interactPromptObj.SetActive(false);
        }
    }

    public void playerDie()
    {
        //store local state
        isDead = true;
        //show respawn text
        respawnText.transform.parent.gameObject.SetActive(true);
        //play die animation
        playerAnim.SetBool("isDead", true);
    }
   
   public void playerRespawn()
    {
        //store local state
        isDead = false;
        //show respawn text
        respawnText.transform.parent.gameObject.SetActive(false);
        //RespawnTextObj.gameObject.SetActive(false);
        //play die animation
        playerAnim.SetBool("isDead", false);
    }
}

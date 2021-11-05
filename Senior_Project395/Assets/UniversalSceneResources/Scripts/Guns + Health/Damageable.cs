using UnityEngine;
using System.Collections;
using Photon.Pun;
public class Damageable : MonoBehaviourPun
{
    //The box's current health point total
    public float healthCapacity = 3f;
    public float currentHealth;
    public int points = 100;
    public Material[] damageLevelMats;
    PhotonView pv;
    public void Start()
    {
        pv = PhotonView.Get(this);
        currentHealth = healthCapacity;
    }
    public int doDamage(int damageAmount) //returns points
    {
        
        pv.RPC("updateHealthAndModel", RpcTarget.All, damageAmount);

        //Check if health has fallen below zero
        if (currentHealth <= 0f)
        {
            //if health has fallen below zero, deactivate it 
            deactivate();
            return points;
        }
        return 0;
    }


    [PunRPC]
    public void updateHealthAndModel(int damageAmount) {
        //subtract damage amount when Damage function is called
        currentHealth -= (float)damageAmount;
        int currentMatIdx = (int)Mathf.Ceil((currentHealth / healthCapacity) * (float)(damageLevelMats.Length - 1));
        //Debug.Log("current Mat: " + currentMatIdx);
        if (currentMatIdx < damageLevelMats.Length)
        {
            gameObject.GetComponentInChildren<Renderer>().material = damageLevelMats[currentMatIdx];
        }
    }

    [PunRPC]
    public void deactivate() {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else {
            callMasterToDestroy();
        }
    }

    public void callMasterToDestroy() {
        pv.RPC("deactivate", RpcTarget.MasterClient);
    }


}

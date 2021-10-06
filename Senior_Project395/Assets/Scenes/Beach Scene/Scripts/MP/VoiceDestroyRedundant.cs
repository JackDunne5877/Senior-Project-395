using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VoiceDestroyRedundant : MonoBehaviour
{

    public static VoiceDestroyRedundant voiceInstance;

    private void Start()
    {
        if (voiceInstance == null)
        {
            voiceInstance = this;
        }
        else {
            Debug.Log("CREATED");
            Destroy(gameObject);
        }
    }
}

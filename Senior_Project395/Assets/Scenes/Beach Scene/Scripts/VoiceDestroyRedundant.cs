using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VoiceDestroyRedundant : MonoBehaviour
{

    public static VoiceDestroyRedundant voiceInstance;

    private void Awake()
    {
        if (voiceInstance == null)
        {
            voiceInstance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}

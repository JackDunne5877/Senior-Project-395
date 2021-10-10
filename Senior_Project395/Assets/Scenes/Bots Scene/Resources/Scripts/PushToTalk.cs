using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

namespace Com.Orion.MP{
    public class PushToTalk : MonoBehaviourPun
    {

        [SerializeField]
        private GameObject speakerIconImage;

        private Recorder voice;


        // Start is called before the first frame update
        void Start()
        {
            speakerIconImage.SetActive(false);
            
            voice = PlayerNetworkManager.LocalPlayerInstance.GetComponent<Recorder>();//GetComponent<Recorder>();
            
            
            if (voice != null)
            {
                voice.TransmitEnabled = false;
            }
            else {
                Debug.Log("Error, voice recorder not found!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (voice != null)
            {
                if (Input.GetButton("Evade"))
                {
                    speakerIconImage.SetActive(true);
                    voice.TransmitEnabled = true;
                }
                else
                {
                    speakerIconImage.SetActive(false);
                    voice.TransmitEnabled = false;
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

namespace Com.Orion.MP{
    public class PushToTalk : MonoBehaviour
    {

        [SerializeField]
        private GameObject speakerIconImage;

        private Recorder voice;


        // Start is called before the first frame update
        void Start()
        {
            voice = GameObject.FindGameObjectWithTag("Voice").GetComponent<Recorder>();
            speakerIconImage.SetActive(false);
            voice.TransmitEnabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Evade"))
            {
                speakerIconImage.SetActive(true);
                voice.TransmitEnabled = true;
            }
            else {
                speakerIconImage.SetActive(false);
                voice.TransmitEnabled = false;
            }
        }
    }

}

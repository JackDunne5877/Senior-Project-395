using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class randomSoundPlayer : MonoBehaviourPun
{
    public AudioSource source;
    public AudioClip[] audioClips;
    private PhotonView pv;


    private void Start()
    {
        pv = PhotonView.Get(this);
    }

    public void playRandomSound()
    {
        int clipNum = Random.Range(0, audioClips.Length);
        Debug.Log("playing sound " + clipNum);
        source.PlayOneShot(audioClips[clipNum]);
        source.Play();
    }

    public void playThisSound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}

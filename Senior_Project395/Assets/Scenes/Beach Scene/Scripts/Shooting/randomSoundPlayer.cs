using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSoundPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] audioClips;

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
        //source.Play();
    }
}

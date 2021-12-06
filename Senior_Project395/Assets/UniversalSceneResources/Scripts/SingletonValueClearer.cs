using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonValueClearer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void clearViewingConnectionPlayer()
    {
        SingletonManager.Instance.viewingConnectionPlayer = null;
    }

    public void clearPlayingWithOtherPlayer()
    {
        SingletonManager.Instance.playingWithOtherPlayer = null;
    }

    public void clearCurrentPlayingGame()
    {
        SingletonManager.Instance.currentPlayingGame = null;
    }
}

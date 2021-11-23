using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatRoomIndicator : MonoBehaviour
{
    public Image bkgd;
    public Color colorA;
    public Color colorB;
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        bkgd.color = Color.Lerp(colorA, colorB, Mathf.PingPong(Time.time, 1));
    }
}

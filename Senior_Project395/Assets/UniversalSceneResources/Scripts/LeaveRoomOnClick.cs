using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Orion.MP;

public class LeaveRoomOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    NetworkManager netMan;
    void Start()
    {
        netMan = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => netMan.LeaveRoom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

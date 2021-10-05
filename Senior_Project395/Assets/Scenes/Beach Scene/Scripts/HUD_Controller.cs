using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Orion.MP;

public class HUD_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PointsTextObj;
    public GameObject HealthBarObj;
    
    private PlayerNetworkManager PlayerNetMan;
    private Text pointsText;
    private int Points = 0;
    
    void Start()
    {
        PlayerNetMan = GetComponentInParent<PlayerNetworkManager>();
        pointsText = PointsTextObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int pts)
    {
        Debug.Log("Adding points");
        Points += pts;
        pointsText.text = Points.ToString();
    }
}

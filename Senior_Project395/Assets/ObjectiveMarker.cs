using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    [SerializeField]
    private Image img;

    [SerializeField]
    private Transform ObjectiveTarget;

    [SerializeField]
    private Text meter;

    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        if(ObjectiveTarget == null)
            ObjectiveTarget = GameObject.FindGameObjectWithTag("Objective").transform;

        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(ObjectiveTarget.position + offset);
        
        if (Vector3.Dot((ObjectiveTarget.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(ObjectiveTarget.position, transform.position)).ToString() + "m";
    }
}
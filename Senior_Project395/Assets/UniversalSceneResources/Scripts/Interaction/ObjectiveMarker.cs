using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{
    [SerializeField]
    private Image img;

    [SerializeField]
    private Transform player;

    private Transform target;

    [SerializeField]
    private Text meter;

    [SerializeField]
    private Vector3 offset;

    private bool isObjective = true;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Objective").transform;
        if (target == null) {
            img.enabled = false;
            meter.enabled = false;
            isObjective = false;
        }
    }

    private void Update()
    {
        if (!isObjective || target == null) {
            return;
        }


        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - player.position), player.forward) < 0)
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
        meter.text = ((int)Vector3.Distance(target.position, player.position)).ToString() + "m";
    }
}
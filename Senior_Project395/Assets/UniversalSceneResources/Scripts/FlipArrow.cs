using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlipArrow : MonoBehaviour
{
    public bool arrowFacesTheLeft; // The arrow faces right by default, flip it to the left if this is selected


    void Update()
    {
        if (arrowFacesTheLeft)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

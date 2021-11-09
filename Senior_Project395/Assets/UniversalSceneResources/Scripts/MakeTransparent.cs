using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    public GameObject currentGameObject;
    public float alpha = 0.5f;

    private void Start()
    {
        currentGameObject = gameObject;
    }

    private void Update()
    {
        ChangeAlpha(currentGameObject.GetComponent<Renderer>().material, alpha);
    }

    void ChangeAlpha(Material mat, float AlphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, AlphaVal);
        mat.SetColor("_Color", newColor);
    }
}

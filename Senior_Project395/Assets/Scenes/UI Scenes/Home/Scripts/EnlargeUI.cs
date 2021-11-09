using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnlargeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor; // Amount to enlarge the UI by (1 is the current UI size)

    private GameObject ui; // UI to enlarge
    private RectTransform rectTrans; // The RectTransform of the UI
    private RectTransform initialRectTrans; // The initial RectTransform to reset to when the UI shrinks again
    private Vector2 enlarged; // Vector for the enlarged UI
    private Vector2 initial; // Vector for the initial UI


    void Start()
    {
        // Set the UI to be enlarged as the UI that this script is attached to
        ui = this.gameObject;

        // Get the canvas' rect transform
        rectTrans = ui.GetComponent<RectTransform>();
        initialRectTrans = ui.GetComponent<RectTransform>();

        // Calculate the initial UI size
        initial = new Vector2(rectTrans.sizeDelta.x, rectTrans.sizeDelta.y);

        // Calculate the enlarged UI size
        scaleFactor -= 1;
        float width = rectTrans.rect.width;
        float height = rectTrans.rect.height;
        float enlargedX = rectTrans.sizeDelta.x + (width * scaleFactor);
        float enlargedY = rectTrans.sizeDelta.y + (height * scaleFactor);
        enlarged = new Vector2(enlargedX, enlargedY);
    }



    // Rescale the UI
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTrans.sizeDelta = enlarged;
    }



    // Reset the UI size
    public void OnPointerExit(PointerEventData eventData)
    {
        rectTrans.sizeDelta = initial;
    }
}

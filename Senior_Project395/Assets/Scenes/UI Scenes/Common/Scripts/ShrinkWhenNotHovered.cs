using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShrinkWhenNotHovered : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int shrinkDistance; // Amount to be shrunk, in offset-max/mins i.e. Top, Bottom, Right, Left in RectTransform

    private GameObject ui; // UI to enlarge
    VerticalLayoutGroup layout; // The RectTransform of the UI
    private RectTransform initialRectTrans; // The initial RectTransform to reset to when the UI shrinks again
    private Vector2 enlarged; // Vector for the enlarged UI
    private Vector2 initial; // Vector for the initial UI


    void Start()
    {
        // Get the canvas' rect transform
        layout = gameObject.GetComponent<VerticalLayoutGroup>();
        //initialRectTrans = ui.GetComponent<RectTransform>();

        // Calculate the initial UI size
        //initial = new Vector2(rectTrans.sizeDelta.x, rectTrans.sizeDelta.y);

        setShrinkOffsets();

        // Calculate the enlarged UI size
        //scaleFactor -= 1;
        //float width = rectTrans.rect.width;
        //float height = rectTrans.rect.height;
        //float enlargedX = rectTrans.sizeDelta.x + (width * scaleFactor);
        //float enlargedY = rectTrans.sizeDelta.y + (height * scaleFactor);
        //enlarged = new Vector2(enlargedX, enlargedY);
    }



    // Rescale the UI / push it to the edges of it's parent's boundaries
    public void OnPointerEnter(PointerEventData eventData)
    {
        //rectTrans.sizeDelta = enlarged;

        /*Left, Bottom*/
        //rectTrans.offsetMin = Vector2.zero;
        /*Right, Top*/
        //rectTrans.offsetMax = Vector2.zero;

        layout.padding.left = 0;
        layout.padding.right = 0;
        layout.padding.top = 0;
        layout.padding.bottom = 0;
    }



    // Reset the UI size
    public void OnPointerExit(PointerEventData eventData)
    {
        //rectTrans.sizeDelta = initial;

        setShrinkOffsets();
    }

    public void setShrinkOffsets()
    {
        /*Left, Bottom*/
        //rectTrans.sizeDelta = new Vector2((100 - shrinkDistance)/100f, (100 - shrinkDistance)/100f);
        //rectTrans.offsetMin = new Vector2(shrinkDistance, shrinkDistance);
        /*Right, Top*/
        //rectTrans.offsetMax = new Vector2(-shrinkDistance, -shrinkDistance);

        layout.padding.left = shrinkDistance;
        layout.padding.right = shrinkDistance;
        layout.padding.top = shrinkDistance;
        layout.padding.bottom = shrinkDistance;
    }
}

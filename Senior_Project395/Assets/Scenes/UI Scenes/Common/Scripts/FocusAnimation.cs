using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocusAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float rotationSpeed; // Animation rotation speed in degrees per second
    public GameObject animationImageObject; // The object that holds animationImage

    private GameObject ui; // UI to hover over
    private Image animationImage; // Image to be animated
    public bool playing; // True if the animation is playing
     


    void Start()
    {
        // Set the UI as the UI that this script is attached to
        ui = this.gameObject;

        // Set the animation image
        animationImage = animationImageObject.GetComponent<Image>();

        // The animation is disabled by default
        animationImage.enabled = false;
    }



    void Update()
    {
        // Spin the image
        if (playing)
        {
            animationImageObject.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }



    // Rescale the UI
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Make the image visible
        animationImage.enabled = true;

        playing = true;
    }



    // Reset the UI size
    public void OnPointerExit(PointerEventData eventData)
    {
        // Make the image invisible
        animationImage.enabled = false;

        playing = false;
    }
}

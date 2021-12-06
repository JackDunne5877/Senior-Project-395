using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButtonPress : MonoBehaviour
{
    private Vector3 buttonPressedLocation;
    public GameObject pressedButtonObj;
    private Interactable_new interactable;
    public GameObject buttonObj;
    private bool showingPress = false;
    private Vector3 unpressedButtonLocation;
    // Start is called before the first frame update
    void Start()
    {
        buttonPressedLocation = pressedButtonObj.transform.position;
        interactable = GetComponent<Interactable_new>();
        unpressedButtonLocation = new Vector3(buttonObj.transform.position.x, buttonObj.transform.position.y, buttonObj.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable != null && interactable.isBeingInteracted && !showingPress)
        {
            StartCoroutine(showButtonPress());
        }
    }

    IEnumerator showButtonPress()
    {
        showingPress = true;

        //press in
        while (Vector3.Distance(buttonObj.transform.position, buttonPressedLocation) > 0.001f)
        {
            buttonObj.transform.position = Vector3.Lerp(buttonObj.transform.position, buttonPressedLocation, 3f * Time.deltaTime);
            yield return null;
        }
        buttonObj.transform.position = buttonPressedLocation;

        //pop out
        while (Vector3.Distance(buttonObj.transform.position, unpressedButtonLocation) > 0.001f)
        {
            buttonObj.transform.position = Vector3.Lerp(buttonObj.transform.position, unpressedButtonLocation, 5f * Time.deltaTime);
            yield return null;
        }
        buttonObj.transform.position = unpressedButtonLocation;

        showingPress = false;
    }

}

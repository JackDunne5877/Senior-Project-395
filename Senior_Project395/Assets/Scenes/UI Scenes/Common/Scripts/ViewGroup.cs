using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGroup : MonoBehaviour
{
    public GameObject[] views;
    public int viewIdx;
    // Start is called before the first frame update
    void Start()
    {
    }

    void showCurrentView()
    {

        for (int i = 0; i < views.Length; i++)
        {
            if (i == viewIdx){
                views[i].gameObject.SetActive(true);
                ReloadView reload = views[i].GetComponent<ReloadView>();
                if (reload != null)
                {
                    reload.reloadView();
                }
            }
            else { 
                views[i].gameObject.SetActive(false);
                Cleanup clean = views[i].GetComponent<Cleanup>();
                if(clean != null)
                {
                    clean.cleanupView();
                }

            }
        }


    }

    public void setViewIndex(int index)
    {
        viewIdx = index;
        showCurrentView();
    }
}

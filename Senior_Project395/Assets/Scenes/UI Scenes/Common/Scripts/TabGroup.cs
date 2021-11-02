using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public GameObject[] tabs;
    public ViewGroup viewGroup;
    public Color inactiveTabColor;
    public Color ActiveTabColor;
    int tabIdx = 0;
    // Start is called before the first frame update
    void Awake()
    {
        viewGroup.setViewIndex(tabIdx);
    }

    void showCurrentTab()
    {
        for(int i = 0; i < tabs.Length; i++)
        {
            if(i == tabIdx)
            {
                tabs[i].gameObject.GetComponentInChildren<Image>().color = ActiveTabColor;
            }
            else
            {
                tabs[i].gameObject.GetComponentInChildren<Image>().color = inactiveTabColor;
            }
        }
    }

    // Update is called once per frame
    public void setTabIndex(int index)
    {
        tabIdx = index;
        viewGroup.setViewIndex(tabIdx);
        showCurrentTab();
    }
}

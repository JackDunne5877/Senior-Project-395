using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<GameObject> tabs = new List<GameObject>();
    public ViewGroup viewGroup;
    public Color inactiveTabColorTint;
    private Color inactiveTabColor;
    public Color ActiveTabColor;
    public int tabIdx = 0;
    // Start is called before the first frame update
    void Awake()
    {
        viewGroup.setViewIndex(tabIdx);

        int tabNum = 0;


        foreach(Transform tab in this.transform)
        {
            int tabNumCopy = tabNum;
            tabs.Add(tab.gameObject);
            tab.gameObject.GetComponent<Button>().onClick.AddListener(delegate {
                setTabIndex(tabNumCopy);
            });
            tabNum++;
        }

        ActiveTabColor = tabs[0].GetComponent<Image>().color;
        inactiveTabColor = ActiveTabColor * inactiveTabColorTint;

        setTabIndex(tabIdx);
    }

    void showCurrentTab()
    {
        Debug.Log("showingCurrentTab in Tab Group");
        for (int i = 0; i < tabs.Count; i++)
        {
            if(i == tabIdx)
            {
                tabs[i].GetComponent<Image>().color = ActiveTabColor;
            }
            else
            {
                tabs[i].GetComponent< Image > ().color = inactiveTabColor;
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

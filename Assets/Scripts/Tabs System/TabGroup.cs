using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{

    [SerializeField] List<GameObject> tabs = new List<GameObject>();
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private Sprite TabIdle;
    [SerializeField] private Sprite TabActive;

    public void AddTab(GameObject tab)
    {
        tabs ??= new List<GameObject>();

        tabs.Add(tab);
    }
    
    public void AddButton(GameObject button)
    {
        buttons ??= new List<GameObject>();

        buttons.Add(button);
    }

    public void ClearTabs()
    {

        foreach (var tab in tabs)
        {
            if(tab != null)
                tab.SetActive(false);
        }
        
        foreach (var button in buttons)
        {
            if(button != null)
                button.SetActive(false);
        }
        
    }
    
}

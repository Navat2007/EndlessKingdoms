using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TabButton : MonoBehaviour
{
    private Button _button;
    
    [SerializeField] private bool tabLock;
    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private GameObject tab;
    [SerializeField] private GameObject button;
    [SerializeField] private TMPro.TMP_Text text;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(MakeActive);
        
        if(tabGroup != null)
            tabGroup.AddButton(button);
    }

    private void MakeActive()
    {
        if (!tabLock)
        {
            if(tabGroup != null)
                tabGroup.ClearTabs();
        
            if(tab != null)
                tab.SetActive(true);
            
            if(button != null)
                button.SetActive(true);
        }
    }
}

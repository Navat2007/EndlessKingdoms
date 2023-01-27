using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPage : MonoBehaviour
{
    
    [SerializeField] private TabGroup tabGroup;

    private void Awake()
    {
        if(tabGroup != null)
            tabGroup.AddTab(gameObject);
        
    }
}

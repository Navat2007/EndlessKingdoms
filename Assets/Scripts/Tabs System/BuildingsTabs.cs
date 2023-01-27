using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsTabs : MonoBehaviour
{

    [Header("Tab buttons:")]
    [SerializeField] private Button buildingTabBtn;
    [SerializeField] private Button buildingUpdatesTabBtn;

    [Header("Tab panels:")] 
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject buildingUpdatesPanel;

    private void Start()
    {
        if (buildingTabBtn != null)
        {
            buildingTabBtn.onClick.AddListener(() =>
            {
                buildingUpdatesPanel.SetActive(false);
                buildingUpdatesTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
                buildingUpdatesTabBtn.GetComponent<TMP_Text>().color = Color.gray;
                
                buildingPanel.SetActive(true);
                buildingTabBtn.transform.Find("LineFocus").gameObject.SetActive(true);
                buildingTabBtn.GetComponent<TMP_Text>().color = Color.yellow;
            });
            
            buildingUpdatesTabBtn.onClick.AddListener(() =>
            {
                buildingPanel.SetActive(false);
                buildingTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
                buildingTabBtn.GetComponent<TMP_Text>().color = Color.gray;
                
                buildingUpdatesPanel.SetActive(true);
                buildingUpdatesTabBtn.transform.Find("LineFocus").gameObject.SetActive(true);
                buildingUpdatesTabBtn.GetComponent<TMP_Text>().color = Color.yellow;
            });
        }
    }
}

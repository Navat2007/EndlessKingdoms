using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private GameObject textPrefab;

    private void Awake()
    {
        GameManager.DebugPanel = this;
    }

    public void AddText(string text)
    {
        GameObject item = Instantiate(textPrefab, grid, false) as GameObject;
        item.transform.SetSiblingIndex(0);
        var textComponent = item.GetComponentInChildren<TMPro.TMP_Text>();
        textComponent.text = text;
    }
}

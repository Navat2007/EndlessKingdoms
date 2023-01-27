using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Button _showShopBtn;
    [SerializeField] private Button _closeShopBtn;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _addStarsBtn;
    [SerializeField] private Button _addGemsBtn;
    
    [Header("Tabs:")]
    [SerializeField] private Button _chestTabBtn;
    [SerializeField] private Button _starsTabBtn;
    [SerializeField] private Button _gemsTabBtn;
    
    [Header("Panels:")]
    [SerializeField] private GameObject _chestPanel;
    [SerializeField] private GameObject _starsPanel;
    [SerializeField] private GameObject _gemsPanel;
    
    [Header("Chest buttons:")]
    [SerializeField] private Button _chest1Btn;
    [SerializeField] private Button _chest2Btn;
    [SerializeField] private Button _chest3Btn;

    private void Awake()
    {
        GameManager.ShopManager = this;
    }

    public void Init()
    {
        if (_showShopBtn != null)
            _showShopBtn.onClick.AddListener(() => { _panel.SetActive(true); });
        
        if (_closeShopBtn != null)
            _closeShopBtn.onClick.AddListener(() => { _panel.SetActive(false); });

        if (_chestTabBtn != null)
            _chestTabBtn.onClick.AddListener(() => { ActivePanel(_chestPanel , _chestTabBtn); });

        if (_starsTabBtn != null)
            _starsTabBtn.onClick.AddListener(() => { ActivePanel(_starsPanel , _starsTabBtn); });
        
        if (_addStarsBtn != null)
            _addStarsBtn.onClick.AddListener(() =>
            {
                _panel.SetActive(true);
                ActivePanel(_starsPanel , _starsTabBtn);
            });

        if (_gemsTabBtn != null)
            _gemsTabBtn.onClick.AddListener(() => { ActivePanel(_gemsPanel , _gemsTabBtn); });
        
        if (_addGemsBtn != null)
            _addGemsBtn.onClick.AddListener(() =>
            {
                _panel.SetActive(true);
                ActivePanel(_gemsPanel , _gemsTabBtn);
            });
    
    }
    
    private void ActivePanel(GameObject panel, Button button)
    {
        HidePanels();
        panel.SetActive(true);
        button.transform.Find("LineFocus").gameObject.SetActive(true);
        button.GetComponent<TMP_Text>().color = Color.yellow;
    }

    private void HidePanels()
    {
        _chestPanel.SetActive(false);
        _starsPanel.SetActive(false);
        _gemsPanel.SetActive(false);
        _chestTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        _starsTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        _gemsTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        _chestTabBtn.GetComponent<TMP_Text>().color = Color.gray;
        _starsTabBtn.GetComponent<TMP_Text>().color = Color.gray;
        _gemsTabBtn.GetComponent<TMP_Text>().color = Color.gray;
    }
}
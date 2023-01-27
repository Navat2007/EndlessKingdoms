using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    [SerializeField] private Button _showConfigBtn;
    [SerializeField] private Button _closeConfigBtn;
    [SerializeField] private GameObject _panel;
    
    [Header("Tabs:")]
    [SerializeField] private Button _gameplayTabBtn;
    [SerializeField] private Button _audioTabBtn;
    [SerializeField] private Button _graphicsTabBtn;
    [SerializeField] private Button _languageTabBtn;
    
    [Header("Panels:")]
    [SerializeField] private GameObject _gameplayPanel;
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private GameObject _graphicsPanel;
    [SerializeField] private GameObject _languagePanel;
    
    [SerializeField] private Button resetButton;

    [Space] 
    [Header("Tabs:")] 
    [SerializeField][TextArea(5,5)] private string reset_text = "Are you sure you want to reset game?\nAll data will be erased.\n\nShop purchases and crystals will be saved!";
    private void Awake()
    {
        GameManager.ConfigManager = this;
    }

    public void Init()
    {
        GameManager.DebugPanel.AddText($"Config Manager. Старт.");
        
        if (_showConfigBtn != null)
            _showConfigBtn.onClick.AddListener(() => { _panel.SetActive(true); });
        
        if (_closeConfigBtn != null)
            _closeConfigBtn.onClick.AddListener(() => { _panel.SetActive(false); });
        
        if(resetButton != null)
            resetButton.onClick.AddListener(() =>
            {
                GameManager.UI_Manager.ShowQuestionPopup(reset_text, "Yes", "No", () =>
                {
                    GameManager.ResetGame();
                }, null);
            });
        
        if (_gameplayTabBtn != null)
            _gameplayTabBtn.onClick.AddListener(() => { ActivePanel(_gameplayPanel , _gameplayTabBtn); });
        
        if (_audioTabBtn != null)
            _audioTabBtn.onClick.AddListener(() => { ActivePanel(_audioPanel , _audioTabBtn); });
        
        if (_graphicsTabBtn != null)
            _graphicsTabBtn.onClick.AddListener(() => { ActivePanel(_graphicsPanel , _graphicsTabBtn); });
        
        if (_languageTabBtn != null)
            _languageTabBtn.onClick.AddListener(() => { ActivePanel(_languagePanel , _languageTabBtn); });
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
        _gameplayPanel.SetActive(false);
        _audioPanel.SetActive(false);
        _graphicsPanel.SetActive(false);
        _languagePanel.SetActive(false);
        
        _gameplayTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        _audioTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        _graphicsTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        _languageTabBtn.transform.Find("LineFocus").gameObject.SetActive(false);
        
        _gameplayTabBtn.GetComponent<TMP_Text>().color = Color.gray;
        _audioTabBtn.GetComponent<TMP_Text>().color = Color.gray;
        _graphicsTabBtn.GetComponent<TMP_Text>().color = Color.gray;
        _languageTabBtn.GetComponent<TMP_Text>().color = Color.gray;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class PopupOffline : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Text goldText;
    [SerializeField] private TMPro.TMP_Text timeText;
    [SerializeField] private Button collectBtn;
    [SerializeField] private Button collectX2Btn;
    [SerializeField] private Button closeBtn;

    public void Init(BigDouble gold, long time)
    {
        goldText.text = String.Empty;
        timeText.text = String.Empty;
        collectBtn.onClick.RemoveAllListeners();
        collectX2Btn.onClick.RemoveAllListeners();

        var total_income = gold * (int) time;
        goldText.text = $"{GameManager.UI_Manager.ScoreShow(total_income)}";
        
        TimeSpan t = TimeSpan.FromSeconds( time );
        timeText.text = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
        
        GameManager.DebugPanel.AddText($"Время оффлайн: {t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s");
        
        collectBtn.onClick.AddListener(() =>
        {
            GameManager.CurrencyManager.AddCurrency(Currency.GOLD, total_income);
            gameObject.SetActive(false);
        });
        collectX2Btn.onClick.AddListener(() =>
        {
            //TODO Добавить просмотр рекламы или списать кристалы
            GameManager.UI_Manager.ShowMessagePopup("Ad video", "Ad video is not available right now, but we will still add a reward.", () =>
            {
                GameManager.DebugPanel.AddText($"Начисляю двойную награду за оффлайн: {GameManager.UI_Manager.ScoreShow(total_income * 2)}");
                GameManager.CurrencyManager.AddCurrency(Currency.GOLD, total_income * 2);
                gameObject.SetActive(false);
            });
            
        });
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public event EventHandler<BuyCount> OnBuyCountChanged;
    
    [Header("Loader:")] 
    [SerializeField] private GameObject loader;

    [Header("Кол-во для покупки:")] 
    [SerializeField] private Button buyCountBtn;
    [SerializeField] private TMPro.TMP_Text buyCountText;
    [SerializeField] private BuyCount buyCount;

    [Header("Валюта:")] 
    [SerializeField] private TMPro.TMP_Text goldText;
    [SerializeField] private TMPro.TMP_Text goldPerSecondText;
    [SerializeField] private TMPro.TMP_Text crystalText;
    [SerializeField] private TMPro.TMP_Text crystalShopText;
    [SerializeField] private TMPro.TMP_Text starsText;

    [Header("Всплывающие окна:")] 
    [SerializeField] private GameObject popupOffline;
    [SerializeField] private GameObject popupMessage;
    [SerializeField] private GameObject popupQuestion;

    private void Awake()
    {
        GameManager.UI_Manager = this;
        loader.SetActive(true);
    }

    public void Init()
    {
        GameManager.DebugPanel.AddText($"UI Manager. Старт.");

        if (buyCountBtn != null)
        {
            buyCountBtn.onClick.AddListener(() => { SetBuyCount(Next(buyCount)); });
        }
    }
    
    public void Subscribe()
    {
        GameManager.DebugPanel.AddText($"UI Manager. Подписываюсь на события");

        GameManager.CurrencyManager.OnGoldChanged += (sender, d) => UpdateCurrency(sender, d, Currency.GOLD);
        GameManager.CurrencyManager.OnGoldPerSecondChanged +=
            (sender, d) => UpdateCurrency(sender, d, Currency.GOLDPERSECOND);
        GameManager.CurrencyManager.OnCrystalChanged += (sender, d) => UpdateCurrency(sender, d, Currency.CRYSTAL);
        GameManager.CurrencyManager.OnStarsChanged += (sender, d) => UpdateCurrency(sender, d, Currency.STAR);
    }
    
    public IEnumerator RemoveLoader(int second)
    {
        yield return new WaitForSeconds(second);

        loader.SetActive(false);
    }

    private BuyCount Next(BuyCount myEnum)
    {
        switch (myEnum)
        {
            case BuyCount.ONE:
                return BuyCount.TEN;
            case BuyCount.TEN:
                return BuyCount.TWENTYFIVE;
            case BuyCount.TWENTYFIVE:
                return BuyCount.HUNDRED;
            case BuyCount.HUNDRED:
                return BuyCount.MAX;
            case BuyCount.MAX:
                return BuyCount.ONE;
            default:
                return BuyCount.ONE;
        }
    }

    public void ShowOfflinePopup(BigDouble gold, long time)
    {
        var popupOfflineScript = popupOffline.GetComponent<PopupOffline>();
        popupOfflineScript.Init(gold, time);
        popupOffline.SetActive(true);
    }

    public void ShowMessagePopup(string title, string text, Action callback)
    {
        var popupMessageScript = popupMessage.GetComponent<PopupMessage>();
        popupMessageScript.Init(title, text, callback);
        popupMessage.SetActive(true);
    }

    public void ShowQuestionPopup(string title, string okText, string cancelText, Action okCallback,
        Action cancelCallback)
    {
        var popupQuestionScript = popupQuestion.GetComponent<PopupQuestion>();
        popupQuestionScript.Init(title, okText, cancelText, okCallback, cancelCallback);
        popupQuestion.SetActive(true);
    }

    void UpdateCurrency(object obj, BigDouble value, Currency currency)
    {
        switch (currency)
        {
            case Currency.GOLD:
                if (goldText != null)
                    goldText.text = ScoreShow(value);
                break;
            case Currency.GOLDPERSECOND:
                if (goldPerSecondText != null)
                    goldPerSecondText.text = $"{ScoreShow(value)} / sec";
                break;
            case Currency.CRYSTAL:
                if (crystalText != null)
                {
                    crystalText.text = ScoreShow(value);
                    crystalShopText.text = ScoreShow(value);
                }

                break;
            case Currency.STAR:
                if (starsText != null)
                    starsText.text = ScoreShow(value);
                break;
        }
    }
    
    public string ScoreShow(BigDouble Score)
    {
        string result;
        string[] ScoreNames =
        {
            "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an",
            "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf",
            "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx",
            "by", "bz",
        };
        int i;

        for (i = 0; i < ScoreNames.Length; i++)
            if (Score < 900)
                break;
            else Score = BigDouble.Floor(Score / 100f) / 10f;

        if (Score == BigDouble.Floor(Score))
            result = Score + ScoreNames[i];
        else result = Score.ToString("F1") + ScoreNames[i];
        return result;
    }

    public string ScoreShow(double Score)
    {
        string result;
        string[] ScoreNames =
        {
            "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an",
            "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf",
            "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx",
            "by", "bz",
        };
        int i;

        for (i = 0; i < ScoreNames.Length; i++)
            if (Score < 900)
                break;
            else Score = Math.Floor(Score / 100f) / 10f;

        if (Score == Math.Floor(Score))
            result = Score + ScoreNames[i];
        else result = Score.ToString("F1") + ScoreNames[i];
        return result;
    }

    public string ScoreShow(float Score)
    {
        string result;
        string[] ScoreNames =
        {
            "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an",
            "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf",
            "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx",
            "by", "bz",
        };
        int i;

        for (i = 0; i < ScoreNames.Length; i++)
            if (Score < 900)
                break;
            else Score = Mathf.Floor(Score / 100f) / 10f;

        if (Score == Mathf.Floor(Score))
            result = Score + ScoreNames[i];
        else result = Score.ToString("F1") + ScoreNames[i];
        return result;
    }

    public string ScoreShow(int Score)
    {
        float Scor = Score;
        string result;
        string[] ScoreNames =
        {
            "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an",
            "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf",
            "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx",
            "by", "bz",
        };
        int i;

        for (i = 0; i < ScoreNames.Length; i++)
            if (Scor < 900)
                break;
            else Scor = Mathf.Floor(Scor / 100f) / 10f;

        if (Scor == Mathf.Floor(Scor))
            result = Scor + ScoreNames[i];
        else result = Scor.ToString("F1") + ScoreNames[i];
        return result;
    }

    public void SetBuyCount(BuyCount value)
    {
        buyCount = value;

        switch (buyCount)
        {
            case BuyCount.ONE:
                buyCountText.text = $"x 1";
                break;
            case BuyCount.TEN:
                buyCountText.text = $"x 10";
                break;
            case BuyCount.TWENTYFIVE:
                buyCountText.text = $"x 25";
                break;
            case BuyCount.HUNDRED:
                buyCountText.text = $"x 100";
                break;
            case BuyCount.MAX:
                buyCountText.text = $"MAX";
                break;
        }

        OnBuyCountChanged?.Invoke(null, buyCount);
    }

    public BuyCount GetBuyCount()
    {
        return buyCount;
    }
}

public enum BuyCount
{
    ONE,
    TEN,
    TWENTYFIVE,
    HUNDRED,
    MAX
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Text titleText;
    [SerializeField] private TMPro.TMP_Text messageText;
    [SerializeField] private Button okBtn;
    
    public void Init(string title, string text, Action callback)
    {
        titleText.text = title;
        messageText.text = text;
        
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(() =>
        {
            callback?.Invoke();
            gameObject.SetActive(false);
        });
    }
}

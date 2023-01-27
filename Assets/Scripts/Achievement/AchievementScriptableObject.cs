using System;
using BreakInfinity;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementScriptableObject", menuName = "ScriptableObjects/AchievementScriptableObject", order = 3)]
public class AchievementScriptableObject : ScriptableObject
{
    [Header("Настройки:")]
    public int ID;
    public Sprite image;
    [Space]
    public AchievementType achievementType;
    public BigDouble amount;
    [Space]
    [SerializeField] string title = String.Empty;
    [SerializeField] string description = String.Empty;
    
    [Header("Локализация:")]
    [SerializeField]int localizationTitleID;
    [SerializeField]int localizationDescriptionID;

    public int GetID()
    {
        return ID;
    }
    
    public string GetTitle()
    {
        return title;
    }
    
    public void SetTitle(string value)
    {
        title = value;
    }
    
    public string GetDescription()
    {
        return description;
    }
    
    public void SetDescription(string value)
    {
        description = value;
    }
}

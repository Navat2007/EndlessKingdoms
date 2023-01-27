using System;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingScriptableObject", menuName = "ScriptableObjects/BuildingScriptableObject", order = 1)]
public class BuildingScriptableObject : ScriptableObject
{
    
    [Header("Настройки:")]
    public int ID;
    public Sprite image;
    public List<BuildingUpdateScriptableObject> BuildingUpdateScriptableObjects =
        new List<BuildingUpdateScriptableObject>();
    
    [SerializeField] string title = String.Empty;
    [SerializeField] string description = String.Empty;
    [SerializeField] float base_income;
    [SerializeField] List<BaseCost> base_cost;
    [SerializeField] float update_percent = 1.15f;
    [SerializeField] float duration;

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
    
    public double GetDuration()
    {
        return duration;
    }
    
    public double GetBaseIncome()
    {
        return base_income;
    }
    
    public List<BaseCost> GetBaseCost()
    {
        return base_cost;
    }
    
    public float GetUpdatePercent()
    {
        return update_percent;
    }

    public List<BuildingUpdateScriptableObject> GetUpdates()
    {
        return BuildingUpdateScriptableObjects;
    }

}

[Serializable]
public struct BaseCost {
    public Currency Currency;
    public BigDouble Cost;
}

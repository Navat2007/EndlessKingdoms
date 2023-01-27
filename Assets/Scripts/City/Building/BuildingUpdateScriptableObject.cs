using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingUpdateScriptableObject", menuName = "ScriptableObjects/BuildingUpdateScriptableObject", order = 2)]
public class BuildingUpdateScriptableObject : ScriptableObject
{
    [Header("Настройки:")]
    public int ID;
    public Sprite image;
    [Space]
    [SerializeField] string title = String.Empty;
    [SerializeField] string description = String.Empty;
    [SerializeField] BigDouble unlock_level;
    [SerializeField] float base_multiply;
    [SerializeField] UpdateType update_type;
    [SerializeField] UpdateTypeAdd update_type_add;
    [SerializeField] List<BaseCost> base_cost;
    
    [Header("Локализация:")]
    [SerializeField]int localizationTitleID;
    [SerializeField]int localizationDescriptionID;

    public int GetID()
    {
        return ID;
    }
    
    public void SetID(int value)
    {
        ID = value;
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

    public BigDouble GetUnlockLevel()
    {
        return unlock_level;
    }
    public void SetUnlockLevel(BigDouble value)
    {
        unlock_level = value;
    }
    

    public float GetMultiply()
    {
        return base_multiply;
    }
    
    public void SetMultiply(float value)
    {
        base_multiply = value;
    }

    public UpdateType GetUpdateType()
    {
        return update_type;
    }
    
    public void SetUpdateType(UpdateType value)
    {
        update_type = value;
    }

    public UpdateTypeAdd GetUpdateTypeAdd()
    {
        return update_type_add;
    }
    
    public void SetUpdateTypeAdd(UpdateTypeAdd value)
    {
        update_type_add = value;
    }
    
    public List<BaseCost> GetBaseCost()
    {
        return base_cost;
    }
}

[Serializable]
public enum UpdateType
{
    INCOME,
    CRITCHANCE,
    CRITBONUS,
    BUILDINGINCOME
}

[Serializable]
public enum UpdateTypeAdd
{
    ADDITIVE,
    MULTIPLY
}
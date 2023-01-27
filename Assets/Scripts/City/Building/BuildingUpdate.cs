using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

[Serializable]
public class BuildingUpdate
{
    [Header("UI:")] 
    public Building Building;
    public BuildingUpdateScriptableObject BuildingUpdateScriptableObject;
    public BuildingUpdateUI BuildingUpdateUI;
    
    [Header("Data:")] 
    public GameObject BuildingUpdateGameObject;
    public bool Unlocked;
    public bool Instanced;
    public bool Purchased;

    public void Init()
    {
        if (!Purchased)
        {
            BuildingUpdateUI.itemImage.sprite = BuildingUpdateScriptableObject.image;
            BuildingUpdateUI.itemTitle.text = BuildingUpdateScriptableObject.GetTitle();
            BuildingUpdateUI.descriptionText.text = BuildingUpdateScriptableObject.GetDescription();
            BuildingUpdateUI.buyButton.onClick.AddListener(BuyUpdate);
        
            UpdateUI(null, 0);
        
            GameManager.CurrencyManager.OnGoldChanged += UpdateUI;
        }
    }

    public void UpdateUI(object sender, BigDouble e)
    {
        
        if(Purchased) return;
        
        var total_cost = CalculateCost();
        BuildingUpdateUI.costText.text = "Cost: ";
        
        foreach (var item in total_cost)
        {
            BuildingUpdateUI.costText.text += $"{GameManager.UI_Manager.ScoreShow(item.Cost)} {item.Currency}";
        }
        
        bool need_buy = true;
        var cost = CalculateCost();

        foreach (var item in cost)
        {
            if (GameManager.CurrencyManager.GetCurrency(item.Currency) < item.Cost)
            {
                need_buy = false;
            }
        }
        
        BuildingUpdateUI.buyButton.interactable = need_buy;
    }
    
    public List<BaseCost> CalculateCost()
    {
        List<BaseCost> result = new List<BaseCost>();

        foreach (var currency in Building.BuildingScriptableObject.GetBaseCost())
        {
            result.Add(new BaseCost
            {
                Currency = currency.Currency,
                Cost = (currency.Cost * BuildingUpdateScriptableObject.GetUnlockLevel()) * 2
            });
        }
        
        return result;
    }

    private void BuyUpdate()
    {
        bool need_buy = true;
        var cost = CalculateCost();

        foreach (var item in cost)
        {
            if (GameManager.CurrencyManager.GetCurrency(item.Currency) < item.Cost)
            {
                need_buy = false;
            }
        }

        if (need_buy)
        {
            foreach (var item in cost)
            {
                GameManager.CurrencyManager.AddCurrency(item.Currency, -item.Cost);
            }
            
            Purchased = true;
            GameManager.CurrencyManager.OnGoldChanged -= UpdateUI;
            Building.BuyUpdate(this);
        }
    }
}

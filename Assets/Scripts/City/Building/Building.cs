using System;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
using UnityEngine;

[Serializable]
public class Building
{
    [Header("UI:")]
    public BuildingScriptableObject BuildingScriptableObject;
    public BuildingUI BuildingUI;

    [Header("Data:")] 
    public GameObject BuildingGameObject;
    public List<BuildingUpdate> BuildingUpdates = new List<BuildingUpdate>();
    public BigDouble Amount;
    public bool Unlocked;
    public bool Instanced;
    
    public event EventHandler<Building> OnBuildingBuy;
    public event EventHandler<BuildingUpdate> OnBuildingUpdateBuy;
    
    public void Init()
    {
        BuildingUI.itemImage.sprite = BuildingScriptableObject.image;
        BuildingUI.itemTitle.text = BuildingScriptableObject.GetTitle();
        BuildingUI.buyButton.onClick.AddListener(BuyBuilding);
        
        if (BuildingScriptableObject.GetUpdates().Count > 0)
        {
            foreach (var update in BuildingScriptableObject.GetUpdates())
            {
                BuildingUpdates.Add(new BuildingUpdate
                {
                    Building = this,
                    Unlocked = Amount >= update.GetUnlockLevel(),
                    Purchased = GameManager.SaveLoadManager.LoadBuildingUpdate(BuildingScriptableObject.GetID(), update.GetID()),
                    BuildingUpdateScriptableObject = update,
                });
            }
        }
        else
        {
            List<BuildingUpdateGenerate> update_list = new List<BuildingUpdateGenerate>
            {
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 10 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 10, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 25 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 25, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 50 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 200%", 
                    level = 50, multiply = 3
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 75 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 75, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 100 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 100, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 125 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 125, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 150 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 200%", 
                    level = 150, multiply = 3
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 175 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 175, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 200 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 200, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 225 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 225, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 250 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 200%", 
                    level = 250, multiply = 3
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 275 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 100%", 
                    level = 275, multiply = 2
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 300 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 300, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 400 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 400, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 500 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 500, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 600 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 600, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 700 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 700, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 800 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 800, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 900 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 300%", 
                    level = 900, multiply = 4
                },
                new BuildingUpdateGenerate
                {
                    title = $"{BuildingScriptableObject.GetTitle()} update 1000 lvl",
                    description = $"Increase {BuildingScriptableObject.GetTitle().ToLower()} income by 500%", 
                    level = 1000, multiply = 6
                },
            };
            
            update_list.Each((update, index) =>
            {
                var so = ScriptableObject.CreateInstance<BuildingUpdateScriptableObject>();
                so.image = BuildingScriptableObject.image;
                so.SetID(index);
                so.SetTitle(update.title);
                so.SetDescription(update.description);
                so.SetUnlockLevel(update.level);
                so.SetMultiply(update.multiply);
                so.SetUpdateType(UpdateType.INCOME);
                so.SetUpdateTypeAdd(UpdateTypeAdd.MULTIPLY);
                
                BuildingUpdates.Add(new BuildingUpdate
                {
                    Building = this,
                    Unlocked = Amount >= update.level,
                    Purchased = GameManager.SaveLoadManager.LoadBuildingUpdate(BuildingScriptableObject.GetID(), so.ID),
                    BuildingUpdateScriptableObject = so,
                });
            });
        }

        UpdateUI();
        
        GameManager.CurrencyManager.OnGoldChanged += (sender, d) => UpdateUI();
        GameManager.UI_Manager.OnBuyCountChanged += (sender, d) => UpdateUI();
    }
    
    public List<BaseCost> CalculateCost(int buy_amount)
    {
        List<BaseCost> result = new List<BaseCost>();

        foreach (var currency in BuildingScriptableObject.GetBaseCost())
        {
            var n = buy_amount;
            var b = currency.Cost;
            var r = BuildingScriptableObject.GetUpdatePercent();
            var k = Amount;
            var cost = b * ((BigDouble.Pow(r, k) - BigDouble.Pow(r, k + n)) / (1 - r));
            
            result.Add(new BaseCost
            {
                Currency = currency.Currency,
                Cost = cost
            });
        }
        
        return result;
    }

    public BigDouble CalculateIncome()
    {
        BigDouble result = BuildingScriptableObject.GetBaseIncome() * Amount;
        
        //Debug.Log($"{BuildingScriptableObject.GetTitle()}: {BuildingScriptableObject.GetBaseIncome()} * {Amount} = {result}");

        if (BuildingScriptableObject.GetID() == 0)
            result += 1;

        foreach (var update in BuildingUpdates)
        {
            if (update.Purchased)
            {
                switch (update.BuildingUpdateScriptableObject.GetUpdateType())
                {
                    case UpdateType.INCOME:
                        //Debug.Log($"{update.BuildingUpdateScriptableObject.GetTitle()}");
                        switch (update.BuildingUpdateScriptableObject.GetUpdateTypeAdd())
                        {
                            case UpdateTypeAdd.ADDITIVE:
                                result += update.BuildingUpdateScriptableObject.GetMultiply();
                                break;
                            case UpdateTypeAdd.MULTIPLY:
                                result *= update.BuildingUpdateScriptableObject.GetMultiply();
                                break;
                        }
                        break;
                }
                
            }
        }
        
        //Debug.Log($"After updates => {BuildingScriptableObject.GetTitle()}: {result}");
        
        result *= GameManager.CurrencyManager.GetStarMultiply();
        
        //Debug.Log($"After stars => {BuildingScriptableObject.GetTitle()}: {result}");
        
        return result;
    }

    public List<BuildingUpdate> GetBuildingUpdates()
    {
        return BuildingUpdates;
    }

    public void BuyUpdate(BuildingUpdate update)
    {
        OnBuildingUpdateBuy?.Invoke(this, update);
        GameManager.BuildingManager.FillUpdateList(BuildingUpdates);
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        var total_cost = CalculateCost(CalculateBuyCount());
        BuildingUI.costText.text = "Cost: ";
        
        foreach (var item in total_cost)
        {
            BuildingUI.costText.text += $"{GameManager.UI_Manager.ScoreShow(item.Cost)} {item.Currency}";
        }

        var total_income = CalculateIncome();

        if (total_income > 0 && !BuildingUI.gpsText.gameObject.activeSelf)
        {
            BuildingUI.gpsText.gameObject.SetActive(true);
        }
        
        if(BuildingScriptableObject.GetID() == 0)
            BuildingUI.gpsText.text = $"Click power: {GameManager.UI_Manager.ScoreShow(total_income)}";
        else
            BuildingUI.gpsText.text = $"Income: {GameManager.UI_Manager.ScoreShow(total_income)}";
        
        if (Amount > 0 && !BuildingUI.amountText.transform.parent.gameObject.activeSelf)
        {
            BuildingUI.amountText.transform.parent.gameObject.SetActive(true);
        }
        
        BuildingUI.amountText.text = $"{Amount}";

        bool need_buy = true;
        var buy_count = CalculateBuyCount();
        var cost = CalculateCost(buy_count);

        foreach (var item in cost)
        {
            if (GameManager.CurrencyManager.GetCurrency(item.Currency) < item.Cost)
            {
                need_buy = false;
            }
        }
        
        BuildingUI.buyButton.interactable = need_buy;

        BuildingUI.buyCountText.text = $"x{buy_count}";
    }
    
    private void BuyBuilding()
    {
        bool need_buy = true;
        var buy_count = CalculateBuyCount();
        var cost = CalculateCost(buy_count);
        
        cost.Each((item, index) =>
        {
            if (GameManager.CurrencyManager.GetCurrency(item.Currency) < item.Cost)
            {
                need_buy = false;
            }
        });

        if (need_buy)
        {
            foreach (var item in cost)
            {
                GameManager.CurrencyManager.AddCurrency(item.Currency, -item.Cost);
            }

            Amount += buy_count;
            
            OnBuildingBuy?.Invoke(this, this);

            GameManager.BuildingManager.BuyItem(BuildingScriptableObject.GetID());
        }

        bool need_fill_updates = false;
        foreach (var update in BuildingUpdates)
        {
            if (!update.Unlocked && Amount >= update.BuildingUpdateScriptableObject.GetUnlockLevel())
            {
                update.Unlocked = true;
                need_fill_updates = true;
            }
                
        }
        
        if(need_fill_updates)
            GameManager.BuildingManager.FillUpdateList(BuildingUpdates);
        
        UpdateUI();
    }

    private int CalculateBuyCount()
    {
        switch (GameManager.UI_Manager.GetBuyCount())
        {
            case BuyCount.ONE:
                return 1;
            case BuyCount.TEN:
                return 10;
            case BuyCount.TWENTYFIVE:
                return 25;
            case BuyCount.HUNDRED:
                return 100;
            case BuyCount.MAX:
                var count_array = new List<int>();
                var base_costs = BuildingScriptableObject.GetBaseCost();
                
                foreach (var item in base_costs)
                {
                    var b = item.Cost;
                    var c = GameManager.CurrencyManager.GetCurrency(item.Currency);
                    var r = BuildingScriptableObject.GetUpdatePercent();
                    var k = Amount;
                    var count = (int) BigDouble.Floor(BigDouble.Log(BigDouble.Pow(r, k) - (c *((1 - r) / b)), r) - k);
                    
                    count_array.Add(count);
                }
                
                return count_array.Count > 0 ? (count_array.Min() == 0 ? 1 : count_array.Min()) : 1;
                
        }

        return 0;
    }

}

class BuildingUpdateGenerate
{
    public int ID;
    public string title;
    public string description;
    public BigDouble level;
    public int multiply;
}
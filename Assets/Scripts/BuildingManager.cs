using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using TMPro;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Global multiply:")] 
    [SerializeField] BigDouble global_multiply;
    
    // Первым зданием в списке ScriptableObject обязательно должно быть здание для улучшения кликов!!!
    [Header("Список зданий:")] 
    public List<BuildingScriptableObject> BuildingsSOList = new List<BuildingScriptableObject>();
    public List<Building> BuildingsList;

    [Header("UI:")] 
    public GameObject itemPrefab;
    public GameObject updatePrefab;
    public GameObject updateNotifPanel;
    public TMP_Text updateNotifText;
    public Transform buildingContainer;
    public Transform buildingUpdatesContainer;
    
    private void Awake()
    {
        GameManager.BuildingManager = this;
    }

    public void Init()
    {
        GameManager.DebugPanel.AddText($"Building Manager. Старт.");

        BuildingsList = new List<Building>();
        
        BuildingsSOList.Each((building, index) =>
        {
            BuildingsList.Add(new Building
            {
                BuildingScriptableObject = building,
                Unlocked = index == 0 || index == 1
            });
        });
    }

    public void StartTick()
    {
        StartCoroutine(TickIncome());
    }

    private IEnumerator TickIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            BigDouble tmp_total = 0;
            
            BuildingsList.Each((building, index) =>
            {
                
                if (index > 0 && building.Amount > 0)
                {
                    tmp_total += building.CalculateIncome();
                }
                
            });
            
            if (tmp_total > 0)
                GameManager.CurrencyManager.AddCurrency(Currency.GOLD, tmp_total);
        }
    }

    public void FillList()
    {
        foreach (var building in BuildingsList)
        {
            if (!building.Unlocked || building.Instanced) continue;
            
            var item = Instantiate(itemPrefab, buildingContainer, false);
            building.BuildingGameObject = item;
            building.BuildingUI = item.GetComponent<BuildingUI>();
            building.Instanced = true;
                    
            building.Init();
        }

        GameManager.CurrencyManager.UpdateGoldIncome();
    }
    
    public void FillUpdateList(List<BuildingUpdate> buildingUpdates)
    {
        foreach (var item in buildingUpdates)
        {
            if(item.Unlocked && item.Instanced && item.Purchased)
                DestroyImmediate(item.BuildingUpdateGameObject);
            
            if (!item.Unlocked || item.Instanced || item.Purchased) continue;
            
            var update = Instantiate(updatePrefab, buildingUpdatesContainer, false);
            item.BuildingUpdateGameObject = update;
            item.BuildingUpdateUI = update.GetComponent<BuildingUpdateUI>();
            item.Instanced = true;
                    
            item.Init();
        }

        if (buildingUpdatesContainer.childCount > 0)
        {
            updateNotifPanel.SetActive(true);
            updateNotifText.text = $"{buildingUpdatesContainer.childCount}";
        }
        else
        {
            updateNotifPanel.SetActive(false);
        }
        
        GameManager.CurrencyManager.UpdateGoldIncome();
    }

    public void BuyItem(int id)
    {
        BuildingsList.Each((building, index) =>
        {
            if (building.BuildingScriptableObject.GetID() == id && index + 1 < BuildingsList.Count && BuildingsList[index].Amount > 9)
            {
                BuildingsList[index + 1].Unlocked = true;
                FillList();
            }
        });
        
        GameManager.CurrencyManager.UpdateGoldIncome();
    }

    public BigDouble GetTotalIncome()
    {
        BigDouble tmp_total = 0;
            
        BuildingsList.Each((building, index) =>
        {
                
            if (index > 0 && building.Amount > 0)
            {
                tmp_total += building.CalculateIncome();
            }
                
        });

        return tmp_total;
    }

    public void Reset()
    {
        for (int i = 0; i < BuildingsList.Count; i++)
        {
            var building = BuildingsList[i];
            
            building.Amount = 0;

            foreach (var update in building.BuildingUpdates)
            {
                update.Instanced = false;
                update.Unlocked = false;
                update.Purchased = false;
                Destroy(update.BuildingUpdateGameObject);
            }

            if (i > 1)
            {
                building.Unlocked = false;
                building.Instanced = false;
                Destroy(building.BuildingGameObject);
            }
        }

        FillList();
        updateNotifPanel.SetActive(false);
    }
}
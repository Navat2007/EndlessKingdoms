using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bayat.SaveSystem;
using BreakInfinity;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private Save _save;
    [SerializeField] private float offlineTime = 300f;
    
    public float autoSaveTimer = 180f;
    
    private void Awake()
    {
        GameManager.SaveLoadManager = this;
    }

    public async Task Init()
    {
        GameManager.DebugPanel.AddText($"SaveLoad Manager. Старт.");
        
        _save = await Load();
        
        StartCoroutine(TickAutoSave());
    }
    
    private IEnumerator TickAutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimer);

            Save(true);
        }
    }
    
    private async Task<Save> Load()
    {
        if (!await SaveSystemAPI.ExistsAsync("save.bin")) return new Save();
        
        return await SaveSystemAPI.LoadAsync<Save>("save.bin");
    }

    public void LoadCurrency()
    {
        GameManager.CurrencyManager.SetCurrency(Currency.GOLD, _save.GetGold());
        GameManager.CurrencyManager.SetCurrency(Currency.CRYSTAL, _save.GetCrystal());
        GameManager.CurrencyManager.SetCurrency(Currency.STAR, _save.GetStars());
    }

    public void LoadUIBuyCount()
    {
        GameManager.UI_Manager.SetBuyCount(_save.GetBuyCount());
    }

    public void LoadOfflineTime()
    {
        
        var current_time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var time = _save.GetTime();

        var diff = (current_time - time) / 1000;
        var gold = GameManager.CurrencyManager.GetCurrency(Currency.GOLDPERSECOND);

        if (diff > offlineTime && gold > 0)
        {
            GameManager.UI_Manager.ShowOfflinePopup(gold, diff);
        }
        else if(diff > 0 && gold > 0)
        {
            var total_income = gold * (int) diff;
            GameManager.CurrencyManager.AddCurrency(Currency.GOLD, total_income);
        }
        
    }

    public void LoadBuildings()
    {
        var list = _save.GetBuildings();
        
        for (int i = 0; i < GameManager.BuildingManager.BuildingsList.Count; i++)
        {
            var building = GameManager.BuildingManager.BuildingsList[i];

            foreach (var item in list)
            {
                if (building.BuildingScriptableObject.GetID() == item.ID)
                {
                    if (item.Amount > 0 || (i > 0 && GameManager.BuildingManager.BuildingsList[i - 1].Amount > 9))
                    {
                        building.Unlocked = true;
                        building.Amount = item.Amount;
                    } 
                    
                    break;
                }
            }
        }
    }

    public bool LoadBuildingUpdate(int building_ID, int building_update_ID)
    {
        var list = _save.GetBuildingsUpdates();

        foreach (var update in list)
        {
            //Debug.Log($"{update.BuildingID}: {update.ID} {update.Purchased}");
            if (update.BuildingID == building_ID && update.ID == building_update_ID)
            {
                return update.Purchased;
            }
        }
        
        return false;
    }
    
    public void LoadAchievements()
    {
        //GameManager.AchievementManager.AchievementList = _save.GetAchievements();
    }

    public ClickSave LoadClicks()
    {
        return _save.GetClickSave();
    }

    public async Task AsyncSave()
    {
        _save.SetAllData();
        await SaveSystemAPI.SaveAsync("save.bin", _save);
    }
    
    public void Save(bool autoSave = false)
    {
        if (autoSave)
            GameManager.DebugPanel.AddText($"Автосохранение. {autoSaveTimer} сек.");
        
        _save.SetAllData();
        SaveSystemAPI.SaveAsync("save.bin", _save);
    }

    public async void Reset()
    {
        await SaveSystemAPI.DeleteAsync("save.bin");
    }
}

public class Save
{
    [SerializeField] private BuyCount _buy_count;
    [SerializeField] private long _time;
    [SerializeField] private BigDouble _gold;
    [SerializeField] private BigDouble _crystal;
    [SerializeField] private BigDouble _stars;
    [SerializeField] private ClickSave _clickSave = new ClickSave();
    [SerializeField] private List<BuildingSave> _buildings = new List<BuildingSave>();
    [SerializeField] private List<BuildingUpdatesSave> _buildings_updates = new List<BuildingUpdatesSave>();
    [SerializeField] private List<AchievementSave> _achievements = new List<AchievementSave>();

    public void SetAllData()
    {
        _buy_count = GameManager.UI_Manager.GetBuyCount();
        _time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _gold = GameManager.CurrencyManager.GetCurrency(Currency.GOLD);
        _crystal = GameManager.CurrencyManager.GetCurrency(Currency.CRYSTAL);
        _stars = GameManager.CurrencyManager.GetCurrency(Currency.STAR);
        
        _clickSave = new ClickSave
        {
            ClickAmount = GameManager.ClickManager.GetClickAmount(),
            CritClickAmount = GameManager.ClickManager.GetCritClickAmount()
        };
        
        _buildings.Clear();
        _buildings_updates.Clear();
        _achievements.Clear();

        foreach (var building in GameManager.BuildingManager.BuildingsList)
        {
            _buildings.Add(new BuildingSave
            {
                ID = building.BuildingScriptableObject.GetID(),
                Amount = building.Amount
            });
            
            foreach (var update in building.BuildingUpdates)
            {
                _buildings_updates.Add(new BuildingUpdatesSave
                {
                    ID = update.BuildingUpdateScriptableObject.GetID(),
                    BuildingID = building.BuildingScriptableObject.GetID(),
                    Purchased = update.Purchased
                });
            }
            
        }

        foreach (var achievement in GameManager.AchievementManager.AchievementList)
        {
            _achievements.Add(new AchievementSave
            {
                ID = achievement.AchievementScriptableObject.GetID(),
                Unlocked = achievement.Unlocked,
                UnlockDate = achievement.UnlockDate
            });
        }
    }

    public long GetTime() => _time;
    public BigDouble GetGold() => _gold;
    public BigDouble GetCrystal() => _crystal;
    public BigDouble GetStars() => _stars;

    public BuyCount GetBuyCount() => _buy_count;
    public ClickSave GetClickSave() => _clickSave;
    public List<BuildingSave> GetBuildings() => _buildings;
    public List<BuildingUpdatesSave> GetBuildingsUpdates() => _buildings_updates;
    public List<AchievementSave> GetAchievements() => _achievements;

}

public class ClickSave
{
    public BigDouble ClickAmount;
    public BigDouble CritClickAmount;
}

public class BuildingSave
{
    public int ID;
    public BigDouble Amount;
}

public class BuildingUpdatesSave
{
    public int ID;
    public int BuildingID;
    public bool Purchased;
}

public class AchievementSave
{
    public int ID;
    public bool Unlocked;
    public DateTime UnlockDate;
}
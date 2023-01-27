using System;
using System.Threading.Tasks;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClickManager : MonoBehaviour
{
    private static readonly int Click = Animator.StringToHash("Click");
    private Building _building;

    [Header("UI:")] 
    [SerializeField] private Animator _chestClickAnimator;
    [SerializeField] private Button _chestClickBtn;

    [Header("Click data:")] 
    [SerializeField] private BigDouble click_amount = 0;
    [SerializeField] private BigDouble crit_click_amount = 0;
    [Space]
    [SerializeField] private BigDouble click_power = 1f;
    [SerializeField] private BigDouble crit_chance = 5f;
    [SerializeField] private BigDouble crit_bonus = 100f;
    
    public event EventHandler<BigDouble> OnClick;
    public event EventHandler<BigDouble> OnCriticalClick;

    private void Awake()
    {
        GameManager.ClickManager = this;
    }

    public void Init()
    {
        if (_chestClickBtn != null)
        {
            _chestClickBtn.onClick.AddListener(MakeClick);
        }

        var click_save = GameManager.SaveLoadManager.LoadClicks();

        click_amount = click_save.ClickAmount;
        crit_click_amount = click_save.CritClickAmount; 
    }

    public void Subscribe()
    {
        GameManager.DebugPanel.AddText($"Click Manager. Подписываюсь на события");
        
        _building = GameManager.BuildingManager.BuildingsList[0];
        
        _building.OnBuildingBuy += (sender, d) => UpdateData();
        _building.OnBuildingUpdateBuy += (sender, d) => UpdateData();
        
        UpdateData();
    }
    
    public BigDouble GetCritChance() => crit_chance;
    
    public BigDouble GetCritBonus() => crit_bonus;

    public BigDouble GetClickAmount() => click_amount;

    public BigDouble GetCritClickAmount() => crit_click_amount;

    public void Reset()
    {
        click_amount = 0;
        crit_click_amount = 0;
        
        UpdateData();
    }

    private void UpdateData()
    {
        var critical_chance = 5f;
        var critical_bonus = 100f;
        
        click_power = _building.CalculateIncome();
        
        foreach (var update in _building.BuildingUpdates)
        {
            if (update.Purchased)
            {
                switch (update.BuildingUpdateScriptableObject.GetUpdateType())
                {
                    case UpdateType.CRITCHANCE:
                        switch (update.BuildingUpdateScriptableObject.GetUpdateTypeAdd())
                        {
                            case UpdateTypeAdd.ADDITIVE:
                                critical_chance += update.BuildingUpdateScriptableObject.GetMultiply();
                                break;
                            case UpdateTypeAdd.MULTIPLY:
                                critical_chance *= update.BuildingUpdateScriptableObject.GetMultiply();
                                break;
                        }
                        break;
                    case UpdateType.CRITBONUS:
                        switch (update.BuildingUpdateScriptableObject.GetUpdateTypeAdd())
                        {
                            case UpdateTypeAdd.ADDITIVE:
                                critical_bonus += update.BuildingUpdateScriptableObject.GetMultiply();
                                break;
                            case UpdateTypeAdd.MULTIPLY:
                                critical_bonus *= update.BuildingUpdateScriptableObject.GetMultiply();
                                break;
                        }
                        break;
                }
            }
        }

        crit_chance = critical_chance;
        crit_bonus = critical_bonus;
    }
    
    private void MakeClick()
    {
        _chestClickAnimator.SetTrigger(Click);

        click_amount++;
        
        OnClick?.Invoke(this, click_amount);

        var crit = false;
        var result_power = click_power;

        if (Random.Range(1, 101) <= GetCritChance())
        {
            crit = true;
            crit_click_amount++;
            result_power += result_power * (GetCritBonus() / 100);
            OnCriticalClick?.Invoke(this, crit_click_amount);
        }

        var position = new Vector2(3.7f, -1f);
        FloatingText.Create(position, result_power, crit);
        
        GameManager.CurrencyManager.AddCurrency(Currency.GOLD, result_power);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private BigDouble _gold;    
    [SerializeField] private BigDouble _goldPerSecond;
    [SerializeField] private BigDouble _crystal;
    [SerializeField] private BigDouble _stars;
    
    public event EventHandler<BigDouble> OnGoldChanged;
    public event EventHandler<BigDouble> OnGoldPerSecondChanged;
    public event EventHandler<BigDouble> OnCrystalChanged;
    public event EventHandler<BigDouble> OnStarsChanged;
    
    private void Awake()
    {
        GameManager.CurrencyManager = this;
    }

    public void Init()
    {
        GameManager.DebugPanel.AddText($"Currency Manager. Старт.");
    }

    public BigDouble GetCurrency(Currency currency)
    {
        switch (currency)
        {
            case Currency.GOLD:
                return _gold;
            case Currency.GOLDPERSECOND:
                return _goldPerSecond;
            case Currency.STAR:
                return _stars;
            case Currency.CRYSTAL:
                return _crystal;
            default:
                return 0;
        }
    }
    
    public void AddCurrency(Currency currency, BigDouble value)
    {
        switch (currency)
        {
            case Currency.GOLD:
                _gold += value;
                OnGoldChanged?.Invoke(null, _gold);
                break;
            case Currency.CRYSTAL:
                _crystal += value;
                OnCrystalChanged?.Invoke(null, _crystal);
                break;
            case Currency.STAR:
                _stars += value;
                OnStarsChanged?.Invoke(null, _stars);
                break;
        }
    }
    
    public void SetCurrency(Currency currency, BigDouble value)
    {
        switch (currency)
        {
            case Currency.GOLD:
                _gold = value;
                OnGoldChanged?.Invoke(null, _gold);
                break;
            case Currency.GOLDPERSECOND:
                _goldPerSecond = value;
                OnGoldPerSecondChanged?.Invoke(null, _goldPerSecond);
                break;
            case Currency.CRYSTAL:
                _crystal = value;
                OnCrystalChanged?.Invoke(null, _crystal);
                break;
            case Currency.STAR:
                _stars = value;
                OnStarsChanged?.Invoke(null, _stars);
                break;
        }
    }

    public void UpdateGoldIncome()
    {
        GameManager.CurrencyManager.SetCurrency(Currency.GOLDPERSECOND, GameManager.BuildingManager.GetTotalIncome());
    }

    public BigDouble GetStarMultiply()
    {
        return (_stars * 0.01) + 1;
    }

    public void Reset()
    {
        SetCurrency(Currency.GOLD, 0);
        SetCurrency(Currency.STAR, 0);
        SetCurrency(Currency.WOOD, 0);
        SetCurrency(Currency.STONE, 0);
        SetCurrency(Currency.COPPER, 0);
    }
}

public enum Currency
{
    GOLD, 
    GOLDPERSECOND, 
    CRYSTAL,
    STAR,
    WOOD,
    STONE,
    IRON,
    COPPER,
}

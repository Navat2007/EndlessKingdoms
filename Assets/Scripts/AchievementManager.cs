using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    
    [SerializeField] private Button _showBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _achievement_container;
    [SerializeField] private GameObject _achievement_prefab;

    [Space] 
    [SerializeField] private List<AchievementScriptableObject> AchievementClickSO = new List<AchievementScriptableObject>();
    [SerializeField] private List<AchievementScriptableObject> AchievementBuildingSO = new List<AchievementScriptableObject>();
    public List<Achievement> AchievementList = new List<Achievement>();
    
    private void Awake()
    {
        GameManager.AchievementManager = this;
    }

    public void Init()
    {
        if (_showBtn != null)
            _showBtn.onClick.AddListener(() => { _panel.SetActive(true); });
        
        if (_closeBtn != null)
            _closeBtn.onClick.AddListener(() => { _panel.SetActive(false); });

        foreach (var achievementScriptableObject in AchievementClickSO)
        {
            
            var item = Instantiate(_achievement_prefab, _achievement_container, false);
            var achievement = new Achievement
            {
                AchievementType = achievementScriptableObject.achievementType,
                AchievementScriptableObject = achievementScriptableObject,
                AchievementItemUI = item.GetComponent<AchievementItemUI>(),
            };
            AchievementList.Add(achievement);
            
            achievement.Init();
            
        }
    }
    
    public void Subscribe()
    {
        GameManager.DebugPanel.AddText($"Achievement Manager. Подписываюсь на события");

        GameManager.ClickManager.OnClick += (sender, d) => CheckAchievement(AchievementType.CLICK_AMOUNT, d);
        GameManager.ClickManager.OnCriticalClick += (sender, d) => CheckAchievement(AchievementType.CLICK_CRIT_AMOUNT, d);

    }

    public void Reset()
    {
        foreach (var achievement in AchievementList)
        {
            achievement.Unlocked = false;
            achievement.Init();
        }
    }

    public void CheckAchievement(AchievementType achievementType, BigDouble amount)
    {
        switch (achievementType)
        {
            case AchievementType.CLICK_AMOUNT:
                foreach (var achievement in AchievementList.Where(a => a.AchievementType == AchievementType.CLICK_AMOUNT))
                {
                    if (!achievement.Unlocked)
                    {
                        if (amount >= achievement.AchievementScriptableObject.amount)
                        {
                            achievement.Unlocked = true;
                            GameManager.MessageQueueManager.AddToQueue(
                                MessagesType.ACHIEVEMENT, 
                                achievement.AchievementScriptableObject.GetTitle(), 
                                achievement.AchievementScriptableObject.GetDescription()
                                );
                        }
                            
                    }
                }
                break;
            case AchievementType.CLICK_CRIT_AMOUNT:
                break;
        }
    }
}

public enum AchievementType
{
    CLICK_AMOUNT,
    CLICK_CRIT_AMOUNT,
    CLICK_PER_SEC,
    GOLD_AMOUNT,
    GOLD_PER_SEC,
    STARS_AMOUNT,
    CRYSTAL_AMOUNT,
    BUILDING_AMOUNT,
}

[Serializable]
public class Achievement
{
    public AchievementType AchievementType;
    public AchievementItemUI AchievementItemUI;
    public AchievementScriptableObject AchievementScriptableObject;
    public bool Unlocked;
    public DateTime UnlockDate;

    public void Init()
    {
        AchievementItemUI.image.sprite = AchievementScriptableObject.image;
    }

}

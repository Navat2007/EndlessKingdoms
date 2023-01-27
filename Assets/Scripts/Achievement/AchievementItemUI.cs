using UnityEngine;
using UnityEngine.UI;

public class AchievementItemUI : MonoBehaviour
{
    public Image image;
    public GameObject check;

    public void SetCheckVisible(bool visible)
    {
        check.SetActive(visible);
    }
}

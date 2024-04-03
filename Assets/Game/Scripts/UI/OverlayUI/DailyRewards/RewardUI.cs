using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [Header("Reward")]
    [SerializeField] private GameObject _dollarsImage;
    [SerializeField] private GameObject _diamondsImage;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Image _border;

    [Header("Closed")]
    [SerializeField] private GameObject _closedPanel;
    [SerializeField] private TextMeshProUGUI _dayCount;
    [SerializeField] private Color _unlockedColor;
    [SerializeField] private Color _collectedCollor;

    public DailyRewardsUI.Reward Reward { get; private set; }

    public void Initialize(DailyRewardsUI.Reward reward, int dayCount)
    {
        Reward = reward;

        _dollarsImage.SetActive(false);
        _diamondsImage.SetActive(false);

        _dayCount.text = "DAY " + dayCount.ToString();

        _closedPanel.SetActive(true);

        if (reward.ResourceType == ResourceType.Dollars)
        {
            _dollarsImage.SetActive(true);
            _valueText.text = reward.value.ToString();
        }
        else if (reward.ResourceType == ResourceType.Diamonds)
        {
            _diamondsImage.SetActive(true);
            _valueText.text = reward.value.ToString();
        }
    }

    public void Unlock()
    {
        _closedPanel.SetActive(false);

        _border.color = _unlockedColor;
    }

    public void Collect()
    {
        Unlock();

        _border.color = _collectedCollor;
    }
}
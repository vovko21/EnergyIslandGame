using TMPro;
using UnityEngine;

public class TaskItemUI : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("Interactive")]
    [SerializeField] private ProgressBarView _progressBar;
    [SerializeField] private ButtonUI _button;

    [Header("Reward")]
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private GameObject _dollar;
    [SerializeField] private GameObject _diamonds;

    public void SetData(GameTask gameTask)
    {
        _nameText.text = gameTask.Name;

        _progressBar.SetDataText($"{gameTask.ProgressCurrent}/{gameTask.ProgressTarget}");
        _progressBar.SetMaxValue(gameTask.ProgressTarget);
        _progressBar.SetValue(gameTask.ProgressCurrent);

        SetRewards(gameTask.ResourceType, gameTask.RewardValue);   

        if(gameTask.IsCompleted)
        {
            _button.SetActive();
        }
        else
        {
            _button.SetInactive();
        }
    }

    private void SetRewards(ResourceType type, int value)
    {
        _dollar.SetActive(false);
        _diamonds.SetActive(false);

        switch (type)
        {
            case ResourceType.Dollars:
                _dollar.SetActive(true);
                break;
            case ResourceType.Diamonds:
                _diamonds.SetActive(true);
                break;
            default:
                break;
        }

        _rewardText.text = value.ToString();
    }
}

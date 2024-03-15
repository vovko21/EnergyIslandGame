using System;
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
    [SerializeField] private GameObject _claimedBorder;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private GameObject _dollar;
    [SerializeField] private GameObject _diamonds;

    private GameTask _gameTask;

    private void OnEnable()
    {
        _button.Button.onClick.AddListener(ButtonClicked);
    }

    private void OnDisable()
    {
        _button.Button.onClick.RemoveAllListeners();
    }

    public void SetData(GameTask gameTask)
    {
        _gameTask = gameTask;

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

        if (gameTask.isTook)
        {
            _button.SetInactive();

            _claimedBorder.SetActive(true);
        }
        else
        {
            _claimedBorder.SetActive(false);
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

    private void ButtonClicked()
    {
        switch (_gameTask.ResourceType)
        {
            case ResourceType.Dollars:
                ProgressionManager.Instance.Wallet.AddDollars(_gameTask.RewardValue);
                break;
            case ResourceType.Diamonds:
                ProgressionManager.Instance.Wallet.AddDiamands(_gameTask.RewardValue);
                break;
            default:
                break;
        }

        _gameTask.isTook = true;
        _button.SetInactive();
        _claimedBorder.SetActive(true);
    }
}

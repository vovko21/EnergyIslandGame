using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class DailyRewardsUI : MonoBehaviour
{
    [Serializable]
    public struct Reward
    {
        public ResourceType ResourceType;
        public int value;
    }

    [Header("Main")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _calendarDayText;

    [Header("Reward Items")]
    [SerializeField] private GameObject _rewardPrefab;
    [SerializeField] private Transform _rewardsConteiner;

    [Header("Butons")]
    [SerializeField] private ButtonUI _takeRewardButton;
    [SerializeField] private ButtonUI _takeReward2xButton;

    [Header("Data")]
    [SerializeField] private List<Reward> _rewards;

    private List<RewardUI> _instantiatedRewards = new List<RewardUI>();
    private DateTime? _lastClaimTime;
    private DateTime _nextClaimTime;
    private IEnumerator _timerCoroutine;
    private int _daysInRow = 0;

    public int DaysInRow => _daysInRow;
    public DateTime? LastClaimTime => _lastClaimTime;

    private void OnEnable()
    {
        _takeRewardButton.Button.onClick.AddListener(OnButtonTakeClicked);
        _takeReward2xButton.Button.onClick.AddListener(OnButtonTake2xClicked);

        if (TimeManager.Instance.IsServerTimeSuccess)
        {
            if (_timerCoroutine == null)
            {
                _timerCoroutine = StartCoutTime();
                StartCoroutine(_timerCoroutine);
            }
            else
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = StartCoutTime();
                StartCoroutine(_timerCoroutine);
            }
        }

        if (CanBeClaimed())
        {
            _takeRewardButton.SetActive();
            _takeReward2xButton.SetActive();
        }
        else
        {
            _takeRewardButton.SetInactive();
            _takeReward2xButton.SetInactive();
        }
    }

    private void OnDisable()
    {
        _takeRewardButton.Button.onClick.RemoveAllListeners();
        _takeReward2xButton.Button.onClick.RemoveAllListeners();

        StopCoroutine(_timerCoroutine);
    }

    public void Initialize()
    {
        //Initialize reward items if they are not
        if (_instantiatedRewards.Count == 0)
        {
            for (int i = 0; i < _rewards.Count; i++)
            {
                var instantiatedItem = Instantiate(_rewardPrefab, _rewardsConteiner).GetComponent<RewardUI>();
                instantiatedItem.Initialize(_rewards[i], i + 1);
                _instantiatedRewards.Add(instantiatedItem);
            }
        }

        //Initialize current panel
        _calendarDayText.text = _daysInRow.ToString();

        if (CanBeClaimed())
        {
            _takeRewardButton.SetActive();
            _takeReward2xButton.SetActive();
        }
        else
        {
            _takeRewardButton.SetInactive();
            _takeReward2xButton.SetInactive();
        }

        //Unlock next
        if (!IsMaxCollected())
        {
            _instantiatedRewards[_daysInRow].Unlock();
        }

        //Rewarded button state
        if (!AdsManager.Instance.RewardedAds.IsLoaded)
        {
            _takeReward2xButton.SetInactive();
        }
        else
        {
            _takeReward2xButton.SetActive();
        }
    }

    public void Initialize(int daysInRow, DateTime? lastClaimTime)
    {
        if (daysInRow > _rewards.Count)
        {
            _daysInRow = _rewards.Count;
        }
        else
        {
            _daysInRow = daysInRow;
        }

        _lastClaimTime = lastClaimTime;

        Initialize();

        for (int i = 0; i < _daysInRow; i++)
        {
            _instantiatedRewards[i].Collect();
        }

        if (!IsMaxCollected())
        {
            _instantiatedRewards[_daysInRow].Unlock();
        }
    }

    private void OnButtonTakeClicked()
    {
        if (TimeManager.Instance.IsServerTimeSuccess)
        {
            Claim();
        }
    }

    private void OnButtonTake2xClicked()
    {
        if(!AdsManager.Instance.NoAds)
        {
            if (!AdsManager.Instance.RewardedAds.IsLoaded) return;

            AdsManager.Instance.RewardedAds.OnAdComplete += RewardedAds_OnAdComplete;

            AdsManager.Instance.RewardedAds.ShowAd();
        }
        else
        {
            if (TimeManager.Instance.IsServerTimeSuccess)
            {
                Claim(isDoubled: true);
            }
        }
    }

    private void RewardedAds_OnAdComplete()
    {
        AdsManager.Instance.RewardedAds.OnAdComplete -= RewardedAds_OnAdComplete;

        Debug.Log("<color=green>Rewarded Ad complete</color>");

        if (TimeManager.Instance.IsServerTimeSuccess)
        {
            Claim(isDoubled: true);
        }
    }

    private void Claim(bool isDoubled = false)
    {
        if (!CanBeClaimed()) return;

        var currentReward = _instantiatedRewards[_daysInRow].Reward;

        if (currentReward.ResourceType == ResourceType.Dollars)
        {
            ProgressionManager.Instance.Wallet.AddDollars(isDoubled ? currentReward.value * 2 : currentReward.value);
        }
        if (currentReward.ResourceType == ResourceType.Diamonds)
        {
            ProgressionManager.Instance.Wallet.AddDiamands(isDoubled ? currentReward.value * 2 : currentReward.value);
        }

        _instantiatedRewards[_daysInRow].Collect();

        _daysInRow++;

        //Update ui text
        _calendarDayText.text = _daysInRow.ToString();

        //Unlock next
        if (!IsMaxCollected())
        {
            _instantiatedRewards[_daysInRow].Unlock();
        }

        _takeRewardButton.SetInactive();
        _takeReward2xButton.SetInactive();

        _lastClaimTime = TimeManager.Instance.LocalDateTime;
        _nextClaimTime = NextTimeFrom(_lastClaimTime.Value);
    }

    public void Deinitialize()
    {
        foreach (var item in _instantiatedRewards)
        {
            Destroy(item);
        }

        _instantiatedRewards.Clear();
    }

    public bool CanBeClaimed()
    {
        if (!TimeManager.Instance.IsServerTimeSuccess) return false;

        if (_lastClaimTime.HasValue)
        {
            if (NextTimeFrom(_lastClaimTime.Value) > TimeManager.Instance.LocalDateTime) return false;
        }

        if (IsMaxCollected()) return false;

        return true;
    }

    private IEnumerator StartCoutTime()
    {
        if (_lastClaimTime == null)
        {
            _nextClaimTime = TimeManager.Instance.LocalDateTime;
        }
        else
        {
            _nextClaimTime = NextTimeFrom(_lastClaimTime.Value);
        }

        while (true)
        {
            var dateTimeDifference = _nextClaimTime - TimeManager.Instance.LocalDateTime;
            if (dateTimeDifference.Ticks >= 0)
            {
                _timerText.text = dateTimeDifference.ToString("hh\\:mm\\:ss");
            }
            if (dateTimeDifference.Ticks <= 0)
            {
                if (CanBeClaimed())
                {
                    _takeRewardButton.SetActive();
                    _takeReward2xButton.SetActive();
                }
                else
                {
                    _takeRewardButton.SetInactive();
                    _takeReward2xButton.SetInactive();
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private DateTime NextTimeFrom(DateTime dateTime)
    {
        return dateTime.AddDays(1);
    }

    private bool IsMaxCollected()
    {
        if (_daysInRow < _instantiatedRewards.Count)
        {
            return false;
        }

        return true;
    }
}
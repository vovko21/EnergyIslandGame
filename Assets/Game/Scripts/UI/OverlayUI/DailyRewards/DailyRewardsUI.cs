using System.Collections.Generic;
using System;
using UnityEngine;

public class DailyRewardsUI : MonoBehaviour
{
    public struct Reward
    {
        public ResourceType ResourceType;
        public int value;
    }

    [Header("Data")]
    [SerializeField] private List<Reward> _rewards;

    private int _daysInRaw;

    private DateTime _lastClaimTime;
    private bool _canClaim = false;

    private void Start()
    {
        InitializeRewards();
    }

    private void InitializeRewards()
    {

    }
}

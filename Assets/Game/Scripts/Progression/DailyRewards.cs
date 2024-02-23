using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewards : MonoBehaviour
{
    public struct Reward
    {
        public ResourceType ResourceType;
        public int value;
    }
    [Header("Data")]
    [SerializeField] private List<Reward> _rewards;
    [Header("UI")]
    [SerializeField] private GameObject _rewardsParent;

    private bool _canClaim = false;
    private DateTime _lastClaimTime;

    private void Start()
    {
        InitializeRewards();
    }

    private void InitializeRewards()
    {
        throw new NotImplementedException();
    }
}

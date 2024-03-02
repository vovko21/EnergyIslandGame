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

    //[Header("UI")]
    //[SerializeField] private UserInterface _ui;

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

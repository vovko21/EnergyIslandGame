using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private GameObject _dollarsImage;

    public void Initialize(DailyRewards.Reward reward)
    {
        _textMeshPro.text = reward.value.ToString();
    }
}

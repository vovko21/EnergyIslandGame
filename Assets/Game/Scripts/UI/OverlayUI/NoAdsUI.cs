using UnityEngine;

public class NoAdsUI : MonoBehaviour
{
    [SerializeField] private ButtonUI _buyButton;

    private void OnEnable()
    {
        _buyButton.Button.onClick.AddListener(OnBuyNoAdsClicked);
    }

    private void OnDisable()
    {
        _buyButton.Button.onClick.RemoveListener(OnBuyNoAdsClicked);
    }

    private void OnBuyNoAdsClicked()
    {
        //IAPManager.Instance.InitiatePurchaseNoAds();
    }
}

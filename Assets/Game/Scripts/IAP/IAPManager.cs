using System;
using UnityEngine;
using UnityEngine.Purchasing;

//[Serializable]
//public class PurchaseItem
//{
//    public string Name;
//    public string Id;
//    public ProductType ProductType;
//    public string Description;
//    public float Price;
//}

//public class IAPManager : SingletonMonobehaviour<IAPManager>, IStoreListener
//{
//    [SerializeField] private PurchaseItem _noAdsItem;

//    private Product _noAdsProduct;
//    private IStoreController _storeController;

//    public Product NoAds => _noAdsProduct;

//    protected override void Awake()
//    {
//        base.Awake();

//        Initialiize();
//    }

//    public void Initialiize()
//    {
//        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//        builder.AddProduct(_noAdsItem.Id, _noAdsItem.ProductType);

//        UnityPurchasing.Initialize(this, builder);
//    }

//    #region Store Callbacks

//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        _storeController = controller;
//        _noAdsProduct = _storeController.products.WithID(_noAdsItem.Id);

//        RestoreNoAdsPurchase();
//    }

//    public void OnInitializeFailed(InitializationFailureReason error)
//    {
//        Debug.Log("Initialize Failed " + error.ToString());
//    }

//    public void OnInitializeFailed(InitializationFailureReason error, string message)
//    {
//        Debug.Log("Initialize Failed " + error.ToString());
//    }

//    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//    {
//        Debug.Log("Purchase failed " + failureReason.ToString());
//    }

//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
//    {
//        var product = purchaseEvent.purchasedProduct;

//        if(product.definition.id == _noAdsItem.Id)
//        {
//            AdsManager.Instance.SetNoAds(true);
//            Debug.Log("<color=green>Purchased</color>");
//        }

//        return PurchaseProcessingResult.Complete;
//    }
//    #endregion

//    public void InitiatePurchaseNoAds()
//    {
//        _storeController.InitiatePurchase(_noAdsItem.Id);
//    }

//    private void RestoreNoAdsPurchase()
//    {
//        if (_storeController == null) return;

//        var product = _storeController.products.WithID(_noAdsItem.Id);

//        if(product == null) return;

//        if(product.hasReceipt)
//        {
//            AdsManager.Instance.SetNoAds(true);
//        }
//        else
//        {
//            AdsManager.Instance.SetNoAds(false);
//        }
//    }
//}

public class IAPManager : MonoBehaviour
{
    private const string NOADS_ID = "com.energygame.noads";

    public void OnPurchaseComplete(Product product)
    {
        switch (product.definition.id)
        {
            case NOADS_ID:
                RemoveAds();
                break;
        }
    }

    private void RemoveAds()
    {
        AdsManager.Instance.SetNoAds(true);

        Debug.Log("<color=green>Purchased No ads</color>");
    }
}

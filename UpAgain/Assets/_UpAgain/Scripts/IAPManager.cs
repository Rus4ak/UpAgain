using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;

public enum IAPProductKey
{
    Coin100, Coin300, Coin500, Coin1000, RemoveAds
}

public class IAPManager : MonoBehaviour
{
    public static IAPManager Instance {  get; private set; }
    
    private const string COIN100 = "coin100";
    private const string COIN300 = "coin300";
    private const string COIN500 = "coin500";
    private const string COIN1000 = "coin1000";
    private const string REMOVE_ADS = "remove_ads";

    private StoreController _storeController;

    public bool IsInitialized { get; private set; } = false;
    public string Coin100Price {  get; private set; }
    public string Coin300Price { get; private set; }
    public string Coin500Price { get; private set; }
    public string Coin1000Price { get; private set; }
    public string RemoveAdsPrice { get; private set; }

    private async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        await InitIAP();
    }

    private async Task InitIAP()
    {
        try
        {
            var option = new InitializationOptions().SetEnvironmentName("production");
            await UnityServices.InitializeAsync(option);

            _storeController = UnityIAPServices.StoreController();

            _storeController.OnStoreDisconnected += OnStoreDisconnected;
            _storeController.OnProductsFetched += OnProductsFetched;
            _storeController.OnProductsFetchFailed += OnProductsFetchFailed;
            _storeController.OnPurchasesFetched += OnPurchasesFetched;
            _storeController.OnPurchasesFetchFailed += OnPurchasesFetchFailed;
            _storeController.OnPurchasePending += OnPurchasePending;
            _storeController.OnPurchaseConfirmed += OnPurchaseConfirmed;
            _storeController.OnPurchaseFailed += OnPurchaseFailed;
            _storeController.OnPurchaseDeferred += OnPurchaseDeferred;

            RegisterEntitlementCallback();

            await _storeController.Connect();

            var initialProductToFetch = BuildProductDefinitios();
            _storeController.FetchProducts(initialProductToFetch);
        }
        catch (Exception e)
        {
            Debug.Log($"Initialization failed with: {e}");
        }
    }

    private List<ProductDefinition> BuildProductDefinitios()
    {
        var initialProductToFetch = new List<ProductDefinition>();

        initialProductToFetch.Add(new ProductDefinition(COIN100, ProductType.Consumable));
        initialProductToFetch.Add(new ProductDefinition(COIN300, ProductType.Consumable));
        initialProductToFetch.Add(new ProductDefinition(COIN500, ProductType.Consumable));
        initialProductToFetch.Add(new ProductDefinition(COIN1000, ProductType.Consumable));
        initialProductToFetch.Add(new ProductDefinition(REMOVE_ADS, ProductType.NonConsumable));

        return initialProductToFetch;
    }

    private void RegisterEntitlementCallback()
    {
        _storeController.OnCheckEntitlement += (result) =>
        {
            UnityEngine.Purchasing.Product product = result.Product;
            var status = result.Status;

            Debug.Log($"Product is {product}, Entitle Status is {status}");

            bool isEntitled = status == EntitlementStatus.FullyEntitled;

            if (isEntitled)
            {
                if (product.definition.id == REMOVE_ADS)
                {
                    AdsData.isShow = false;
                }
            }
        };
    }

    private void OnStoreDisconnected(StoreConnectionFailureDescription description)
    {
        Debug.Log($"Initialization/Connection failed: {description.message}");
    }

    private void OnProductsFetched(List<UnityEngine.Purchasing.Product> products)
    {
        _storeController.FetchPurchases();

        foreach (var product in products)
        {
            string price = product.metadata.localizedPrice + " " + product.metadata.isoCurrencyCode;

            switch (product.definition.id)
            {
                case COIN100:
                    Coin100Price = price;
                    break;
                case COIN300:
                    Coin300Price = price;
                    break;
                case COIN500:
                    Coin500Price = price;
                    break;
                case COIN1000:
                    Coin1000Price = price;
                    break;
                case REMOVE_ADS:
                    RemoveAdsPrice = price;
                    break;
            }
        }
    }

    private void OnProductsFetchFailed(ProductFetchFailed reason)
    {
        Debug.Log($"Product fetch failed: {reason}");
    }

    private void OnPurchasesFetched(Orders orders)
    {
        IsInitialized = true;

        foreach (var product in _storeController.GetProducts())
        {
            _storeController.CheckEntitlement(product);
        }
    }

    private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription reason)
    {
        Debug.Log($"Purchases fetch failed: {reason.message}");
    }

    public void BuyProduct(IAPProductKey productKey)
    {
        if (!IsInitialized)
        {
            Debug.Log("IAP Module is not initialized. Try again some time.");
            return;
        }

        switch (productKey)
        {
            case IAPProductKey.Coin100:
                _storeController.PurchaseProduct(COIN100);
                break;
            case IAPProductKey.Coin300:
                _storeController.PurchaseProduct(COIN300);
                break;
            case IAPProductKey.Coin500:
                _storeController.PurchaseProduct(COIN500);
                break;
            case IAPProductKey.Coin1000:
                _storeController.PurchaseProduct(COIN1000);
                break;
            case IAPProductKey.RemoveAds:
                _storeController.PurchaseProduct(REMOVE_ADS);
                break;
        }
    }

    private void OnPurchasePending(PendingOrder order)
    {
        Debug.Log($"Pending order: {order}");
        _storeController.ConfirmPurchase(order);
    }

    private void OnPurchaseConfirmed(Order order)
    {
        Debug.Log($"Purchase confirmed: {order}");

        if (order?.Info?.PurchasedProductInfo != null && order.Info.PurchasedProductInfo.Count > 0)
        {
            string productId = order.Info.PurchasedProductInfo[0].productId;

            if (productId == REMOVE_ADS)
            {
                FindFirstObjectByType<NoAds>().Buy();
            }
            else if (productId == COIN100)
            {
                FindFirstObjectByType<Shop>().BuyCoin(100);
            }
            else if (productId == COIN300)
            {
                FindFirstObjectByType<Shop>().BuyCoin(300);
            }
            else if (productId == COIN500)
            {
                FindFirstObjectByType<Shop>().BuyCoin(500);
            }
            else if (productId == COIN1000)
            {
                FindFirstObjectByType<Shop>().BuyCoin(1000);
            }
        }
    }

    private void OnPurchaseFailed(FailedOrder failedOrder)
    {
        if (failedOrder?.Info?.PurchasedProductInfo == null || failedOrder.Info.PurchasedProductInfo.Count == 0)
        {
            Debug.Log($"Purchase failed but no product info available");
            return;
        }

        var productId = failedOrder.Info.PurchasedProductInfo[0].productId;
        var reason = failedOrder.FailureReason;
        var message = failedOrder.Details;

        Debug.Log($"Purchase failed. Product is {productId}. Reason is {reason}. Here is message {message}");
    }

    private void OnPurchaseDeferred(DeferredOrder deferredOrder)
    {
        Debug.Log($"Purchase Deferred for product: {deferredOrder?.Info}");
    }
}

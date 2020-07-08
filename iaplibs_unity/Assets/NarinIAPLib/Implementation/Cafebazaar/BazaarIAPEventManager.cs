using Narin.Unity.IAP;
using System;
using UnityEngine;
using BazaarPlugin;
using System.Collections.Generic;

public class BazaarIAPEventManager : MonoBehaviour, IIAPEventManager {
    public event EventHandler<EventArgs>                OnPurchaseSupported;
    public event EventHandler<ErrorEventArgs>           OnPurchaseNotSupported;
    public event EventHandler<PurchaseEventArgs>        OnPurchaseSucceeded;
    public event EventHandler<ErrorEventArgs>           OnPurchaseFailed;
    public event EventHandler<PurchaseEventArgs>        OnConsumeSucceeded;
    public event EventHandler<ErrorEventArgs>           OnConsumeFailed;
    public event EventHandler<QuerySkuInfoEventArgs>    OnQuerySkuInfoSucceeded;
    public event EventHandler<ErrorEventArgs>           OnQuerySkuInfoFailed;
    public event EventHandler<EventArgs>                OnRetriveFailedPurchasesSucceeded;
    public event EventHandler<ErrorEventArgs>           OnRetriveFailedPurchasesFailed;

    void OnEnable() {
        IABEventManager.billingSupportedEvent           += BillingSupportedEventHandler;
        IABEventManager.billingNotSupportedEvent        += BillingNotSupportedEventHandler;
        IABEventManager.purchaseSucceededEvent          += PurchaseSucceededEventHandler;
        IABEventManager.purchaseFailedEvent             += PurchaseFailedEventHandler;
        IABEventManager.consumePurchaseSucceededEvent   += ConsumePurchaseSucceededEventHandler;
        IABEventManager.consumePurchaseFailedEvent      += ConsumePurchaseFailedEventHandler;
        IABEventManager.querySkuDetailsSucceededEvent   += QuerySkuDetailsSucceededEventHandler;
        IABEventManager.querySkuDetailsFailedEvent      += QuerySkuDetailsFailedEventHandler;
        IABEventManager.queryPurchasesSucceededEvent    += QueryPurchasesSucceededEventHandler;
        IABEventManager.queryPurchasesFailedEvent       += QueryPurchasesFailedEventHandler;
    }

    private ProductType ConvertProductType(string typeStr) {
        ProductType ret = ProductType.NotDefined;

        if(typeStr == "Consumable")     ret = ProductType.Consumable;

        if(typeStr == "NonConsumable")  ret = ProductType.NonConsumable;

        if(typeStr == "Subscription")   ret = ProductType.Subscription;

        return ret;
    }

    private void BillingSupportedEventHandler() {
        if(null != OnPurchaseSupported)
            OnPurchaseSupported(this, new EventArgs());
    }

    private void BillingNotSupportedEventHandler(string error) {
        if(null != OnPurchaseNotSupported)
            OnPurchaseNotSupported(this, new ErrorEventArgs(error));
    }

    private void QuerySkuDetailsSucceededEventHandler(List<BazaarSkuInfo> skusInfo) {
        List<SkuInfo> ret = new List<SkuInfo>(skusInfo.Count);

        if(null != skusInfo && skusInfo.Count != 0) {
            skusInfo.ForEach((sku) => {
                ret.Add(new SkuInfo(sku.ProductId
                    , sku.Price
                    , sku.Title
                    , sku.Description
                    , "IRR"
                    , ConvertProductType(sku.Type)
                    ));
            });
        }

        if(null != OnQuerySkuInfoSucceeded) {
            OnQuerySkuInfoSucceeded(this, new QuerySkuInfoEventArgs(ret));
        }
    }

    private void QuerySkuDetailsFailedEventHandler(string error) {
        if(null != OnQuerySkuInfoFailed) {
            OnQuerySkuInfoFailed(this, new ErrorEventArgs(error));
        }
    }

    private void PurchaseSucceededEventHandler(BazaarPurchase purchaseResult) {
        if(null != OnPurchaseSucceeded) {
            OnPurchaseSucceeded(this, new PurchaseEventArgs(
                 purchaseResult.DeveloperPayload
                ,purchaseResult.PackageName
                ,purchaseResult.ProductId
                ,purchaseResult.PurchaseToken
                ,ConvertProductType(purchaseResult.Type)
                ));
        }
    }

    private void PurchaseFailedEventHandler(string error) {
        if(null != OnPurchaseFailed) {
            OnPurchaseFailed(this, new ErrorEventArgs(error));
        }
    }

    private void ConsumePurchaseSucceededEventHandler(BazaarPurchase purchase) {
        if(null != OnConsumeSucceeded) {
                 OnConsumeSucceeded(this, new PurchaseEventArgs(
                 purchase.DeveloperPayload
                ,purchase.PackageName
                ,purchase.ProductId
                ,purchase.PurchaseToken
                ,ConvertProductType(purchase.Type)
                ));
        }
    }

    private void ConsumePurchaseFailedEventHandler(string error) {
        if(null != OnConsumeFailed) {
            OnConsumeFailed(this, new ErrorEventArgs(error));
        }
    }

    private void QueryPurchasesSucceededEventHandler(List<BazaarPurchase> purchases) {
        throw new NotImplementedException();
    }

    private void QueryPurchasesFailedEventHandler(string error) {
        throw new NotImplementedException();
    }
}
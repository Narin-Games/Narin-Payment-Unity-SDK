#if _dev_ || _cafebazaar_

using System.Collections.Generic;
using UnityEngine;
using BazaarPlugin;
using System;

namespace Narin.Unity.IAP {
    public partial class IAPBuilder {

        private class BazaarIAPManager : MonoBehaviour, IIAPManager {
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

            private string _publicKey;
            private List<ProductBase> _products;
            private bool _isInitialized = false;

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

            void OnDisable() {
                IABEventManager.billingSupportedEvent           -= BillingSupportedEventHandler;
                IABEventManager.billingNotSupportedEvent        -= BillingNotSupportedEventHandler;
                IABEventManager.purchaseSucceededEvent          -= PurchaseSucceededEventHandler;
                IABEventManager.purchaseFailedEvent             -= PurchaseFailedEventHandler;
                IABEventManager.consumePurchaseSucceededEvent   -= ConsumePurchaseSucceededEventHandler;
                IABEventManager.consumePurchaseFailedEvent      -= ConsumePurchaseFailedEventHandler;
                IABEventManager.querySkuDetailsSucceededEvent   -= QuerySkuDetailsSucceededEventHandler;
                IABEventManager.querySkuDetailsFailedEvent      -= QuerySkuDetailsFailedEventHandler;
                IABEventManager.queryPurchasesSucceededEvent    -= QueryPurchasesSucceededEventHandler;
                IABEventManager.queryPurchasesFailedEvent       -= QueryPurchasesFailedEventHandler;
            }

            public void SetData(string publicKey, ProductBase[] products) { 
                _publicKey = publicKey;
                _products = new List<ProductBase>(products);
            }

            #region _iap_api_
            public void Init() {
                BazaarIAB.init(_publicKey);
            }

            public void QuaryInventory(string[] skus) {
                BazaarIAB.queryInventory(skus);
            }
        
            public void PurchaseProduct(string sku) {
                BazaarIAB.purchaseProduct(sku);
            }
        
            public void ConsumeProduct(string sku) {
                BazaarIAB.consumeProduct(sku);
            }
        
            public void QuerySkuInfo(string[] skus) {
                BazaarIAB.querySkuDetails(skus);
            }
        
            public void RetrieveFailedPurchases() {
                throw new System.NotImplementedException();
            }
            #endregion

            #region _event_listeners_
            private void BillingSupportedEventHandler() {
                if(null != OnPurchaseSupported)
                    OnPurchaseSupported(this, new EventArgs());
            }

            private void BillingNotSupportedEventHandler(string error) {
                if(null != OnPurchaseNotSupported)
                    OnPurchaseNotSupported(this, new ErrorEventArgs(error));
            }

            private void QuerySkuDetailsSucceededEventHandler(List<BazaarSkuInfo> skusInfo) {
                List<ProductDetail> ret = new List<ProductDetail>(skusInfo.Count);

                if(null != skusInfo && skusInfo.Count != 0) {
                    skusInfo.ForEach((sku) => {
                        ret.Add(new ProductDetail(sku.ProductId
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
            #endregion

            private ProductType ConvertProductType(string typeStr) {
                ProductType ret = ProductType.NotDefined;

                if(typeStr == "Consumable")     ret = ProductType.Consumable;
                if(typeStr == "NonConsumable")  ret = ProductType.NonConsumable;
                if(typeStr == "Subscription")   ret = ProductType.Subscription;

                return ret;
            }
        }
    }
}

#endif
#if _dev_ || _myket_

using System;
using UnityEngine;
using MyketPlugin;
using System.Collections.Generic;

namespace Narin.Unity.IAP {
    public partial class IAPBuilder {

        private class MyketIAPManager : SingletonMono<MyketIAPManager>, IIAPManager {
            public event EventHandler<EventArgs>                    OnPurchaseSupported;
            public event EventHandler<ErrorEventArgs>               OnPurchaseNotSupported;
            public event EventHandler<PurchaseEventArgs>            OnPurchaseSucceeded;
            public event EventHandler<ErrorEventArgs>               OnPurchaseFailed;
            public event EventHandler<QuerySkuInfoEventArgs>        OnQuerySkuInfoSucceeded;
            public event EventHandler<ErrorEventArgs>               OnQuerySkuInfoFailed;
            public event EventHandler<PurchaseEventArgs>            OnConsumeSucceeded;
            public event EventHandler<ErrorEventArgs>               OnConsumeFailed;
            public event EventHandler<QueryNotConsumedEventArgs>    OnQueryNotConsumedPurchasesSucceeded;
            public event EventHandler<ErrorEventArgs>               OnQueryNotConsumedPurchasesFailed;

            private string _publicKey;
            private Dictionary<string, ProductBase> _products;

            void OnEnable() {
                IABEventManager.billingSupportedEvent           += BillingSupportedEventHandler;
		        IABEventManager.billingNotSupportedEvent        += BillingNotSupportedEventHandler;
                IABEventManager.querySkuDetailsSucceededEvent   += QuerySkuDetailsSucceededEventHandler;
                IABEventManager.querySkuDetailsFailedEvent      += QuerySkuDetailsFailedEventHandler;
                IABEventManager.purchaseSucceededEvent          += PurchaseSucceededEventHandler;
		        IABEventManager.purchaseFailedEvent             += PurchaseFailedEventHandler;
		        IABEventManager.consumePurchaseSucceededEvent   += ConsumePurchaseSucceededEventHandler;
		        IABEventManager.consumePurchaseFailedEvent      += ConsumePurchaseFailedEventHandler;
                IABEventManager.queryPurchasesSucceededEvent    += QueryPurchasesSucceededEventHandler;
                IABEventManager.queryPurchasesFailedEvent       += QueryPurchasesFailedEventHandler;
            }

            void OnDisable() {
                IABEventManager.billingSupportedEvent           -= BillingSupportedEventHandler;
		        IABEventManager.billingNotSupportedEvent        -= BillingNotSupportedEventHandler;
                IABEventManager.querySkuDetailsSucceededEvent   -= QuerySkuDetailsSucceededEventHandler;
                IABEventManager.querySkuDetailsFailedEvent      -= QuerySkuDetailsFailedEventHandler;
                IABEventManager.purchaseSucceededEvent          -= PurchaseSucceededEventHandler;
		        IABEventManager.purchaseFailedEvent             -= PurchaseFailedEventHandler;
		        IABEventManager.consumePurchaseSucceededEvent   -= ConsumePurchaseSucceededEventHandler;
		        IABEventManager.consumePurchaseFailedEvent      -= ConsumePurchaseFailedEventHandler;
                IABEventManager.queryPurchasesSucceededEvent    -= QueryPurchasesSucceededEventHandler;
                IABEventManager.queryPurchasesFailedEvent       -= QueryPurchasesFailedEventHandler;
            }

            public void SetData(string publicKey, Dictionary<string, ProductBase> products) { 
                _publicKey  = publicKey;
                _products   = products;
            }

            #region _iap_api_
            public void Init() {
                MyketIAB.init(_publicKey);
            }

            public void QuerySkuInfo(string[] productIds) {
                MyketIAB.querySkuDetails(FilterAlias(productIds));
            }

            public void PurchaseProduct(string productId) {
                MyketIAB.purchaseProduct(FilterAlias(productId));
            }

            public void ConsumeProduct(string productId) {
                MyketIAB.consumeProduct(FilterAlias(productId));
            }

            public void QueryNotConsumedPurchases() {
                MyketIAB.queryPurchases();
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

            private void QuerySkuDetailsSucceededEventHandler(List<MyketSkuInfo> skusInfo) {
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

            private void PurchaseSucceededEventHandler(MyketPurchase purchaseResult) {
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

            private void ConsumePurchaseSucceededEventHandler(MyketPurchase purchase) {
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

            private void QueryPurchasesSucceededEventHandler(List<MyketPurchase> purchases) {
                if(null != OnConsumeSucceeded) {
                    List<PurchaseEventArgs> ret = new List<PurchaseEventArgs>(purchases.Count);
                    purchases.ForEach((p)=> {
                        ret.Add(new PurchaseEventArgs(
                                 p.DeveloperPayload
                                ,p.PackageName
                                ,p.ProductId
                                ,p.PurchaseToken
                                ,ConvertProductType(p.Type)
                            ));
                    });

                    OnQueryNotConsumedPurchasesSucceeded(this, new QueryNotConsumedEventArgs(ret));
                }
            }

            private void QueryPurchasesFailedEventHandler(string error) {
                if(null != OnQueryNotConsumedPurchasesFailed) {
                    OnQueryNotConsumedPurchasesFailed(this, new ErrorEventArgs(error));
                }
            }
            #endregion

            private ProductType ConvertProductType(string typeStr) {
                Debug.Log("Cafebazaar Product Type: " + typeStr);

                ProductType ret = ProductType.NotDefined;

                if(typeStr == "inapp")  ret = ProductType.Consumable;
                if(typeStr == "subs")   ret = ProductType.Subscription;

                return ret;
            }

            private string FilterAlias(string productId) {
                return _products[productId].ProductId;
            }

            private string[] FilterAlias(string[] productIds) {
                List<string> ret = new List<string>(productIds.Length);

                foreach(string pid in productIds) {
                    ret.Add(_products[pid].ProductId);
                }

                return ret.ToArray();
            }
        }
    }
}
#endif
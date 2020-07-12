#if _dev_ || _googleplay_

using niap = Narin.Unity.IAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uiap = UnityEngine.Purchasing;
using UnityEngine.Purchasing;
using System;

namespace Narin.Unity.IAP {
    public partial class IAPBuilder {
        private class GoogleplayIAPManager : MonoBehaviour, IIAPManager, IStoreListener {
            private object _lockObject = new object();
    
            public event EventHandler<EventArgs>                OnPurchaseSupported;
            public event EventHandler<ErrorEventArgs>           OnPurchaseNotSupported;
            public event EventHandler<niap.PurchaseEventArgs>   OnPurchaseSucceeded;
            public event EventHandler<ErrorEventArgs>           OnPurchaseFailedHandler;
            public event EventHandler<QuerySkuInfoEventArgs>    OnQuerySkuInfoSucceeded;
            public event EventHandler<ErrorEventArgs>           OnQuerySkuInfoFailed;
            public event EventHandler<niap.PurchaseEventArgs>   OnConsumeSucceeded;
            public event EventHandler<ErrorEventArgs>           OnConsumeFailed;
            public event EventHandler<EventArgs>                OnRetriveFailedPurchasesSucceeded;
            public event EventHandler<ErrorEventArgs>           OnRetriveFailedPurchasesFailed;
    
            // This implementation just for implementing two 
            // interface (IStoreListener, IIAPEventManager) with 
            // same methods name (event and method)
            event EventHandler<ErrorEventArgs> IIAPManager.OnPurchaseFailed {
                add {
                    lock(_lockObject) {
                        this.OnPurchaseFailedHandler += value;
                    }
                }

                remove {
                    lock(_lockObject) {
                        this.OnPurchaseFailedHandler -= value;
                    }
                }
            }

            private List<ProductBase> _products;
            private static uiap.IStoreController _storeController;
            private static uiap.IExtensionProvider _storeExtensionProvider;

            #region iap_api
            public void Init() {
                if(IsInitialized()) return;
                var builder = uiap.ConfigurationBuilder.Instance(uiap.StandardPurchasingModule.Instance());
                
                foreach(var p in _products) {
                    builder.AddProduct(p.ProductId, ConvertProductType(p.Type));
                }

                UnityPurchasing.Initialize(this, builder);
            }
        
            public void QuerySkuInfo(string[] productIds) {
                var queryArg = new List<ProductDetail>(productIds.Length);

                foreach (string pid in productIds) {
                    var p = _storeController.products.WithID(pid);
                    queryArg.Add(new ProductDetail(
                         p.definition.id
                        ,p.metadata.localizedPriceString
                        ,p.metadata.localizedTitle
                        ,p.metadata.localizedDescription
                        ,p.metadata.isoCurrencyCode
                        ,ConvertProductType(p.definition.type)
                    ));
                }

                OnQuerySkuInfoSucceeded(this, new QuerySkuInfoEventArgs(queryArg));
            }
        
            public void PurchaseProduct(string productId) {
                if (IsInitialized()) {
                    Product product = _storeController.products.WithID(productId);

                    if (product != null && product.availableToPurchase) {
                        Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                        _storeController.InitiatePurchase(product);
                    }
                    else { 
                        Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }
                }
            }
        
            public void ConsumeProduct(string productId) {
                var p = _storeController.products.WithID(productId);
                _storeController.ConfirmPendingPurchase(p);

                OnConsumeSucceeded(this, new PurchaseEventArgs(
                      string.Empty
                     ,p.metadata.localizedTitle
                     ,p.definition.id
                     ,p.transactionID
                     ,ConvertProductType(p.definition.type)
                     ));
            }

            public void RetrieveFailedPurchases() {
                throw new System.NotImplementedException();
            }
            #endregion

            #region _event_listeners_
            public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
                OnPurchaseSupported(this, new EventArgs());
            }
    
            public void OnInitializeFailed(InitializationFailureReason error) {
                OnPurchaseNotSupported(this, new ErrorEventArgs(error.ToString()));
            }

            public PurchaseProcessingResult ProcessPurchase(uiap.PurchaseEventArgs purchase) {
                 OnPurchaseSucceeded(this, new PurchaseEventArgs(
                      string.Empty
                     ,purchase.purchasedProduct.metadata.localizedTitle
                     ,purchase.purchasedProduct.definition.id
                     ,purchase.purchasedProduct.transactionID
                     ,ConvertProductType(purchase.purchasedProduct.definition.type)
                     ));

                return PurchaseProcessingResult.Pending;
            }

            public void OnPurchaseFailed(Product i, PurchaseFailureReason error) {
                OnPurchaseFailedHandler(this, new ErrorEventArgs(error.ToString()));
            }
            #endregion

            private niap.ProductType ConvertProductType(uiap.ProductType unityPurchaseType) {
                ProductType ret = ProductType.NotDefined;

                if(unityPurchaseType == uiap.ProductType.Consumable)     ret = ProductType.Consumable;
                if(unityPurchaseType == uiap.ProductType.NonConsumable)  ret = ProductType.NonConsumable;
                if(unityPurchaseType == uiap.ProductType.Subscription)   ret = ProductType.Subscription;

                return ret;
            }

            private uiap.ProductType ConvertProductType(niap.ProductType type) {
                uiap.ProductType ret = uiap.ProductType.Consumable;
        
                if(niap.ProductType.Consumable       == type)    ret = uiap.ProductType.Consumable;
                if(niap.ProductType.NonConsumable    == type)    ret = uiap.ProductType.NonConsumable;
                if(niap.ProductType.Subscription     == type)    ret = uiap.ProductType.Subscription;
        
                return ret;
            }

            private bool IsInitialized() {
                return _storeController != null && _storeExtensionProvider != null;
            }
        }
    }
}

#endif
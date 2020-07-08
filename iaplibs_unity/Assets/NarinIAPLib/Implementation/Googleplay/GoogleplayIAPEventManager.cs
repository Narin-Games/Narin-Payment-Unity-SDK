using Narin.Unity.IAP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uiap = UnityEngine.Purchasing;
using niap = Narin.Unity.IAP;
using UnityEngine.Purchasing;

public class GoogleplayIAPEventManager : MonoBehaviour, uiap.IStoreListener, niap.IIAPEventManager {
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
    event EventHandler<ErrorEventArgs> IIAPEventManager.OnPurchaseFailed {
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

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        throw new NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error) {
        throw new NotImplementedException();
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {
        throw new NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(uiap.PurchaseEventArgs e) {
        throw new NotImplementedException();
    }
}

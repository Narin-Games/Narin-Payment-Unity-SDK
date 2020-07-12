using System;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

namespace Narin.Unity.IAP {
    public interface IIAPManager {
        event EventHandler<EventArgs>               OnPurchaseSupported;
        event EventHandler<ErrorEventArgs>          OnPurchaseNotSupported;
        event EventHandler<PurchaseEventArgs>       OnPurchaseSucceeded;
        event EventHandler<ErrorEventArgs>          OnPurchaseFailed;
        event EventHandler<QuerySkuInfoEventArgs>   OnQuerySkuInfoSucceeded;
        event EventHandler<ErrorEventArgs>          OnQuerySkuInfoFailed;
        event EventHandler<PurchaseEventArgs>       OnConsumeSucceeded;
        event EventHandler<ErrorEventArgs>          OnConsumeFailed;
        event EventHandler<EventArgs>               OnRetriveFailedPurchasesSucceeded;
        event EventHandler<ErrorEventArgs>          OnRetriveFailedPurchasesFailed;

        void Init();
        void PurchaseProduct(string productId);
        void ConsumeProduct(string productId);
        void QuerySkuInfo(string[] productIds);
        void RetrieveFailedPurchases();
    }
}
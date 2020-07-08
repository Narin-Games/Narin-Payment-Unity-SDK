using System.Collections.Generic;
using System;

namespace Narin.Unity.IAP {
    public interface IIAPEventManager {
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
    }
}
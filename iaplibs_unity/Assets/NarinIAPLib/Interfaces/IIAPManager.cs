using System;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

namespace Narin.Unity.IAP {
    public enum Store {
         Bazaar
        ,Myket
        ,Googleplay
    }

    public interface IIAPManager {
        /// <summary>
        /// Fired after Init is called when IAP is supported on the devive
        /// </summary>
        event EventHandler<EventArgs>               OnPurchaseSupported;
        /// <summary>
        /// Fired after Init is called when IAP is not supported on the device
        /// </summary>
        event EventHandler<ErrorEventArgs>          OnPurchaseNotSupported;
        /// <summary>
        /// Fired after PurchaseProduct is called when a purchase succeeds
        /// </summary>
        event EventHandler<PurchaseEventArgs>       OnPurchaseSucceeded;
        /// <summary>
        /// Fired after PurcahseProduct is called when a purchase fails
        /// </summary>
        event EventHandler<ErrorEventArgs>          OnPurchaseFailed;
        /// <summary>
        /// Fired after QuerySkuInfo is called when query has returned
        /// </summary>
        event EventHandler<QuerySkuInfoEventArgs>   OnQuerySkuInfoSucceeded;
        /// <summary>
        /// Fired after QuerySkuInfo is called when query fails
        /// </summary>
        event EventHandler<ErrorEventArgs>          OnQuerySkuInfoFailed;
        /// <summary>
        /// Fired after ConsumeProduct is called when a consume succeeds
        /// </summary>
        event EventHandler<PurchaseEventArgs>       OnConsumeSucceeded;
        /// <summary>
        /// Fired after ConsumeProduct is called when a consume fails
        /// </summary>
        event EventHandler<ErrorEventArgs>          OnConsumeFailed;
        /// <summary>
        /// !!! This event not implemented !!!
        /// </summary>
        event EventHandler<EventArgs>               OnRetriveFailedPurchasesSucceeded;
        /// <summary>
        /// !!! This event not implemented !!!
        /// </summary>
        event EventHandler<ErrorEventArgs>          OnRetriveFailedPurchasesFailed;

        /// <summary>
        /// Initializes IAP system
        /// </summary>
        void Init();
        /// <summary>
        /// Sends a request to purchase the product
        /// </summary>
        /// <param name="productId"></param>
        void PurchaseProduct(string productId);
        /// <summary>
        /// Sends a request to consume the product
        /// </summary>
        /// <param name="productId"></param>
        void ConsumeProduct(string productId);
        /// <summary>
        /// Sends a request to get all product information as setup in the store panel
        /// </summary>
        /// <param name="productIds"></param>
        void QuerySkuInfo(string[] productIds);
        /// <summary>
        /// !!! This method not implemented !!!
        /// </summary>
        void RetrieveFailedPurchases();
    }
}
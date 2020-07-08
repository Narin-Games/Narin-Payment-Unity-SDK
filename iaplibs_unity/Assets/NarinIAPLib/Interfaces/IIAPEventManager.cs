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

    public enum ProductType {
         NotDefined     = -1
        ,Consumable     = 0
        ,NonConsumable  = 1
        ,Subscription   = 2
    }

    public class ErrorEventArgs:EventArgs {
        public readonly string Message;
        
        public ErrorEventArgs(string message) {
            Message = message;
        }
    }

    public class QuerySkuInfoEventArgs: EventArgs {
        List<SkuInfo> SkusInfo;

        public QuerySkuInfoEventArgs(List<SkuInfo> skusInfo) {
            SkusInfo = skusInfo;
        }
    }

    public class SkuInfo {
        public readonly string ProductId;
        public readonly string Price;
        public readonly string Title;
        public readonly string Description;
        public readonly string CurrencyCode;
        public readonly ProductType Type;

        public SkuInfo(
              string productId
            , string price
            , string title
            , string description
            , string currencyCode
            , ProductType type) {

            ProductId       = productId;
            Price           = price;
            Title           = title;
            Description     = description;
            CurrencyCode    = currencyCode;
            Type            = type;
        }
    }

    public class PurchaseEventArgs: EventArgs {
        public readonly string DeveloperPayload;
        public readonly string PackageName;
        public readonly string ProductId;
        public readonly string PurchaseToken;
        public readonly ProductType Type;

        public PurchaseEventArgs(
              string developerPayload
            , string packageName
            , string productId
            , string purchaseToken
            , ProductType type) {

            DeveloperPayload = developerPayload;
            PackageName = packageName;
            ProductId = productId;
            PurchaseToken = purchaseToken;
            Type = type;
        }
    }
}
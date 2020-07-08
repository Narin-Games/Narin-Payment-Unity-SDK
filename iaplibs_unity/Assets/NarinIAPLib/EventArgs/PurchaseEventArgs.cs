using System;
using System.Collections.Generic;

namespace Narin.Unity.IAP {

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
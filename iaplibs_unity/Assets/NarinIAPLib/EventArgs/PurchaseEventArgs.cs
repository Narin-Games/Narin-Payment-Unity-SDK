using System;
using System.Collections.Generic;
using UnityEngine;

namespace Narin.Unity.IAP {
    [Serializable]
    public class PurchaseEventArgs: EventArgs {
        public string DeveloperPayload;
        public string PackageName;
        public string ProductId;       
        public string PurchaseToken;
        public ProductType Type;       

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

        public static PurchaseEventArgs FromJson(string json) {
            return JsonUtility.FromJson<PurchaseEventArgs>(json);
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }
}
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

namespace Narin.Unity.IAP {
    public interface IIAPManager {
        void Init();
        void PurchaseProduct(string sku);
        void ConsumeProduct(string sku);
        void QuarySkuInfo(string[] sku);
        void RetrieveFailedPurchases();
    }
}
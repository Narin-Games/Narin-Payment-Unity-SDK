using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace Narin.Unity.IAP {
    public interface IIAPManager {
        void Init(string key);
        void PurchaseProduct(string sku);
        void ConsumeProduct(string sku);
        void QuarySkuInfo(string[] sku);
        void RetrieveFailedPurchases();
    }
}
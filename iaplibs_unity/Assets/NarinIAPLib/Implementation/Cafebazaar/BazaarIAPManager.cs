using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narin.Unity.IAP;
using System.Threading.Tasks;
using BazaarPlugin;

public partial class BazaarIAPManager : MonoBehaviour, IIAPManager {
    public void Init(string key) {
        BazaarIAB.init(key);
    }
    
    public void QuaryInventory(string[] skus) {
        BazaarIAB.queryInventory(skus);
    }

    public void PurchaseProduct(string sku) {
        BazaarIAB.purchaseProduct(sku);
    }

    public void ConsumeProduct(string sku) {
        BazaarIAB.consumeProduct(sku);
    }

    public void QuarySkuInfo(string[] skus) {
        BazaarIAB.querySkuDetails(skus);
    }

    public void RetrieveFailedPurchases() {
            
    }
}

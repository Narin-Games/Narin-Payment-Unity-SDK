using System.Collections.Generic;

namespace Narin.Unity.IAP {

    public enum ProductType {
         NotDefined     = -1
        ,Consumable     = 0
        ,NonConsumable  = 1
        ,Subscription   = 2
    }

    public class ProductBase {
        public readonly string ProductId;
        public readonly ProductType Type;

        public ProductBase(string productId, ProductType type) {
            ProductId = productId;
            Type = type;
        }
    }
}
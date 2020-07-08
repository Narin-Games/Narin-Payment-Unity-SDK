namespace Narin.Unity.IAP {

    public enum ProductType {
         NotDefined     = -1
        ,Consumable     = 0
        ,NonConsumable  = 1
        ,Subscription   = 2
    }

    public class SkuBase {
        public readonly string ProductId;
        public readonly ProductType Type;

        public SkuBase(string productId, ProductType type) {
            ProductId = productId;
            Type = type;
        }
    }

}
namespace Narin.Unity.IAP {

    public class SkuInfo: SkuBase {
        public readonly string Price;
        public readonly string Title;
        public readonly string Description;
        public readonly string CurrencyCode;

        public SkuInfo(
              string productId
            , string price
            , string title
            , string description
            , string currencyCode
            , ProductType type):base(productId, type) {

            Price           = price;
            Title           = title;
            Description     = description;
            CurrencyCode    = currencyCode;
        }
    }

}
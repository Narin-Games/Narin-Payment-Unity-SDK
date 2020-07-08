using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;

namespace Narin.Unity.IAP {

    public enum Store {
         Bazaar
        ,Myket
        ,Googleplay
    }

    public static class IAPConstData {
        private static Store _selectedKey;
        public static void SetStoreKey(Store key) {
            _selectedKey = key;
        }

        #if     _dev_ || _cafebazaar_ || _myket_
        public static string Key {get {return _key[_selectedKey]; } }
        private static readonly Dictionary<Store, string> _key = new Dictionary<Store, string>() {
             {Store.Bazaar, ""}
            ,{Store.Myket,  ""}
        };
        #endif

        //Define products with different id per store
        public static SkuBase SpecialOffer {get {return _sepecialOffer[_selectedKey]; } }
        private static readonly Dictionary<Store, SkuBase> _sepecialOffer = new Dictionary<Store, SkuBase>() {
             {Store.Bazaar  , new SkuBase("SpecialOfferBazaar"  , ProductType.Consumable)}
            ,{Store.Myket   , new SkuBase("SpecialOfferMyket"   , ProductType.Consumable)}
        };

        //Define products for all store
        public static readonly SkuBase GemPackage100 = new SkuBase("", ProductType.Consumable);
    }
}
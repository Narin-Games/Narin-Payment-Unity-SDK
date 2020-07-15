using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Narin.Unity.IAP {

    public partial class IAPBuilder {
        public static IIAPManager CurrentIapManager { get; private set; } = null;

        private Dictionary<Store, string> _publicKeys = new Dictionary<Store, string>();
        private Dictionary<string, ProductBase> _products = new Dictionary<string, ProductBase>();

        public IAPBuilder SetPublicKey(Store store, string publicKey) { 
            _publicKeys.Add(store, publicKey);
            return this;
        }

        public IAPBuilder AddProduct(string productId, ProductType type) {
            _products.Add(productId, new ProductBase(productId, type));
            return this;
        }

        public IAPBuilder AddProduct(string aliasProductId, Dictionary<Store, string> productIds, ProductType type) {
            string pid = null;

            #if _dev_ || _cafebazaar_
            pid = productIds.First(x => x.Key == Store.Bazaar).Value;
            #endif
            
            #if _dev_ || _myket_
            pid = productIds.First(x => x.Key == Store.Myket).Value;
            #endif

            #if _dev_ || _googleplay_
            pid = productIds.First(x => x.Key == Store.Googleplay).Value;
            #endif

            _products.Add(aliasProductId, new ProductBase(pid, type));
            return this;
        }

        public IIAPManager BuildAndAttach(MonoBehaviour mono) {
            IIAPManager ret = null;

            #if _dev_ || _cafebazaar_
            ret = mono.gameObject.AddComponent<BazaarIAPManager>();
            ((BazaarIAPManager)ret).SetData(_publicKeys[Store.Bazaar], _products);
            #endif

            #if _dev_ || _myket_
            //Build Myket Manager
            #endif
            
            #if _dev_ || _googleplay_
            ret = mono.gameObject.AddComponent<GooglePlayIAPManager>();
            ((GooglePlayIAPManager)ret).SetData(_products);
            #endif

            CurrentIapManager = ret;

            return ret;
        }

        private void Reset() {
            _publicKeys.Clear();
            _products.Clear();
        }
    }
}
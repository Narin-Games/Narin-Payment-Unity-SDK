using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Narin.Unity.IAP {

    public partial class IAPBuilder {
        /// <summary>
        /// The reference of last builded IIAPManager object
        /// </summary>
        public static IIAPManager CurrentIapManager { get; private set; } = null;

        private Dictionary<Store, string> _publicKeys = new Dictionary<Store, string>();
        private Dictionary<string, ProductBase> _products = new Dictionary<string, ProductBase>();

        /// <summary>
        /// Set public key for each store (needed only for Bazaar and Myket)
        /// </summary>
        /// <param name="store"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public IAPBuilder SetPublicKey(Store store, string publicKey) { 
            _publicKeys.Add(store, publicKey);
            return this;
        }
        /// <summary>
        /// Set new product for all store (you must set all products before init IAP system)
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IAPBuilder AddProduct(string productId, ProductType type) {
            _products.Add(productId, new ProductBase(productId, type));
            return this;
        }

        /// <summary>
        /// Set new product with specific productId for each store
        /// </summary>
        /// <param name="aliasProductId"></param>
        /// <param name="productIds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Build IIAPManager object and attach it to GameObject (After calling this method the IAPBuilder class is reset)
        /// </summary>
        /// <param name="mono">Mono reference from GameObject to which you want to attach IIAPManager</param>
        /// <returns></returns>
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
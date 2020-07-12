using BazaarPlugin;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Narin.Unity.IAP {

    public partial class IAPBuilder {
        private Dictionary<Store, string> _publicKeys = new Dictionary<Store, string>();
        private List<ProductBase> _products = new List<ProductBase>();

        public IAPBuilder SetPublicKey(Store store, string publicKey) { 
            _publicKeys.Add(store, publicKey);
            return this;
        }

        public IAPBuilder AddProduct(string productId, ProductType type, Dictionary<Store, string> Ids = null) {
            string pid = null;

            if(null == Ids) {
                pid = productId;
            } 
            else {
                #if _dev_ || _cafebazaar_
                pid = Ids.First(x => x.Key == Store.Bazaar).Value;
                #endif
                
                #if _dev_ || _myket_
                pid = Ids.First(x => x.Key == Store.Myket).Value;
                #endif

                #if _dev_ || _googleplay_
                pid = Ids.First(x => x.Key == Store.Googleplay).Value;
                #endif
            }

            _products.Add(new ProductBase(pid, type));
            return this;
        }

        public IIAPManager BuildAndAttach(MonoBehaviour mono) {
            #if _dev_ || _cafebazaar_
            
            #endif
            
            #if _dev_ || _myket_
            
            #endif
            
            #if _dev_ || _googleplay_
            
            #endif
            return null;
        }
    }
}
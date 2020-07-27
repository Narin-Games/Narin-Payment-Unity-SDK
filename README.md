# Narin-Payment-Unity-SDK
This SDK is implemented for the integration payment method of online app stores (Googleplay, Cafebazaar, Myket) as a single interface.

If you want to release your game in multiple app stores, this SDK will have the following advantages for you:

- No need to create different branches in git to get the build of each store
- Implement payment of all stores with just one implementation
- Can be used in implementation of **Distributed Build System** and **Batch Building**

## How To Use
This system has three stages in its life cycle, which I will explain in order:

**BUILD --> Initialize --> Use IAP Methods**

### Build:
In this step you need to create an object of type IIAPManager through the IAPBuilder class To access the store payment API through this object.

You must first provide payment information to the IAPBuilder class, as in the following code example:

``` csharp
using Narin.Unity.IAP;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IAPSample : MonoBehaviour {

    public const string PUBLIC_KEY_BAZAAR   = "BazaarPublicKey";
    public const string PUBLIC_KEY_MYKET    = "MyketPublicKey";

    public const string GEM_PACK_1          = "com.narin.gempack1";
    public const string HERO_1              = "com.narin.hero1" ;
    public const string VIP_ALIAS           = "com.narin.vip";

    void Awake() {
    
        // Check if IIAPManager object is not build yet
        if(null == IAPBuilder.CurrentIapManager) {
        
            // Create a new builder object
            IAPBuilder builder = new IAPBuilder();
            
            // Set public key for each store (googleplay don't need a public key)
            builder.SetPublicKey(Store.Bazaar, PUBLIC_KEY_BAZAAR);
            builder.SetPublicKey(Store.Myket, PUBLIC_KEY_MYKET);
            
            // Add your products with product id and product type (Consumable, Subscription, ...)
            builder.AddProduct(GEM_PACK_1  , ProductType.Consumable);
            builder.AddProduct(HERO_1      , ProductType.Subscription);

            // You can also add product with different id for each store
            // and access to them with one product id that we called aliasProductId
            builder.AddProduct(VIP_ALIAS, new Dictionary<Store, string>() {
                 {Store.Bazaar,     "com.narin.cafebazaar.vip"}
                ,{Store.Myket,      "com.narin.myket.vip"}
                ,{Store.Googleplay, "com.narin.googleplay.vip"}
            }
            , ProductType.Subscription);

            // Finally you must build the IIAPManager and 
            // Attached them to the gameObject that you passed reference as a parameter
            builder.BuildAndAttach(this);

            SceneManager.LoadScene(1);

            Destroy(this);
        }
    }
}
```

**Notice:**




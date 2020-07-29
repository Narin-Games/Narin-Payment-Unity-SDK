# Narin-Payment-Unity-SDK
This SDK is implemented for the integration payment method of online app stores (Googleplay, Cafebazaar, Myket) as a single interface.

If you want to release your game in multiple app stores, this SDK will have the following advantages for you:

- No need to create different branches in git to get the build of each store
- Implement payment of all stores with just one implementation
- Can be used in implementation of **Distributed Build System** and **Batch Building**

## How To Use
This system has three stages in its life cycle, which I will explain in order:

**BUILD --> Initialize --> Use IAP Methods**

### 1) Build:
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
**Notice 1-1:**

The Build stage only needs to happen once when the game is run. and after calling **IAPBuilder.AttachAndBuild()**, you create a IAPManager component whose reference is stored in the static variable **IAPBuilder.CurrentIAPManager** and all the information entered about the store is reset in the IAPBuilder object.

``` csharp
//This static variable is set after calling IAPBuilder.BuildAndAttach()
IAPBuilder.CurrentIapManager
```

**Notice 1-2:**

It is better to perform this step in a separate Scene that is loaded only once in the game.

### 2) Initialize:
Before using any of the **IIAPManager** object methods, We must first initialize the payment system via the **IIAPManager.Init()** method and get the answer through the callback events.

``` csharp
public class IAPSampleUI : MonoBehaviour {
    [SerializeField] Text _txtResult;

    IIAPManager iapManager;

    void OnEnable() {
        if(null == iapManager) iapManager = IAPBuilder.CurrentIapManager;

        iapManager.OnPurchaseSupported                  += OnPurchaseSupportedHandler;
        iapManager.OnPurchaseNotSupported               += OnPurchcaseNotSupportedHandler;
        
        //Add other events subscriber
    }

    void OnDisable() {
        iapManager.OnPurchaseSupported                  -= OnPurchaseSupportedHandler;
        iapManager.OnPurchaseNotSupported               -= OnPurchcaseNotSupportedHandler;
        
        //Remove other events subscriber
    }

    public void Init_OnClick() {
        iapManager.Init();
    }


    #region _event_listeners_
    private void OnPurchaseSupportedHandler(object sender, EventArgs e) {
        string log = "On Purchase Supported";
        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnPurchcaseNotSupportedHandler(object sender, ErrorEventArgs error) {
        string log = "On Purchase Not Supported: " + error.Message;
        Debug.Log(log);
        _txtResult.text = log;
    }

    // Define other event handler
    #endregion
}
```

### 3) Use IAP Methods:
After successfully initializing, You can now use all IIAPManager methods. The list of these methods and Event Listeners is written below.

### Methods:

``` csharp

// Initializes IAP system
void Init();

// Sends a request to purchase the product
void PurchaseProduct(string productId);

// Sends a request to consume the product
void ConsumeProduct(string productId);

// Sends a request to get all product information as setup in the store panel
void QuerySkuInfo(string[] productIds);

// Sends a request to get all purchases that not consumed
void QueryNotConsumedPurchases();

```


### Events:

``` csharp

// Fired after Init is called when IAP is supported on the devive
event EventHandler<EventArgs>                   OnPurchaseSupported;

// Fired after Init is called when IAP is not supported on the device
event EventHandler<ErrorEventArgs>              OnPurchaseNotSupported;

// Fired after PurchaseProduct is called when a purchase succeeds
event EventHandler<PurchaseEventArgs>           OnPurchaseSucceeded;

// Fired after PurcahseProduct is called when a purchase fails
event EventHandler<ErrorEventArgs>              OnPurchaseFailed;

// Fired after QuerySkuInfo is called when query has returned
event EventHandler<QuerySkuInfoEventArgs>       OnQuerySkuInfoSucceeded;

// Fired after QuerySkuInfo is called when query fails
event EventHandler<ErrorEventArgs>              OnQuerySkuInfoFailed;

// Fired after ConsumeProduct is called when a consume succeeds
event EventHandler<PurchaseEventArgs>           OnConsumeSucceeded;

// Fired after ConsumeProduct is called when a consume fails
event EventHandler<ErrorEventArgs>              OnConsumeFailed;

// Fired after QueryNotConsumedPurchases is called when query has returned
event EventHandler<QueryNotConsumedEventArgs>   OnQueryNotConsumedPurchasesSucceeded;

// Fired after QueryNotConsumedPurchases is called when query fails
event EventHandler<ErrorEventArgs>              OnQueryNotConsumedPurchasesFailed;

```

## Sample
In the [Sample Directory](https://github.com/Narin-Games/Narin-Payment-Unity-SDK/tree/master/iaplibs_unity/Assets/NarinIAPLib/Sample) there is a complete example of how to use the SDK that you can use.

## Build and Export Project

You need to do the following two steps before export build from your project:

### 1) Set Scripting Define Symbols:
First, go to the following path in the Unity engine:

**File > Build Settings > Player Settings > Player > Other Settings > Scripting Define Symbols**

![unity-scripting-define-symbols]()

Then in this path, define the specific symbol of that store according to the table:

| Store         | Symbole       |
| :--:          | :--:          |
| GooglePlay    | \_googleplay_ |
| Cafebazaar    | \_cafebazaar_ |
| myket         | \_myket_      |


This step causes only the code related to that store to be used in your final build.

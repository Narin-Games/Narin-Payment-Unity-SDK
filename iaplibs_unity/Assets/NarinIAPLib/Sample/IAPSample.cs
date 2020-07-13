using Narin.Unity.IAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPSample : MonoBehaviour {

    public const string PUBLIC_KEY_BAZAAR       = "";
    public const string PUBLIC_KEY_MYKET        = "";
    public const string GEM_PACK_1              = "com.company.gempack1";
    public const string HERO_1                  = "com.company.hero1" ;
    public const string VIP                     = "com.company.vip";

    void Awake() {
        IAPBuilder builder = new IAPBuilder();

        builder.SetPublicKey(Store.Bazaar, PUBLIC_KEY_BAZAAR)

            .SetPublicKey(Store.Myket, PUBLIC_KEY_MYKET)

            .AddProduct(GEM_PACK_1  , ProductType.Consumable)

            .AddProduct(HERO_1      , ProductType.NonConsumable)

            .AddProduct(VIP, new Dictionary<Store, string>() {
                 {Store.Bazaar,     "ir.publisher1.vip"}
                ,{Store.Myket,      "ir.publisher2.vip"}
                ,{Store.Googleplay, "com.publisher3.vip"}
            }
            , ProductType.Subscription)

            .BuildAndAttach(this);
    }
}
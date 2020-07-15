using Narin.Unity.IAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IAPSample : MonoBehaviour {

    public const string PUBLIC_KEY_BAZAAR = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwCg+LKQRVjYsZAcAMjS2/OeoZIm92/" +
        "Sxv7TZ90lsZqFSTtyseBgHlDWNV7mwrddI982To/djjraDeIPgSxON7lU+Z/couQ1421+fUl4SQdzbHlQuZp2zStLAHR9T3+rU/yDBcKb9ARJ0" +
        "m1BNS7KGloJ1sG3IETK6eLQIQautwyoIx6Kyzo+aCNrro76YYEtlZkWaGt6rLdj1EKmC2rXIG92VoQoSzZSod8LU/xdpF0CAwEAAQ==";

    public const string PUBLIC_KEY_MYKET    = "";
    public const string GEM_PACK_1          = "com.narin.gempack1";
    public const string HERO_1              = "com.narin.hero1" ;
    public const string VIP_ALIAS           = "com.narin.vip";

    void Awake() {
        if(null == IAPBuilder.CurrentIapManager) {

            IAPBuilder builder = new IAPBuilder();

            builder
                .SetPublicKey(Store.Bazaar, PUBLIC_KEY_BAZAAR)
                .SetPublicKey(Store.Myket, PUBLIC_KEY_MYKET)
                .AddProduct(GEM_PACK_1  , ProductType.Consumable)
                .AddProduct(HERO_1      , ProductType.NonConsumable)

                .AddProduct(VIP_ALIAS, new Dictionary<Store, string>() {
                     {Store.Bazaar,     "com.narin.cafebazaar.vip"}
                    ,{Store.Myket,      "com.narin.myket.vip"}
                    ,{Store.Googleplay, "com.narin.googleplay.vip"}
                }
                , ProductType.Subscription)

                .BuildAndAttach(this);

            SceneManager.LoadScene(1);

            Destroy(this);
        }
    }
}
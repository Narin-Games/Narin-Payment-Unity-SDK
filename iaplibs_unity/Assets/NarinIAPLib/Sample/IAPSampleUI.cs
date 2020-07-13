using Narin.Unity.IAP;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class IAPSampleUI : MonoBehaviour
{
    IIAPManager iapManager;

    void Start() {
        iapManager = IAPBuilder.CurrentIapManager;
    }

    public void Init_OnClick() {
        iapManager.Init();
    }

    public void PurchaseProduct_OnClick() {
        iapManager.PurchaseProduct(IAPSample.GEM_PACK_1);
    }

    public void ConsumeProduct_OnClick() {
        iapManager.ConsumeProduct(IAPSample.GEM_PACK_1);
    }

    public void QuerySkuInfo_OnClick() {
        iapManager.QuerySkuInfo(new string[] { 
             IAPSample.GEM_PACK_1
            ,IAPSample.HERO_1
            ,IAPSample.VIP
        });
    }
}

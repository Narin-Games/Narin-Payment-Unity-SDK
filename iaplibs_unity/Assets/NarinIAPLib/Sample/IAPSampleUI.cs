using Narin.Unity.IAP;
using UnityEngine;
using System;
using UnityEngine.UI;
using NBidi;

public class IAPSampleUI : MonoBehaviour
{
    [SerializeField] Text _txtResult;

    IIAPManager iapManager;

    void OnEnable() {
        if(null == iapManager) iapManager = IAPBuilder.CurrentIapManager;

        iapManager.OnPurchaseSupported                  += OnPurchaseSupportedHandler;
        iapManager.OnPurchaseNotSupported               += OnPurchcaseNotSupportedHandler;
        iapManager.OnQuerySkuInfoSucceeded              += OnQuerySkuInfoSucceededHandler;
        iapManager.OnQuerySkuInfoFailed                 += OnQuerySkuInfoFailedHandler;
        iapManager.OnPurchaseSucceeded                  += OnPurchaseSucceededHandler;
        iapManager.OnPurchaseFailed                     += OnPurchaseFailedHandler;
        iapManager.OnConsumeSucceeded                   += OnConsumeSucceededHandler;
        iapManager.OnConsumeFailed                      += OnConsumeFailedHandler;
        iapManager.OnQueryNotConsumedPurchasesSucceeded += OnQueryNotConsumedPurchasesSucceededHandler;
        iapManager.OnQueryNotConsumedPurchasesFailed    += OnQueryNotConsumedPurchasesFailedHandler;
    }

    void OnDisable() {
        iapManager.OnPurchaseSupported                  -= OnPurchaseSupportedHandler;
        iapManager.OnPurchaseNotSupported               -= OnPurchcaseNotSupportedHandler;
        iapManager.OnQuerySkuInfoSucceeded              -= OnQuerySkuInfoSucceededHandler;
        iapManager.OnQuerySkuInfoFailed                 -= OnQuerySkuInfoFailedHandler;
        iapManager.OnPurchaseSucceeded                  -= OnPurchaseSucceededHandler;
        iapManager.OnPurchaseFailed                     -= OnPurchaseFailedHandler;
        iapManager.OnConsumeSucceeded                   -= OnConsumeSucceededHandler;
        iapManager.OnConsumeFailed                      -= OnConsumeFailedHandler;
        iapManager.OnQueryNotConsumedPurchasesSucceeded -= OnQueryNotConsumedPurchasesSucceededHandler;
        iapManager.OnQueryNotConsumedPurchasesFailed    -= OnQueryNotConsumedPurchasesFailedHandler;            
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
            ,IAPSample.VIP_ALIAS
        });
    }

    public void QueryNotConsumedPurchases_OnClick() {
        iapManager.QueryNotConsumedPurchases();
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

    private void OnQuerySkuInfoSucceededHandler(object sender, QuerySkuInfoEventArgs skus) {
        string log = "";
        
        for (int i=0; i<skus.SkusInfo.Count; ++i) {
            log +=
                "Idx: "             + i                                                         + '\n' +
                "ProductId: "       + skus.SkusInfo[i].ProductId                                + '\n' + 
                "Type: "            + skus.SkusInfo[i].Type                                     + '\n' +
                "Price: "           + NBidi.NBidi.LogicalToVisual(skus.SkusInfo[i].Price)       + '\n' +
                "Currency Code: "   + skus.SkusInfo[i].CurrencyCode                             + '\n' +
                "Title: "           + NBidi.NBidi.LogicalToVisual(skus.SkusInfo[i].Title)       + '\n' +
                "Description: "     + NBidi.NBidi.LogicalToVisual(skus.SkusInfo[i].Description) + '\n' +
                ".";
        }

        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnQuerySkuInfoFailedHandler(object sender, ErrorEventArgs error) {
        string log = "On Query SkuInfo Failed: " + error.Message;
        Debug.LogFormat(log);
        _txtResult.text = log;
    }

    private void OnPurchaseSucceededHandler(object sender, PurchaseEventArgs purchase) {
        string log = 
            "ProductId: "           + purchase.ProductId                                + '\n' + 
            "Type: "                + purchase.Type.ToString()                          + '\n' +
            "PackageName: "         + NBidi.NBidi.LogicalToVisual(purchase.PackageName) + '\n' +
            "Purchase Token: "      + purchase.PurchaseToken                            + '\n' +
            "DeveloperPayload: "    + purchase.DeveloperPayload                         + '\n' +
            "";

        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnPurchaseFailedHandler(object sender, ErrorEventArgs error) {
        string log = "On Purchase Failed: " + error.Message;
        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnConsumeSucceededHandler(object sender, PurchaseEventArgs purchase) {
        string log = 
            "ProductId: "           + purchase.ProductId                                + '\n' + 
            "Type: "                + purchase.Type.ToString()                          + '\n' +
            "PackageName: "         + NBidi.NBidi.LogicalToVisual(purchase.PackageName) + '\n' +
            "Purchase Token: "      + purchase.PurchaseToken                            + '\n' +
            "DeveloperPayload: "    + purchase.DeveloperPayload                         + '\n' +
            "";

        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnConsumeFailedHandler(object sender, ErrorEventArgs error) {
        string log = "On Consume Failed: " + error.Message;
        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnQueryNotConsumedPurchasesSucceededHandler(object sender, QueryNotConsumedEventArgs purchases) {
        var purchaseList = purchases.Purchases;
        string log = "You have no not consumed purchases";
        for(int i=0; i<purchaseList.Count; ++i) {
            log += 
                "Idx: "                 + i                                                         + '\n' +
                "ProductId: "           + purchaseList[i].ProductId                                 + '\n' +
                "Type: "                + purchaseList[i].Type.ToString()                           + '\n' +
                "PackageName: "         + NBidi.NBidi.LogicalToVisual(purchaseList[i].PackageName)  + '\n' +
                "Purchase Token: "      + purchaseList[i].PurchaseToken                             + '\n' +
                "DeveloperPayload: "    + purchaseList[i].DeveloperPayload                          + '\n' +
                "";
        }
        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnQueryNotConsumedPurchasesFailedHandler(object sender, ErrorEventArgs error) {
        throw new NotImplementedException();
    }
    #endregion
}
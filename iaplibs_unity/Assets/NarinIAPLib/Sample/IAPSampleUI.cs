using Narin.Unity.IAP;
using UnityEngine;
using System;
using UnityEngine.UI;

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
        iapManager.OnRetriveFailedPurchasesSucceeded    += OnRetriveFailedPurchasesSucceededHandler;
        iapManager.OnRetriveFailedPurchasesFailed       += OnRetriveFailedPurchasesFailedHandler;
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
        iapManager.OnRetriveFailedPurchasesSucceeded    -= OnRetriveFailedPurchasesSucceededHandler;
        iapManager.OnRetriveFailedPurchasesFailed       -= OnRetriveFailedPurchasesFailedHandler;            
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

    public void RetrieveFailedPurchases_OnClick() {
        iapManager.RetrieveFailedPurchases();
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
            log += i + ":" + '\n' +
                "ProductId: "       + skus.SkusInfo[i].ProductId    + '\n' + 
                "Type: "            + skus.SkusInfo[i].Type         + '\n' +
                "Price: "           + skus.SkusInfo[i].Price        + '\n' +
                "Currency Code: "   + skus.SkusInfo[i].CurrencyCode + '\n' +
                "Title: "           + skus.SkusInfo[i].Title        + '\n' +
                "Description: "     + skus.SkusInfo[i].Description  + '\n' +
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
            "ProductId: "       + purchase.ProductId        + '\n' + 
            "Type: "            + purchase.Type.ToString()  + '\n' +
            "PackageName: "     + purchase.PackageName      + '\n' +
            "Currency Code: "   + purchase.PurchaseToken    + '\n' +
            "Title: "           + purchase.DeveloperPayload + '\n' +
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
            "ProductId: "       + purchase.ProductId        + '\n' + 
            "Type: "            + purchase.Type.ToString()  + '\n' +
            "PackageName: "     + purchase.PackageName      + '\n' +
            "Currency Code: "   + purchase.PurchaseToken    + '\n' +
            "Title: "           + purchase.DeveloperPayload + '\n' +
            "";

        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnConsumeFailedHandler(object sender, ErrorEventArgs error) {
        string log = "On Consume Failed: " + error.Message;
        Debug.Log(log);
        _txtResult.text = log;
    }

    private void OnRetriveFailedPurchasesSucceededHandler(object sender, EventArgs e) {
        throw new NotImplementedException();
    }

    private void OnRetriveFailedPurchasesFailedHandler(object sender, ErrorEventArgs e) {
        throw new NotImplementedException();
    }
    #endregion
}
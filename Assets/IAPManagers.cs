using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManagers : MonoBehaviour
{ 

    private string coin500 = "com.vdogames.iaptest.coin500";
    private string coin1000 = "com.vdogames.iaptest.coin1000";
    private string removeAds = "com.vdogames.iaptest.removeads";
    private IStoreController storeController;

    public GameObject restoreButton;

    private void Awake()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == coin500)
        {
            // reward your player
            Debug.Log("You've gained 500 coins");
            Debug.Log("receipt :: " + product.receipt);
            Debug.Log("hasReceipt :: " + product.hasReceipt);
            Debug.Log("appleProductIsRestored :: " + product.appleProductIsRestored);
            Debug.Log("appleOriginalTransactionID :: " + product.appleOriginalTransactionID);
            Debug.Log("transactionID :: " + product.transactionID);
            Debug.Log("availableToPurchase :: " + product.availableToPurchase);
            Debug.Log("metadata :: " + product.metadata);
            Debug.Log("definition :: " + product.definition);
        }

        if (product.definition.id == removeAds)
        {
            // reward your player
            Debug.Log("All ads removed!");
        }

        if (product.definition.id == coin1000)
        {
            // reward your player
            Debug.Log("You've gained 1000 coins");
            Debug.Log("receipt :: " + product.receipt);
            Debug.Log("hasReceipt :: " + product.hasReceipt);
            Debug.Log("appleProductIsRestored :: " + product.appleProductIsRestored);
            Debug.Log("appleOriginalTransactionID :: " + product.appleOriginalTransactionID);
            Debug.Log("transactionID :: " + product.transactionID);
            Debug.Log("availableToPurchase :: " + product.availableToPurchase);
            Debug.Log("metadata :: " + product.metadata);
            Debug.Log("definition :: " + product.definition);
        }



        IAPManager.instance.PurchaseProduct(product.definition.id);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureReason)
    {
        Debug.Log(product.definition.id + "failed because" + failureReason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Transaction completed, provide the user with the IAP token
        string iapToken = args.purchasedProduct.receipt;
        Debug.Log("IAP Token: " + iapToken);
        var wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(iapToken);
        if (null == wrapper)
        {
            //  throw new InvalidReceiptDataException();
        }

        var store = (string)wrapper["Store"];
        var payload = (string)wrapper["Payload"];

        var details = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
        var json = (string)details["json"]; // This is the INAPP_PURCHASE_DATA information
        var sig = (string)details["signature"]; // This is the INAPP_DATA_SIGNATURE information

        Debug.Log("INAPP_PURCHASE_DATA ::" + json);
        Debug.Log("INAPP_PURCHASE_SIGNATURE ::" + sig);
        // Handle the IAP token as needed

        return PurchaseProcessingResult.Complete;
    }

}

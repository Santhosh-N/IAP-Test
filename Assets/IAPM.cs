using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPM : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    private void Start()
    {
        InitializePurchasing();
    }

    private void InitializePurchasing()
    {
        // Create a builder instance for the Unity Purchasing system
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add your in-app product IDs
        builder.AddProduct("com.vdogames.iaptest.coin500", ProductType.Consumable);
        builder.AddProduct("com.vdogames.iaptest.removeads", ProductType.NonConsumable);

        // Initialize the Unity Purchasing system
        UnityPurchasing.Initialize(this, builder);
     //   PurchaseProduct("com.vdogames.iaptest.coin500");

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Handle initialization failure
    }

    public void PurchaseProduct(string productId)
    {
        if (storeController != null)
        {
            // Retrieve the product reference using its product ID
            Product product = storeController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                // Initiate the purchase
                storeController.InitiatePurchase(product);
            }
            else
            {
                // Handle product retrieval or purchase initiation failure
            }
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Transaction completed, provide the user with the IAP token
        string iapToken = args.purchasedProduct.receipt;
        Debug.Log("IAP Token: " + iapToken);
        // Handle the IAP token as needed
        GetReceiptData(args);
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // Handle purchase failure
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public ReceiptData GetReceiptData(PurchaseEventArgs e)

    {
        ReceiptData data = new ReceiptData();

        if (e != null)
        {
            //Main receipt root
            string receiptString = e.purchasedProduct.receipt;
            Debug.Log("receiptString " + receiptString);
            var receiptDict = (Dictionary<string, object>)MiniJson.JsonDecode(receiptString);
            Debug.Log("receiptDict COUNT" + receiptDict.Count);

            //Next level Paylod dict
            string payloadString = (string)receiptDict["Payload"];
            Debug.Log("payloadString " + payloadString);
            var payloadDict = (Dictionary<string, object>)MiniJson.JsonDecode(payloadString);

            //Stuff from json object
            string jsonString = (string)payloadDict["json"];
            Debug.Log("jsonString " + jsonString);
            var jsonDict = (Dictionary<string, object>)MiniJson.JsonDecode(jsonString);
            string orderIdString = (string)jsonDict["orderId"];
            Debug.Log("orderIdString " + orderIdString);
            string packageNameString = (string)jsonDict["packageName"];
            Debug.Log("packageNameString " + packageNameString);
            string productIdString = (string)jsonDict["productId"];
            Debug.Log("productIdString " + productIdString);

            double orderDateDouble = Convert.ToDouble(jsonDict["purchaseTime"]);
            Debug.Log("orderDateDouble " + orderDateDouble);

            string purchaseTokenString = (string)jsonDict["purchaseToken"];
            Debug.Log("purchaseTokenString " + purchaseTokenString);

            //Stuff from skuDetails object
            string skuDetailsString = (string)payloadDict["skuDetails"];
            Debug.Log("skuDetailsString " + skuDetailsString);
            var skuDetailsDict = (Dictionary<string, object>)MiniJson.JsonDecode(skuDetailsString);
            long priceAmountMicrosLong = Convert.ToInt64(skuDetailsDict["price_amount_micros"]);
            Debug.Log("priceAmountMicrosLong " + priceAmountMicrosLong);
            string priceCurrencyCodeString = (string)skuDetailsDict["price_currency_code"];
            Debug.Log("priceCurrencyCodeString " + priceCurrencyCodeString);

            //Creating UTC from Epox
            DateTime orderDateTemp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            orderDateTemp = orderDateTemp.AddMilliseconds(orderDateDouble);

            data.orderId = orderIdString;
            data.packageName = packageNameString;
            data.productId = productIdString;
            data.purchaseToken = purchaseTokenString;
            data.priceAmountMicros = priceAmountMicrosLong;
            data.priceCurrencyCode = priceCurrencyCodeString;
            // data.orderDate = orderDateTemp;
            data.receipt = receiptString;
            Debug.Log("GetReceiptData succesfull");
        }
        else
        {
            Debug.Log("PurchaseEventArgs is NULL");
        }

        return data;
    }

}
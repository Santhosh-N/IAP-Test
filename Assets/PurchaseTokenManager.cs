using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using System;
using System.Collections.Generic;

public class PurchaseTokenManager : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static CrossPlatformValidator validator;

    private static string publicKey = "IAPTest"; // Replace with your actual public key from the Google Play Console.

    void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add your in-app product identifiers here
        builder.AddProduct("com.vdogames.iaptest.coin1000", ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        var module = extensions.GetExtension<IGooglePlayStoreExtensions>();
        if (module != null)
        {
            // Register the Google Play validator with the public key
            validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), publicKey);
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("IAP Initialization failed: " + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log("Process Purchase");
        // Perform validation on the purchase
        try
        {
            var receipt = args.purchasedProduct.receipt;
            var payload = args.purchasedProduct.transactionID;
            if (validator != null)
            {
                bool isValidPurchase = ValidatePurchase(args.purchasedProduct.definition.id, payload, receipt);
                if (isValidPurchase)
                {
                    // Purchase is valid, get the raw purchase token
                    var rawPurchaseToken = GetRawPurchaseTokenFromReceipt(receipt);
                    Debug.Log("Raw Purchase Token: " + rawPurchaseToken);
                }
                else
                {
                    Debug.Log("Purchase validation failed!");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error during purchase validation: " + ex);
        }

        return PurchaseProcessingResult.Complete;
    }

    private bool ValidatePurchase(string productId, string payload, string receipt)
    {
        // Implement your purchase validation logic here.
        // You can use the validator.Validate method or any other custom validation logic.
        // For the sake of this example, we'll return true to indicate a valid purchase.

        return true;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase failed: " + product.definition.id + ", reason: " + failureReason);
    }

    private string GetRawPurchaseTokenFromReceipt(string receipt)
    {
        // This method extracts the raw purchase token from the purchase receipt.
        // The receipt parameter should contain the full purchase receipt, which you can get from args.purchasedProduct.receipt in the ProcessPurchase method.

        Dictionary<string, object> jsonReceipt = MiniJson.JsonDecode(receipt) as Dictionary<string, object>;
        if (jsonReceipt != null && jsonReceipt.ContainsKey("purchaseToken"))
        {
            string rawPurchaseToken = jsonReceipt["purchaseToken"] as string;
            return rawPurchaseToken;
        }

        // Implementation depends on the receipt format used in your Unity project.

        return "";
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
     //   throw new NotImplementedException();
    }
}
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
    public static PurchaseManager instance;
    private IStoreController storeController;
    private IExtensionProvider extensionProvider;

    private const string productId = "com.vdogames.iaptest.coin500"; // Replace with your actual product ID

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }


    private void Start()
    {
        InitializePurchasing();
    }

    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(productId, ProductType.Consumable); // Add your product with its type

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Initialization failed, handle the error
    }

    public void PurchaseProduct()
    {
        if (storeController == null)
        {
            Debug.Log("Store controller is not initialized.");
            return;
        }

        Product product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log("Unable to purchase the product.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        bool isValid = ValidateReceipt(args.purchasedProduct.receipt);

        if (isValid)
        {
            // The purchase is valid, process it on the server and grant the item to the user
            string purchaseToken = args.purchasedProduct.receipt;
            Debug.Log("Purchase token: " + purchaseToken);

            // Call your server-side validation endpoint with the purchaseToken and handle the response

            // Optionally, update the user's account or grant the purchased item in your game
        }
        else
        {
            // The purchase is not valid, handle it accordingly (e.g., show an error message)
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // The purchase has failed, handle the failure reason
    }

    private bool ValidateReceipt(string receipt)
    {
        // Unity IAP provides a validator class to validate the receipt
        CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

        try
        {
            IPurchaseReceipt[] purchaseReceipts = validator.Validate(receipt);

            // Validate the receipts for each platform (Google Play, Apple, etc.)
            foreach (IPurchaseReceipt purchaseReceipt in purchaseReceipts)
            {
                // Validate the receipt for each platform
                if (purchaseReceipt.productID == productId)
                {
                    // Receipt is valid
                    return true;
                }
            }
        }
        catch (IAPSecurityException ex)
        {
            // Invalid receipt, handle the exception
            Debug.Log("Invalid receipt: " + ex.Message);
        }

        return false;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }
}
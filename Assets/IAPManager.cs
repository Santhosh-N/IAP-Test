using UnityEngine;
using System.Runtime.InteropServices;

public class IAPManager : MonoBehaviour
{
    public static IAPManager instance;

    private AndroidJavaObject googlePlayIAPBridge;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Its Android Platform");
            // Create an instance of the GooglePlayIAPBridge class
            using (var pluginClass = new AndroidJavaClass("com.com.VDOGAMES.IAPTest.GooglePlayIAPBridge"))
            {
                googlePlayIAPBridge = pluginClass.CallStatic<AndroidJavaObject>("instance", new object[] { this });
            }
        }
    }

    public void PurchaseProduct(string productId)
    {
        if (googlePlayIAPBridge != null)
        {
            Debug.Log("googlePlayIAPBridge is not null");
            googlePlayIAPBridge.Call("purchaseProduct", productId);
        }
    }

    // Method to receive the IAP token from the Java/Kotlin class
    public void OnIAPTokenReceived(string iapToken)
    {
        // Handle the IAP token as needed
        Debug.Log("IAP Token: " + iapToken);
    }
}
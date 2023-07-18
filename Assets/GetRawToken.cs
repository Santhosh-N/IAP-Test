using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Purchasing;

public class GetRawToken : MonoBehaviour
{
    private AndroidJavaObject googlePlayIAPBridge;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Create an instance of the GooglePlayIAPBridge class com.yourpackage.GooglePlayIAPBridge
            using (var pluginClass = new AndroidJavaClass("com.VDOGAMES.IAPTest.GooglePlayIAPBridge"))
            {
                googlePlayIAPBridge = pluginClass.CallStatic<AndroidJavaObject>("instance", new object[] { this });
            }
        }
    }

    public void PurchaseProduct(string productId)
    {
        if (googlePlayIAPBridge != null)
        {
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
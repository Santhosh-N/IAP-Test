using UnityEngine;

public class IAPHandler : MonoBehaviour
{
    // Called from Java bridge to receive the purchase token
    public void OnPurchaseTokenReceived(string purchaseToken)
    {
        // Process the purchase token in Unity as needed
        Debug.Log("Raw IAP token received: " + purchaseToken);
    }

    // Call this method from Unity to initiate the Android activity and retrieve the token
    public void GetIAPToken()
    {
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass iapBridgeClass = new AndroidJavaClass("com.VDOGAMES.IAPTest.IAPBridge");
        currentActivity.Call("startActivity", iapBridgeClass);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPTokenReceiver : MonoBehaviour
{
    // Call this method to retrieve the IAP token from Java
    public void GetIAPToken()
    {
        // Use Unity's AndroidJavaClass to get the current activity
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // Replace "com.yourcompanyname.yourappname.GooglePlayBillingManager" with the actual package and class name of your Java file.
        // Ensure you have the correct package and class name.
        AndroidJavaClass javaClass = new AndroidJavaClass("com.VDOGAMES.IAPTest.GooglePlayBillingManager");

        // Start the activity to retrieve the IAP token
        javaClass.CallStatic("startActivity", activity);
    }

    // This method is called by the Java code when the IAP token is received
    public void OnIAPTokenReceived(string iapToken)
    {
        IAPTokenReceiver iapTokenReceiver = GetComponent<IAPTokenReceiver>();
        iapTokenReceiver.GetIAPToken();
        // Process the IAP token in Unity
        Debug.Log("Received IAP Token: " + iapToken);

        // Example: Send the IAP token to your server for verification
        // YourServer.VerifyPurchase(iapToken);
    }

    private void Start()
    {
     
    }
}

/*using UnityEngine;
public class IAPTokenGetter : MonoBehaviour
{
    private AndroidJavaObject currentActivity;
    private AndroidJavaObject unityPlayer;
    private AndroidJavaObject billingClient;

    private void Start()
    {
        // Get the UnityPlayer activity
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // Initialize the BillingClient
        AndroidJavaClass billingClass = new AndroidJavaClass("com.android.billingclient.api.BillingClient");
        billingClient = billingClass.CallStatic<AndroidJavaObject>("newBuilder", currentActivity)
            .Call<AndroidJavaObject>("setListener", new BillingListener()) // Create a custom BillingListener class
            .Call<AndroidJavaObject>("build");

        // Connect to the BillingClient
        billingClient.Call("startConnection", new BillingClientStateListener());
    }

    private class BillingClientStateListener : AndroidJavaProxy
    {
        public BillingClientStateListener() : base("com.android.billingclient.api.BillingClientStateListener") { }

        public void onBillingSetupFinished(AndroidJavaObject responseCode)
        {
            if (responseCode.Call<int>("getResponseCode") == BillingResponseCode.OK)
            {
                // Billing client setup successful
                QueryPurchaseHistory();
            }
        }

        public void onBillingServiceDisconnected()
        {
            // Handle billing service disconnection
        }
    }

    private void QueryPurchaseHistory()
    {
        AndroidJavaObject purchasesResult = billingClient.Call<AndroidJavaObject>("queryPurchaseHistory", "inapp");

        AndroidJavaObject purchaseHistoryRecords = purchasesResult.Call<AndroidJavaObject>("getPurchaseHistoryRecordList");
        int size = purchaseHistoryRecords.Call<int>("size");
        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject purchaseHistoryRecord = purchaseHistoryRecords.Call<AndroidJavaObject>("get", i);
            string purchaseToken = purchaseHistoryRecord.Call<string>("getPurchaseToken");
            // Handle the purchase token as needed
        }
    }

    private class BillingListener : AndroidJavaProxy
    {
        public BillingListener() : base("com.android.billingclient.api.PurchasesUpdatedListener") { }

        public void onPurchasesUpdated(AndroidJavaObject responseCode, AndroidJavaObject purchases)
        {
            if (responseCode.Call<int>("getResponseCode") == BillingResponseCode.OK)
            {
                // Handle successful purchases
            }
        }
    }
}*/
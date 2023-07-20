package com.VDOGAMES.IAPTest;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import com.android.billingclient.api.BillingClient;
import com.android.billingclient.api.BillingClient.BillingResponse;
import com.android.billingclient.api.BillingClientStateListener;
import com.android.billingclient.api.Purchase;
import com.android.billingclient.api.Purchase.PurchasesResult;
import com.android.billingclient.api.PurchasesUpdatedListener;
import java.util.List;

public class GooglePlayBillingManager extends Activity implements PurchasesUpdatedListener {
    private static final String TAG = "GooglePlayBilling";

    private BillingClient billingClient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Initialize the BillingClient
        billingClient = BillingClient.newBuilder(this).enablePendingPurchases().setListener(this).build();
        connectToBillingService();
    }

    private void connectToBillingService() {
        billingClient.startConnection(new BillingClientStateListener() {
            @Override
            public void onBillingSetupFinished(BillingResult billingResult) {
                if (billingResult.getResponseCode() == BillingResponse.OK) {
                    // The billing client connection is successful. You can now query purchases or start the purchase flow.
                    queryPurchases();
                } else {
                    Log.e(TAG, "onBillingSetupFinished: Error " + billingResult.getResponseCode());
                }
            }

            @Override
            public void onBillingServiceDisconnected() {
                // Implement retry logic if needed
            }
        });
    }

    private void queryPurchases() {
        PurchasesResult purchasesResult = billingClient.queryPurchases(BillingClient.SkuType.INAPP);
        if (purchasesResult.getResponseCode() == BillingResponse.OK) {
            List<Purchase> purchases = purchasesResult.getPurchasesList();
            if (purchases != null) {
                // Process the purchases and extract the IAP token from the Purchase object
                for (Purchase purchase : purchases) {
                    String iapToken = purchase.getPurchaseToken();
                    Log.d(TAG, "IAP Token: " + iapToken);

                    // Pass the IAP token back to Unity
                    UnityPlayer.UnitySendMessage("YourGameObjectName", "OnIAPTokenReceived", iapToken);

                    // Example: Send the IAP token to your server for verification
                    // YourServer.VerifyPurchase(iapToken);
                }
            }
        } else {
            Log.e(TAG, "queryPurchases: Error " + purchasesResult.getResponseCode());
        }
    }

    @Override
    public void onPurchasesUpdated(BillingResult billingResult, List<Purchase> purchases) {
        // Handle purchase updates, e.g., new purchases or pending updates
        if (billingResult.getResponseCode() == BillingResponse.OK && purchases != null) {
            // Process the purchases and extract the IAP token from the Purchase object
            for (Purchase purchase : purchases) {
                String iapToken = purchase.getPurchaseToken();
                Log.d(TAG, "IAP Token: " + iapToken);

                // Pass the IAP token back to Unity
                UnityPlayer.UnitySendMessage("IAPTokenReceiver", "OnIAPTokenReceived", iapToken);

                // Example: Send the IAP token to your server for verification
                // YourServer.VerifyPurchase(iapToken);
            }
        } else {
            Log.e(TAG, "onPurchasesUpdated: Error " + billingResult.getResponseCode());
        }
    }
}
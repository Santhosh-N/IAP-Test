import android.app.Activity;
import android.os.Bundle;
import androidx.annotation.NonNull;
import com.android.billingclient.api.BillingClient;
import com.android.billingclient.api.BillingClientStateListener;
import com.android.billingclient.api.BillingResult;
import com.android.billingclient.api.PurchasesUpdatedListener;
import com.android.billingclient.api.Purchase;

public class IAPBridge extends Activity {

    private BillingClient billingClient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Initialize the BillingClient
        billingClient = BillingClient.newBuilder(this)
                .setListener(new MyPurchasesUpdatedListener())
                .enablePendingPurchases()
                .build();

        // Connect the BillingClient
        billingClient.startConnection(new MyBillingClientStateListener());
    }

    // Define your own implementation of PurchasesUpdatedListener
    private class MyPurchasesUpdatedListener implements PurchasesUpdatedListener {
        @Override
        public void onPurchasesUpdated(@NonNull BillingResult billingResult, List<Purchase> purchases) {
            // Handle purchase updates here
        }
    }

    // Define your own implementation of BillingClientStateListener
    private class MyBillingClientStateListener implements BillingClientStateListener {
        @Override
        public void onBillingSetupFinished(@NonNull BillingResult billingResult)
        {
            if (billingResult.getResponseCode() == BillingClient.BillingResponseCode.OK) 
            {
                // BillingClient is connected and ready to use


                  QueryProductDetailsParams queryProductDetailsParams = QueryProductDetailsParams.newBuilder().setProductList(ImmutableList.of(Product.newBuilder()
                                                                           .setProductId("com.vdogames.iaptest.coin1000").setProductType(ProductType.INAPP).build())).build();

                  billingClient.queryProductDetailsAsync(queryProductDetailsParams, new ProductDetailsResponseListener() 
                  {
                     public void onProductDetailsResponse(BillingResult billingResult, List<ProductDetails> productDetailsList) 
                     {
                        // check billingResult
                        // process returned productDetailsList
                     }
                  })
            }
        }

        @Override
        public void onBillingServiceDisconnected() {
            // Handle the case where BillingClient connection is lost
             billingClient.startConnection(new MyBillingClientStateListener());
        }
    }
}
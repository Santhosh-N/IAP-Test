import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

public class IAPBridge extends Activity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        // Retrieve the purchase token from the intent
        Intent intent = getIntent();
        String purchaseToken = intent.getStringExtra("purchaseToken");
        
        // Pass the purchase token back to Unity
        UnityPlayer.UnitySendMessage("IAPHandler", "OnPurchaseTokenReceived", purchaseToken);
        
        finish();
    }
}
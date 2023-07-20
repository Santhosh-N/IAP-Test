package com.VDOGAMES.IAPTest;

import com.unity3d.player.UnityPlayerActivity;

public class UnityActivity extends UnityPlayerActivity {

    private static UnityActivity currentActivity;

    public static UnityActivity getCurrentActivity() {
        return currentActivity;
    }

    @Override
    protected void onResume() {
        super.onResume();
        currentActivity = this;
    }

    @Override
    protected void onPause() {
        super.onPause();
        currentActivity = null;
    }
}





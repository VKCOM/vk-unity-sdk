package com.vk.unity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.vk.sdk.VKAccessToken;
import com.vk.sdk.VKCallback;
import com.vk.sdk.VKSdk;
import com.vk.sdk.api.VKError;

public class LoginTransientActivity extends Activity {

    LoginRequest inputParams;

    public static void performLogin(String jsonParameters) {
        Intent intent = new Intent(UnityPlayer.currentActivity, LoginTransientActivity.class);
        intent.putExtra("JsonParameters", jsonParameters);
        // start self
        UnityWrapper.getUnityActivity().startActivity(intent);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        String jsonParams = getIntent().getStringExtra("JsonParameters");
        inputParams = new LoginRequest(jsonParams);
        VKSdk.login(this, inputParams.Scopes);
    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        Log.v(VK.TAG, "LoginTransientActivity onActivityResult(requestCode = " + requestCode +", resultCode = " + resultCode + ")");

        VKCallback<VKAccessToken> callback = new VKCallback<VKAccessToken>() {
            @Override
            public void onResult(VKAccessToken res) {
                sendSuccess(res);
            }

            @Override
            public void onError(VKError error) {
                sendError(error);
            }
        };


        if (!VKSdk.onActivityResult(requestCode, resultCode, data, callback)) {
            super.onActivityResult(requestCode, resultCode, data);
        }

        finish();
    }

    private void sendSuccess(VKAccessToken res) {
        new LoginResponse(inputParams.CallbackId, res).sendToUnity();
    }

    private void sendError(VKError error) {
        new LoginResponse(inputParams.CallbackId, error).sendToUnity();
    }
}

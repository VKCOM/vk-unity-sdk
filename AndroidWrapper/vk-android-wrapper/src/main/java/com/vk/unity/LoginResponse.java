package com.vk.unity;

import android.util.Log;

import com.vk.sdk.VKAccessToken;
import com.vk.sdk.api.VKError;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by alexs on 1/18/2017.
 */


public class LoginResponse extends  ResponseBase {

    private VKAccessToken accessToken;

    public LoginResponse(String callbackId, VKAccessToken accessToken){
        super(callbackId);
        this.accessToken = accessToken;
    }

    public LoginResponse(String callbackId, VKError error) {
        super(callbackId, error);
    }

    @Override
    protected String getMethodName() {
        return "OnLoginComplete";
    }

    @Override
    protected void fillTheResult(JSONObject resultObj) {
        try {
            if (accessToken != null) {
                resultObj.put("accessToken", accessToken.accessToken);
                resultObj.put("expiresIn", accessToken.expiresIn);
                resultObj.put("userId", accessToken.userId);
            }
        }
        catch (JSONException exc) {
            Log.e(VK.TAG, "LoginResponse.fillTheResult failed: " + exc.getMessage());
        }
    }
}

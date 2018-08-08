package com.vk.unity;

import android.util.Log;

import com.vk.sdk.VKAccessToken;

import org.json.JSONException;
import org.json.JSONObject;

public class AccessTokenChangedMessage extends  ResponseBase {

    private VKAccessToken accessToken;
    public AccessTokenChangedMessage(VKAccessToken newToken) {
        super("");
        accessToken = newToken;
    }


    @Override
    protected String getMethodName() {
        return "OnAccessTokenChanged";
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
        catch (JSONException jsonExc) {
            Log.e(VK.TAG, "AccessTokenChangedMessage.fillTheResult failed: " + jsonExc.getMessage());
        }
    }
}

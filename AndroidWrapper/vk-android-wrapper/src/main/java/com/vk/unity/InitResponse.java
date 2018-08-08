package com.vk.unity;

import android.util.Log;

import com.vk.sdk.VKAccessToken;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by alexs on 1/18/2017.
 */

public class InitResponse extends  ResponseBase{
    private boolean isLoggedIn;

    private VKAccessToken accessToken;

    private String certificateFingerprint;

    public InitResponse(String callbackId, boolean isLoggedIn, VKAccessToken accessToken,
                        String certificateFingerprint) {
        super(callbackId);
        this.isLoggedIn = isLoggedIn;
        this.accessToken = accessToken;
        this.certificateFingerprint = certificateFingerprint;
    }

    @Override
    protected String getMethodName() {
        return "OnInitComplete";
    }

    @Override
    protected void fillTheResult(JSONObject resultObj) {
        try {
            if (accessToken != null) {
                resultObj.put("accessToken", accessToken.accessToken);
                resultObj.put("expiresIn", accessToken.expiresIn);
                resultObj.put("userId", accessToken.userId);
            }

            resultObj.put("certificateFingerprint", certificateFingerprint);
        }
        catch (JSONException jsonExc) {
            Log.e(VK.TAG, "InitResponse.fillTheResult failed: " + jsonExc.getMessage());
        }
    }
}

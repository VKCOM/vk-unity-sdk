package com.vk.unity;

import android.util.Log;

import com.vk.sdk.api.VKError;

import org.json.JSONObject;

public class APIResponse extends ResponseBase {

    private String responseJsonString;

    public APIResponse(String callbackId, String jsonStr) {
        super(callbackId);
        responseJsonString = jsonStr;
    }

    public APIResponse(String callbackId, VKError error) {
        super(callbackId, error);
    }

    @Override
    protected String getMethodName() {
        return "OnAPICallComplete";
    }

    @Override
    protected void fillTheResult(JSONObject resultObj) {
        try {
            if (responseJsonString != null) {
                resultObj.put("responseJsonString", responseJsonString);
            }
        }
        catch(Exception exc) {
            Log.e(VK.TAG, "APIResponse.fillTheResult failed: " + exc.getMessage());
        }
    }
}

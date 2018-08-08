package com.vk.unity;

import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

// input: from unity managed to android native
public class RequestBase {
    public String CallbackId;
    public RequestBase(String jsonStr) {
        try {
            JSONObject o = new JSONObject(jsonStr);
            CallbackId = o.getString("callbackId");
        } catch (JSONException exc) {
            Log.e(VK.TAG, "RequestBase failed to parse params: " + jsonStr);
        }
    }
}

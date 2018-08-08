package com.vk.unity;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;





public class LoginRequest extends  RequestBase{

    public String[] Scopes;

    public LoginRequest(String jsonStr) {
        super(jsonStr);
        try {
            JSONObject o = new JSONObject(jsonStr);
            JSONArray scopesJsonArray = o.getJSONArray("scopes");
            Scopes = new String[scopesJsonArray.length()];
            for (int i = 0; i < scopesJsonArray.length(); i++) {
                Scopes[i] = scopesJsonArray.getString(i);
            }
        }
        catch (JSONException exc) {
            Log.e(VK.TAG, "Failed to parse login input params " + jsonStr);
        }
    }
}

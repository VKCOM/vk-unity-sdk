package com.vk.unity;

import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by alexs on 1/18/2017.
 */

public class InitRequest extends RequestBase{

    public int AppId;

    public String ApiVersion;

    public InitRequest(String jsonStr) {
        super(jsonStr);
        try {
            JSONObject o = new JSONObject(jsonStr);
            AppId = o.getInt("appId");
            ApiVersion = o.getString("apiVersion");
        }
        catch (JSONException exc) {
            Log.e(VK.TAG, "Failed to parse InitRequest params " + jsonStr);
        }
    }
}

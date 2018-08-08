package com.vk.unity;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Map;

/**
 * Created by alexs on 3/6/2017.
 */

public class APIRequest extends RequestBase {
    public String methodName;

    public ArrayList<String> parameters;

    public APIRequest(String jsonStr) {
        super(jsonStr);
        try {
            JSONObject o = new JSONObject(jsonStr);
            methodName = o.getString("methodName");
            parameters = new ArrayList<>();
            JSONArray jsonParamsArray = o.getJSONArray("parameters");
            for (int i=0;i<jsonParamsArray.length();i++) {
                parameters.add(jsonParamsArray.getString(i));
            }
        }
        catch (Exception exc) {
            Log.e(VK.TAG, "Failed to parse APIRequest params " + jsonStr);
        }
    }
}

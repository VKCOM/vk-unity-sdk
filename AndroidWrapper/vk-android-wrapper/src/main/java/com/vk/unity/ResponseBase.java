package com.vk.unity;

import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.vk.sdk.api.VKError;

import org.json.JSONException;
import org.json.JSONObject;

// from android native to unity managed
public abstract class ResponseBase {

    private VKError error;
    private String callbackId;

    protected abstract String getMethodName();

    public VKError getError() {
        return error;
    }



    public boolean getIsSuccess() {
        return getError() == null;
    }

    public ResponseBase(String callbackId) {
        this.callbackId = callbackId;
    }

    public ResponseBase(String callbackId, VKError error) {
        this(callbackId);
        this.error = error;
    }

    public String getAsStringData() {

        try {
            JSONObject obj = new JSONObject();

            obj.put("callbackId", callbackId);

            if (error != null) {
                JSONObject errJson = new JSONObject();
                errJson.put("errorCode", error.errorCode);
                errJson.put("errorMessage", error.errorMessage);
                errJson.put("errorReason", error.errorReason);

                obj.put("error", errJson);
            }
            else {
                fillTheResult(obj);
            }

            return obj.toString();
        }
        catch (JSONException jsonExc) {
            Log.e(VK.TAG, "ResponseBase.getAsStringData failed: " + jsonExc.getMessage());
            return "";
        }
    }

    public void sendToUnity() {
        String message = getAsStringData();
        Log.v(VK.TAG, "ResponseBase.sendToUnity sending: " + getMethodName() + "(" + message +")");
        UnityWrapper.SendMessage(VK.VK_UNITY_OBJ, getMethodName(), message);
    }

    protected abstract void fillTheResult(JSONObject resultObj);

}

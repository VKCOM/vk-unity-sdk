package com.vk.unity;

import android.support.annotation.Nullable;
import android.util.Log;

import com.vk.sdk.VKAccessToken;
import com.vk.sdk.VKAccessTokenTracker;
import com.vk.sdk.VKSdk;
import com.vk.sdk.api.VKError;
import com.vk.sdk.api.VKParameters;
import com.vk.sdk.api.VKRequest;
import com.vk.sdk.api.VKResponse;
import com.vk.sdk.util.VKUtil;

public class VK {

    static final String TAG = VK.class.getName();

    static final String VK_UNITY_OBJ = "VKUnityPlugin";

    public static void init(String jsonParameters) {
        Log.v(TAG, "init(" + jsonParameters +")");


        VKAccessTokenTracker accessTokenTracker = new VKAccessTokenTracker() {
            @Override
            public void onVKAccessTokenChanged(@Nullable VKAccessToken oldToken, @Nullable VKAccessToken newToken) {
                Log.v(TAG, "onVKAccessTokenChanged()");
                AccessTokenChangedMessage message = new AccessTokenChangedMessage(newToken);
                message.sendToUnity();
            }
        };

        accessTokenTracker.startTracking();

        InitRequest initRequest = new InitRequest(jsonParameters);

        VKSdk.customInitialize(UnityWrapper.getUnityActivity().getApplicationContext(),
                initRequest.AppId,
                initRequest.ApiVersion);


        String certFingerprint = fetchCertFingerprint();

        new InitResponse(initRequest.CallbackId,
                VKSdk.isLoggedIn(),
                VKSdk.getAccessToken(),
                 certFingerprint).sendToUnity();

    }

    private static String fetchCertFingerprint() {
        String[] fingerprints = VKUtil.getCertificateFingerprint(UnityWrapper.getUnityActivity(), UnityWrapper.getUnityActivity().getPackageName());
        if (fingerprints != null && fingerprints.length > 0) {
            return fingerprints[0];
        }
        return "";
    }

    public static void login(String jsonParameters) {
        Log.v(TAG, "login(" + jsonParameters + ")");
        LoginTransientActivity.performLogin(jsonParameters);
    }

    public static void logout() {
        VKSdk.logout();
    }

    public static void apiCall(String jsonParameters) {
        Log.v(TAG, "apiCall(" + jsonParameters +")");

        final APIRequest apiRequest = new APIRequest(jsonParameters);

        VKParameters requestParams  =  VKParameters.from(apiRequest.parameters.toArray(new Object[apiRequest.parameters.size()]));
        String methodName = apiRequest.methodName;
        VKRequest vkRequest = new VKRequest(methodName, requestParams);

        vkRequest.executeWithListener(new VKRequest.VKRequestListener() {
            @Override
            public void onComplete(VKResponse response) {
                new APIResponse(apiRequest.CallbackId, response.responseString).sendToUnity();
            }

            @Override
            public void onError(VKError error) {
                new APIResponse(apiRequest.CallbackId, error).sendToUnity();
            }
        });
    }

}

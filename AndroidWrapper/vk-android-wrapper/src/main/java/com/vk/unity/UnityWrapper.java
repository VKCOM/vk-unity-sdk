package com.vk.unity;

import android.app.Activity;

import com.unity3d.player.UnityPlayer;


public class UnityWrapper {

    public static Activity getUnityActivity() {
        return UnityPlayer.currentActivity;
    }

    public static void SendMessage(String unityObject, String unityMethod, String message) {
        UnityPlayer.UnitySendMessage(unityObject, unityMethod, message);
    }
}

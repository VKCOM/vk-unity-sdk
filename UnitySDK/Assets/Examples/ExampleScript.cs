using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VK.Unity;

public class ExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnLoginClick()
    {
        Debug.Log("Login clicked!");
        VK.Unity.VKSDK.Init(new VKInitParams { ApiVersion = "5.62", AppId = 5798402 }, () =>
        {
            Debug.Log("Initialization completed");

            VKSDK.LogIn(new List<Scope> { Scope.Audio, Scope.Messages }, (authResp) =>
            {
                Debug.Log("Logged in " + authResp.accessToken);
            });

        });

    }
}

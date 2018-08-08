# VK Unity SDK 
**VK Unity SDK** - tools wich uses VK API, implements many features to integrate your application with VK, works under Unity and created for developers who wants to quickly integrate applications. **(it uses .NET 4.6)**.

## Available platforms

 - Android
 - WebGL

## Try it!
For using VK Unity SDK you must install Unity since 2017.1 version. Download the latest version from [official website](https://unity3d.com/). You also need to download [VK Unity SDK](https://vk.com/dev/Unity_SDK).
Basic unity application included in .unitypackage file. You can start it from Unity. This package consists of:
* **VK.Unity library** which implemented tools for management API methods.
* **VK.Unity.Editor library** which provides interaction with Unity Editor.
* **Open sans** font
* **Plugins** for correct interaction between platforms
* **Executable .cs file**

When you run the .unitypackage file, Unity will ask you to import mentioned files, you might agree this. Header menu will be include **VK** item, inside this item you can see **Edit settings** button and when you clicked on it, settings menu will be visible for you. Inside this menu you can set `API Version` and `Application ID`.

MainMenu scene exists Inside **VKSDK/Examples** folder, you need to move this scene into your project. Also source code of the project you can find inside **Scripts** folder. Elements of user interface will be created automatically in programming code for MainMenu scene therefore you can just build application for platform.

### WebGL. Warnings
`Application ID` from Unity Editor which you set must be equal with IFrame application id. Our methods available only inside VK application page.


## VKSDK Object
### Fields
| Field | Available on Android | Available on WebGL |
| ------ | ------ | ------ |
| [IsLoggedIn](#isloggedin) | + | - |
| [IsInitialized](#docnav) | + | + |
| [OnAccessTokenChanged](#docnav) | + | - |
| [AppId](#docnav) | + | + |
| [SDKVersion](#docnav) | + | + |
| [AccessToken](#docnav) | + | - |
| [UserId](#docnav) | + | - |
### Functions
| Function | Available on Android | Available on WebGL |
| ------ | ------ | ------ |
| [AddCallback](#docnav) | - | + |
| [API]() | + | + |
| [GetExtraData]() | + | + |
| [Init]() | + | + |
| [Login]() | + | - |
| [Logout]() | + | - |
| [RemoveCallback]() | - | + |

#### IsLoggedIn

```cs
public static bool IsLoggedIn
```
This field contains `true` if method [Login](#login) executed successfully and `false` if not.
**Example:**
Enable GUI when a script is enabled. It will be done if [Login](#login) successfully finished.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    void Start() {
        GUI.enabled = VKSDK.IsLoggedIn;
    }
}
```
***
#### IsInitialized

```cs
public static bool IsInitialized
```
Like a [IsLoggedIn](#isloggedin) this filed contains `true` if [Init](#init) method successfully finished. 
**Example:**
Getting friends list using [API](#api) method of sdk if initialization complete successfully. If you have not called [Init](#init) method before, then will be warning message.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    protected void FriendsFromVK() {
        if (VKSDK.IsInitialized) {
            VKSDK.API("friends.get", new Dictionary<string, string>() { { "order", "name" } }  , (result) =>
            {
                Debug.Log("Your VK friends: " + result.ToString());
            });
        }
        else
        {
            Debug.Warn("You must to run VKSDK.Init() first");
        }
    }
}
```
***
#### OnAccessTokenChanged

```cs
public static Action<AccessToken> OnAccessTokenChanged
```
***
#### AppId

```cs
public static long AppId
```
This field contains application id you set so in unity editor.
**Example:**
For example you can create link to your VK IFrame application.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    private string AppLink() {
        return "https://vk.com/app" + VKSDK.AppId;
    }
}
```
***
#### SDKVersion

```cs
public static string SDKVersion
```
**SDKVersion** contains current version of VK Unity SDK.
**Example:**
Method writes to console information about developer and used software.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    private static readonly string STARTING_YEAR = "2018";

    private string Credits() {
        Debug.Log("Application created using VK Unity SDK v" + VKSDK.SDKVersion);
        Debug.Log("Supporting by me since " + STARTING_YEAR);
    }
}
```
***
#### AccessToken

```cs
public static AccessToken AccessToken
```
Returns you current `AccessToken` Object. This object contains fields `TokenString`, `ExpiresIn`, `UserId`. Some platforms uses access tokens to identify your application, this object stores your current access token. More about access tokens [here](https://vk.com/dev/access_token).
**Example:**
Our method show information about requested access token.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    private string AccessTokenInfo(AccessToken at) {
        Debug.Log("Token info:");
        Debug.Log("Owner`s userId: " + at.UserId);
        Debug.Log("Lifetime to: " + at.ExpiresIn);
    }
}
```
***
#### UserId

```cs
public static int UserId
```
**UserId** contains UserId from current `AccessToken` object. Look at [AccessToken](#accesstoken) to see example.
***
#### AddCallback

```cs
public static void AddCallback(string eventName, Action<APICallResponse> callback = null)
```
Look at the [VK JavaScript SDK](https://vk.com/dev/Javascript_SDK) it's supports [adding callbacks](https://vk.com/dev/Javascript_SDK?f=4.1.%20VK.addCallback) and [removing callbacks](https://vk.com/dev/Javascript_SDK?f=4.2.%20VK.removeCallback). List of available events [here](https://vk.com/dev/Javascript_SDK?f=4.3.%20%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA%20%D1%81%D0%BE%D0%B1%D1%8B%D1%82%D0%B8%D0%B9). Supporting WebGL in Unity SDK based on our JavaScript SDK. This method adding your function as callback of one of some events, and call it when event occurs.
**Example:**
Here created function, wich adds callback on `onSettingsChanged`, stores callback id and gives info about event on console.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    private string callbackListenerId = "";
#if UNITY_WEBGL
    private void AddCallback()
    {
        VKSDK.AddCallback("onSettingsChanged", (result) => {
            Debug.Log("Event onSettingsChanged with result: " + result.ToString());
            callbackListenerId = result.callbackId;
        });
    }
#endif
}
```
***
#### API

```cs
public static void API(string method, IDictionary<string, string> queryParams, Action<APICallResponse> callback = null)
```
This method make request to VK API. Full list of methods available [here](https://vk.com/dev/methods).
**Example:** You may check [IsInitialized](#isinitialized) which using API method.
***
#### GetExtraData

```cs
public static string GetExtraData(string key)
```
***
#### Init

```cs
public static void Init(VKInitParams initParams, Action initializedCallback = null)
public static void Init(Action initializedCallback = null)
```
This method initialize your application and makes available every SDK methods to execute.
**Example:**
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    void Start() {
        VKSDK.Init();
    }
}
```
***
#### Login

```cs
public static void Login(IEnumerable<Scope> scope, Action<AuthResponse> callback = null)
```
***
#### Logout

```cs
public static void Logout()
```
***
#### RemoveCallback

```cs
public static void RemoveCallback(string eventName, string callbackId)
```
**Example:**
This function removes callback which has been added earlier by [AddCallback](#addcallback) function using callback id.
```cs
using UnityEngine;
using VK.Unity;

public class ExampleClass : MonoBehaviour {
    private string callbackListenerId = "1";
#if UNITY_WEBGL
    private void AddCallback()
    {
        VKSDK.RemoveCallback("onSettingsChanged", callbackListenerId);
    }
#endif
}
```




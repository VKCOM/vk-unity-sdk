VK SDK for Unity v.0.9

Usage:

- import vksdk.unutipackage
- Unity menu -> VK -> Edit Settings: provide your vk application id and (optionally) desired API version.  
- Setup your vk app (https://vk.com/editapp?id=[your_app_id]) with proper package name and certificate fingerprint (as described here https://vk.com/dev/android_sdk)
- Go to File->Build Settings. Drag MainMenu scene from VKSDK/Examples folder.
- Choose Android platform, Build And Run.

Limitations and known issues
- only Android supported in current version
- custom AndroidManifest is used and it may interfere with other plugins which customize the manifest (Facebook, in particular).




var VKJSPlugin = {
	Handler: function(method, data, callback) {
		var Init = function(SDKVersion) {
			VKSDKVersion = SDKVersion;
			var SDKScriptElement = document.createElement('script');
			SDKScriptElement.src = VK_JS_SDK_URL;
			SDKScriptElement.type = 'text/javascript';
			SDKScriptElement.onload = onLoad;
			document.getElementsByTagName('body')[0].append(SDKScriptElement);
			
			function onLoad() {
				VK.init(null, null, VKSDKVersion);	
			}
		};

		var API = function(data, callbackId) {
			
			var arrayToObject = function(array) { // it will be destroyed later
				var answer = {};
				for (var i = 0; i < array.length - 1; i += 2) {
					answer[array[i]] = array[i + 1];
				}
				return answer;
			}; // --
			
			data = JSON.parse(data);
			VK.api(data.methodName, arrayToObject(data.parameters), function(APIResponse) {
				var answer = prepareResponse(APIResponse, data.callbackId);
				sendResponse(answer, callbackId);
			});
		};
		
		var AddCallback = function(data, callback) {
			data = JSON.parse(data);
			VKEventExternalCallbacks[data.callbackId] = callback;
			VK.addCallback(data.eventName, eventCallbackHandler.bind({ callbackId: data.callbackId }));
		};
		
		var RemoveCallback = function(data) {
			data = JSON.parse(data);
			VK.removeCallback(data.eventName, eventCallbackHandler.bind({ callbackId: data.callbackId }));
		}
		
		var eventCallbackHandler = function() {
			var eventObject = {
				response: arguments
			};
			var answer = prepareResponse(eventObject, this.callbackId);
			sendResponse(answer, VKEventExternalCallbacks[this.callbackId]);
		};
		
		var prepareResponse = function(responseObject, requestedCallbackId) {
			/* Fields named like APICallResponse Object*/
			var response = {
				responseJsonString: null,
				callbackId: requestedCallbackId,
				error: null
			};
			
			if (responseObject.error) {
				response.responseJsonString = responseObject.error;
				response.error = {
					errorСode: responseObject.error.error_code,
					errorMessage: responseObject.error.error_msg
				};
			} else {
				response.responseJsonString = responseObject.response;
			}
			response.responseJsonString = encodeURIComponent(JSON.stringify(response.responseJsonString));
			
			return JSON.stringify(response);
		};
		
		var sendResponse = function(response, callback) {
			var bufferSize = lengthBytesUTF8(response) + 1;
			var responseBuffer = _malloc(bufferSize);
			stringToUTF8(response, responseBuffer, bufferSize);
			
			Runtime.dynCall('vi', callback, [responseBuffer]);
			
			_free(responseBuffer);
		};
		
		var VK_JS_SDK_URL = 'https://vk.com/js/api/xd_connection.js?2';
		var AVAILABLE_FUNCTIONS = ['Init', 'API', 'AddCallback', 'RemoveCallback'];
		var FUNCTIONS = [Init, API, AddCallback, RemoveCallback];
		
		var VKSDKVersion = 0;
		var VKEventExternalCallbacks = []; // storage for C# callback functions on VK events
		
		method = Pointer_stringify(method);
		data = Pointer_stringify(data);
		
		var functionNumber = AVAILABLE_FUNCTIONS.indexOf(method); 
		if (~functionNumber) {
			return FUNCTIONS[functionNumber](data, callback);
		}
		console.warn('Call to undefined method');
	}
};

mergeInto(LibraryManager.library, VKJSPlugin);
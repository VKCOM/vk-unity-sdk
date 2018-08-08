using System.Collections.Generic;

namespace VK.Unity.Results
{
    public interface IVKResponse
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>A collection of key values pairs that are parsed from the json result.</value>
        IDictionary<string, object> ResultDictionary { get; }

        /// <summary>
        /// Gets the raw json result string.
        /// </summary>
        /// <value>The raw json result string.</value>
        string JsonString { get; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The error string from the result. If no error occured value is null or empty.</value>
        string Error { get; }
    }
}

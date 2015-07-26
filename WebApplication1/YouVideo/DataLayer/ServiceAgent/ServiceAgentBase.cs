using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.DataLayer.ServiceAgent
{
    internal abstract class ServiceAgentBase : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAgentBase" /> class.
        /// This constructor is not really used
        /// </summary>
        protected ServiceAgentBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAgentBase" /> class.
        /// </summary>
        /// <param name="serviceBaseAddress">The base address (url) of the service</param>
        protected ServiceAgentBase(string serviceBaseAddress)
            : this(serviceBaseAddress, AcceptedMediaType.Json)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAgentBase" /> class.
        /// </summary>
        /// <param name="serviceBaseAddress">The base address (url) of the service</param>
        /// <param name="acceptedMediaType">The MediaType that is accepted by the service. The Default is JSON</param>
        protected ServiceAgentBase(string serviceBaseAddress, AcceptedMediaType acceptedMediaType)
        {
        }

        /// <summary>
        /// This is a temp metthod that does an HTTP POST asynchronously and returns an instance
        /// of the generic type parameter
        /// </summary>
        /// <param name="addressSuffix">Any suffix to the Base Url</param>
        /// <param name="formDataCollection">A collection of form data that needs to be posted to the service</param>
        /// <returns>An instance of the generic type specified</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Justification = "We want to be able to use a address suffix rather than a full url")]
        public Task<string> PostAsyncJson(string addressSuffix, FormDataCollection formDataCollection)
        {
            return PostAsyncJsonCore(addressSuffix, formDataCollection);
        }

        /// <summary>
        /// This is a generic metthod that does an HTTP POST asynchronously and returns an instance
        /// of the generic type parameter
        /// </summary>
        /// <typeparam name="TResourceModel">The type of the return type</typeparam>
        /// <param name="addressSuffix">Any suffix to the Base Url</param>
        /// <param name="formDataCollection">A collection of form data that needs to be posted to the service</param>
        /// <returns>An instance of the generic type specified</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Justification = "We want to be able to use a address suffix rather than a full url")]
        public Task<TResourceModel> PostAsync<TResourceModel>(string addressSuffix, FormDataCollection formDataCollection)
        {
            return PostAsyncCore<TResourceModel>(addressSuffix, formDataCollection);
        }

        /// <summary>
        /// This is a generic metthod that does an HTTP POST asynchronously. This method
        /// sends out the data parameter encoded as per the media type formatter provided (JSON/XML etc.)
        /// and returns an instance of the generic type parameter
        /// </summary>
        /// <typeparam name="TInput">The type of the input type</typeparam>
        /// <typeparam name="TOutput">The type of the output type</typeparam>
        /// <param name="addressSuffix">Any suffix to the Base Url</param>
        /// <param name="data">An instance of a type that you want serialized and sent across the wire</param>
        /// <param name="mediaTypeFormatter">An instance of a MediaTypeFormatter that will be used format the HTTP Request (JSON/XML etc.) and parse the HTTP Response</param>
        /// <returns>An instance of a type that has been deserialized from the HTTP Response</returns>
        public Task<TOutput> PostAsync<TInput, TOutput>(string addressSuffix, TInput data, MediaTypeFormatter mediaTypeFormatter)
        {
            return PostAsyncCore<TInput, TOutput>(addressSuffix, data, mediaTypeFormatter);
        }

        /// <summary>
        /// This is a generic metthod that does an HTTP GET asynchronously and returns an instance
        /// of the generic type parameter
        /// </summary>
        /// <typeparam name="TResourceModel">The type of the return type</typeparam>
        /// <param name="addressSuffix">Any suffix to the Base Url</param>
        /// <param name="queryParameters">A collection of name value pairs that need to be passed as "query" parameters</param>
        /// <returns>An instance of a Task of the generic type parameter</returns>
        public Task<TResourceModel> GetAsync<TResourceModel>(string addressSuffix, FormDataCollection queryParameters)
        {
            return GetAsyncCore<TResourceModel>(addressSuffix, queryParameters);
        }


        /// <summary>
        /// Disposes off the instance
        /// </summary>
        public void Dispose()
        {
            DisposeCore();
        }

        protected abstract Task<TResourceModel> PostAsyncCore<TResourceModel>(string addressSuffix, FormDataCollection formDataCollection);
        protected abstract Task<string> PostAsyncJsonCore(string addressSuffix, FormDataCollection formDataCollection);
        protected abstract Task<TOutput> PostAsyncCore<TInput, TOutput>(string addressSuffix, TInput data, MediaTypeFormatter mediaTypeFormatter);
        protected abstract Task<TResourceModel> GetAsyncCore<TResourceModel>(string addressSuffix, FormDataCollection queryParameters);
        protected abstract void DisposeCore();
    }
}

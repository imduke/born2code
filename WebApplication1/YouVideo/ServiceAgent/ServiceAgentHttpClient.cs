using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DomainLayer.Exceptions;

namespace YouVideo.ServiceAgent
{
    /// <summary>
    /// This class is a generic Service Agent that includes
    /// HTTP functionality and abstracts it.
    /// </summary>
    internal class ServiceAgentHttpClient : IDisposable
    {
        #region Private members

        /// <summary>
        /// Hold a reference to the Exception types in the current assembly
        /// </summary>
        private static readonly Dictionary<string, Type> ExceptionRegistry = InitializeExceptionRegistry();

        /// <summary>
        /// A flag indicating whether this instance has been Disposed or not
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Hold a reference to the instance of an HttpClient object that this class uses internally
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Hold a reference to the Media Type (in  the form of a string) that the service accepts
        /// </summary>
        private string mediaType;

        /// <summary>
        /// Hold a reference to an array of MediaTypeFormatters
        /// </summary>
        private MediaTypeFormatter[] mediaTypeFormatters;

        #endregion Private Members


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAgentHttpClient" /> class.
        /// </summary>
        /// <param name="serviceBaseAddress">The base address (url) of the service</param>
        public ServiceAgentHttpClient(string serviceBaseAddress)
            : this(serviceBaseAddress, AcceptedMediaType.Json)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAgentHttpClient" /> class.
        /// </summary>
        /// <param name="serviceBaseAddress">The base address (url) of the service</param>
        /// <param name="acceptedMediaType">The MediaType that is accepted by the service. The Default is JSON</param>
        public ServiceAgentHttpClient(string serviceBaseAddress, AcceptedMediaType acceptedMediaType)
            : this(serviceBaseAddress, acceptedMediaType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAgentHttpClient" /> class.
        /// </summary>
        /// <param name="serviceBaseAddress">The base address (url) of the service</param>
        /// <param name="acceptedMediaType">The MediaType that is accepted by the service. The Default is JSON</param>
        /// <param name="networkCredentials">An instance of a NetworkCredential class initialized with the UserName and Password required to access the network resource</param>
        public ServiceAgentHttpClient(string serviceBaseAddress, AcceptedMediaType acceptedMediaType, NetworkCredential networkCredentials = null)
        {
            this.InitializeContentTypeSpecificAttributes(acceptedMediaType);
            this.httpClient = this.MakeHttpClient(serviceBaseAddress, networkCredentials);
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
            var content = CreateFormContent(formDataCollection);

            return TaskExtensions.Unwrap<TResourceModel>(this.httpClient.PostAsync(addressSuffix, content).ContinueWith(task =>
            {
                content.Dispose();
                var responseMessage = task.Result;
                EnsureSuccessStatusCodeBetter(responseMessage);
                return responseMessage.Content
                                      .ReadAsAsync<TResourceModel>(this.mediaTypeFormatters)
                                      .ContinueWith(readTask => readTask.Result);
            }));
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
            return TaskExtensions.Unwrap<TOutput>(this.httpClient.PostAsync<TInput>(addressSuffix, data, mediaTypeFormatter).ContinueWith(
                    (task) =>
                    {
                        var responseMessage = task.Result;
                        EnsureSuccessStatusCodeBetter(responseMessage);
                        return responseMessage.Content.ReadAsAsync<TOutput>(
                            new MediaTypeFormatter[] { mediaTypeFormatter })
                                              .ContinueWith((readTask) => readTask.Result);
                    }));
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
            var serviceUri = queryParameters == null ?
                addressSuffix : addressSuffix + "?" + GetNameValueDelimited(queryParameters);

            return TaskExtensions.Unwrap<TResourceModel>(this.httpClient.GetAsync(serviceUri).ContinueWith(task =>
            {
                var responseMessage = task.Result;
                EnsureSuccessStatusCodeBetter(responseMessage);
                return responseMessage.Content.ReadAsAsync<TResourceModel>().ContinueWith(readTask => readTask.Result);
            }));
        }

        /// <summary>
        /// Disposes off the instance
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual HttpClientHandler MakeHttpClientHandler()
        {
            return new HttpClientHandler{ AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip };
        }

        /// <summary>
        /// This method takes in an instance of a FormDataCollection and
        /// returns a string constructed in the form name1=value1&amp;name2=value2
        /// </summary>
        /// <param name="formDataCollection">A collection of name-value pairs</param>
        /// <returns>Returns a string that is constructed in the form name1=value1&amp;name2=value2</returns>
        private static string GetNameValueDelimited(FormDataCollection formDataCollection)
        {
            var sb = new StringBuilder();
            foreach (var formField in formDataCollection)
            {
                sb.Append(formField.Key + "=" + formField.Value + "&");
            }

            if (sb.Length > 0)
            {
                sb = sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// This method takes in an instance of FormDataCollection and
        /// returns an instance of StringContent whose "data" is structured
        /// like it would need to be in order to do an HTML form POST with an
        /// enctype of application/x-www-form-urlencoded
        /// </summary>
        /// <param name="formDataCollection">A collection of name-value pairs</param>
        /// <returns>An instance of a StringContent whose encoding is set to UTF 8 and media type is set to application/x-www-form-urlencoded</returns>
        private static StringContent CreateFormContent(FormDataCollection formDataCollection)
        {
            return new StringContent(
                GetNameValueDelimited(formDataCollection),
                Encoding.UTF8,
                "application/x-www-form-urlencoded");
        }

        /// <summary>
        /// This method initializes the local ExceptionRegistry cache
        /// </summary>
        /// <returns>A Dictionary of Exceptions extracted from the currently executing assembly</returns>
        private static Dictionary<string, Type> InitializeExceptionRegistry()
        {
            var exceptionRegistry = new Dictionary<string, Type>();

            // Register the base exception type GeicoEnterpriseSecurityException
            var baseExceptionType = typeof(YouVideosBaseException);
            exceptionRegistry.Add(baseExceptionType.Name, baseExceptionType);

            // Get the current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Register all descendents of GeicoEnterpriseSecurityException in the registry
            var secuirtyExceptionTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(baseExceptionType));
            foreach (var type in secuirtyExceptionTypes)
            {
                exceptionRegistry.Add(type.Name, type);
            }

            return exceptionRegistry;
        }

        /// <summary>
        /// This method enhances the HttpResponseMessage.EnsureSuccessStatusCode method
        /// to better handle domain exceptions that are thrown across REST boundaries.
        /// </summary>
        /// <param name="responseMessage">The HttpResponseMessage to evaluate.</param>
        private static void EnsureSuccessStatusCodeBetter(HttpResponseMessage responseMessage)
        {
            // Check the response code of the message
            if (!responseMessage.IsSuccessStatusCode)
            {
                // if we have a non-successful code get the error message from the Content of the response
                var exceptionMessage =
                    responseMessage.Content.ReadAsStringAsync().ContinueWith((readTask) => readTask.Result).Result;

                // locate the proper exception in the exception registry based on the ReasonPhrase of the response
                if (ExceptionRegistry.ContainsKey(responseMessage.ReasonPhrase))
                {
                    var exceptionType = ExceptionRegistry[responseMessage.ReasonPhrase];
                    if (exceptionType != null)
                    {
                        // Create and throw the new exception passing the extracted exceptionMessage to the constructor
                        var parameters = new object[] { exceptionMessage };
                        var exception =
                            Activator.CreateInstance(exceptionType, parameters) as YouVideosBaseException;
                        throw exception;
                    }
                }

                // if we don't have a matching exception in the registry, perform the standard check
                responseMessage.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// This method is to be used internally. This is the method that actually creates an instance of HttpClient
        /// If the networkCredentials parameter is not null the HttpClient instance will be assigned a ClientHandler
        /// if the networkCredentials parameter is null, then the HttpClient is created without a ClientHandler assigned
        /// </summary>
        /// <param name="networkCredentials">An instance of a NetworkCredential class initialized with the UserName and Password required to access the network resource</param>
        /// <returns>An instance of an HttpClient</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The purpose of this method is to create an instance and retrn it")]
        private HttpClient InternalMakeHttpClient(NetworkCredential networkCredentials)
        {
            var httpClientHandler = MakeHttpClientHandler();
            if (networkCredentials != null)
            {                
                httpClientHandler.Credentials = networkCredentials;
                this.httpClient = new HttpClient(httpClientHandler, true);
            }
            else
            {
                this.httpClient = new HttpClient(httpClientHandler);
            }

            return this.httpClient;
        }

        /// <summary>
        /// This method initilizes an instance of HttpClient
        /// </summary>
        /// <param name="serviceBaseAddress">The base address (url) of the service</param>
        /// <param name="networkCredentials">An instance of a NetworkCredential class initialized with the UserName and Password required to access the network resource</param>
        /// <returns>An instance of an HttpClient</returns>
        private HttpClient MakeHttpClient(string serviceBaseAddress, NetworkCredential networkCredentials)
        {
            this.httpClient = this.InternalMakeHttpClient(networkCredentials);
            this.httpClient.BaseAddress = new Uri(serviceBaseAddress);
            this.httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(this.mediaType));
            this.httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            this.httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
            this.httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("GeicoHttpClient", "1.0")));
            return this.httpClient;
        }

        /// <summary>
        /// This method initializes the MediaTypeFormatter thaat will be used for the HTTP communications
        /// </summary>
        /// <param name="acceptedMediaType">The MediaType that is accepted by the service.</param>
        private void InitializeContentTypeSpecificAttributes(AcceptedMediaType acceptedMediaType)
        {
            switch (acceptedMediaType)
            {
                case AcceptedMediaType.Json:
                    this.mediaType = "application/json";
                    this.mediaTypeFormatters = new MediaTypeFormatter[] { new JsonMediaTypeFormatter() };
                    break;
                case AcceptedMediaType.Xml:
                    this.mediaType = "application/xml";
                    this.mediaTypeFormatters = new MediaTypeFormatter[] { new XmlMediaTypeFormatter() };
                    break;
                default:
                    this.mediaType = "application/xml";
                    break;
            }
        }

        /// <summary>
        /// This method is called by the public Dispose method
        /// </summary>
        /// <param name="disposing">A flag indicating whether this instance is being Disposed</param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.httpClient != null)
                {
                    var hc = this.httpClient;
                    this.httpClient = null;
                    hc.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}

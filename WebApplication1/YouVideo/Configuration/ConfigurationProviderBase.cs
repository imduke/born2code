using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouVideo.DomainLayer.Exceptions;

namespace YouVideo.Configuration
{
    internal abstract class ConfigurationProviderBase
    {
        /// <summary>
        /// This Configuration parameter is Optional.
        /// </summary>
        public string DomainFacadeServiceLocatorClass
        {
            get
            {
                return GetDomainFacadeServiceLocatorClass();
            }
        }

        /// <summary>
        /// This Configuration parameter is Required
        /// </summary>
        public string TranscodingServiceUrl
        {
            get
            {
                var transcodingServiceUrl = GetTranscodingServiceUrl();
                if (string.IsNullOrEmpty(transcodingServiceUrl))
                    throw new ConfigurationParameterMissingException(string.Format("A (Required) Configuration Parameter: {0}, was not found in the appSettings section of the Configuration File", transcodingServiceUrl));
                if (transcodingServiceUrl.EndsWith("/"))
                    return transcodingServiceUrl;
                else
                    return transcodingServiceUrl + @"/";
            }
        }

        /// <summary>
        /// This Configuration Parameter is Optional. This parameter determines the configured "Behavior" of the DataService.
        /// That is polymorphic behavior of the DataService
        /// </summary>
        public string BehaviorDataService
        {
            get
            {
                return GetBehaviorDataService();
            }
        }

        protected abstract string GetDomainFacadeServiceLocatorClass();
        protected abstract string GetTranscodingServiceUrl();
        protected abstract string GetBehaviorDataService();
    }
}

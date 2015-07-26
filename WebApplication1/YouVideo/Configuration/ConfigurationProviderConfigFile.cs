using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace YouVideo.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class ConfigurationProviderConfigFile : ConfigurationProviderBase
    {
        protected override string GetDomainFacadeServiceLocatorClass()
        {
            return System.Configuration.ConfigurationManager.AppSettings["domainFacadeServiceLocatorClass"];
        }

        protected override string GetTranscodingServiceUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings["transcodingServiceUrl"];
        }

        protected override string GetBehaviorDataService()
        {
            return System.Configuration.ConfigurationManager.AppSettings["behavior:DataService"];           
        }
    }
}

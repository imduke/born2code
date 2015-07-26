using YouVideo.DataLayer;
using YouVideo.DomainLayer;
using YouVideo.DomainLayer.Managers;
using YouVideo.DomainLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.Configuration;
using YouVideo.DomainLayer.Exceptions;

namespace YouVideo.DomainLayer.ServiceLocator
{
    /// <summary>
    /// This is the "Production" version of the ServiceLocator
    /// The idea is that we pretty much know what instances of what classes are reuired in production and so
    /// rather than using reflection and/or indirection this class simply "news" up the appropriate instance directly
    /// </summary>
    internal sealed class DomainFacadeServiceLocatorProduction : DomainFacadeServiceLocatorBase
    {
        /// <summary>
        /// In this project the useage/design DataServiceBase and descendants is such that there are 2 implementation
        /// that could be used in production. The DataService class and the DataServiceGateway class. The DataServiceProvider
        /// class determines which instance to create but it uses the ServiceLocator in order to create the actual instance
        /// </summary>
        /// <param name="identifier">An identifier that identifies the actual instance to create. For now the options are<para>1. DataService</para><para>2. DataServiceGateway</para></param>
        /// <returns></returns>
        protected override DataServiceBase CreateDataServiceNamedCore(string identifier)
        {
            if (string.Compare(identifier, "DataService", StringComparison.OrdinalIgnoreCase) == 0)
                return new DataService();
            else if (string.Compare(identifier, "DataServiceGateway", StringComparison.OrdinalIgnoreCase) == 0)
                return new DataServiceGateway();
            else
                throw new DataServiceUnknownException(string.Format("A descendant of DataServiceBase class identified by: \"{0}\", was not found.", identifier));
        }

        protected override MemberManagerBase CreateMemberManagerCore()
        {
            return new MemberManager();
        }

        protected override DomainLayer.Managers.VideoManagerBase CreateVideoManagerCore()
        {
            return new VideoManager();
        }

        protected override EmailServiceBase CreateEmailServiceCore()
        {
            return new EmailService();
        }

        protected override Configuration.ConfigurationProviderBase CreateConfigurationProviderCore()
        {
            return new ConfigurationProviderConfigFile();
        }
    }
}

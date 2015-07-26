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

namespace YouVideo.DomainLayer.ServiceLocator
{
    internal abstract class DomainFacadeServiceLocatorBase
    {
        /// <summary>
        /// In this project the design is such that the DataServiceBase class
        /// has two descendants that are both useable in production. Which one to
        /// use is configurable, but nonetheless the ServiceLocator API needs to support
        /// instantiating an instance given an "Identifier" of sorts
        /// </summary>
        /// <param name="identifier">An Identifier that is usually the class name of the class to instantiate</param>
        /// <returns></returns>
        public DataServiceBase CreateDataServiceNamed(string identifier)
        {
            return CreateDataServiceNamedCore(identifier);
        }

        public MemberManagerBase CreateMemberManager()
        {
            return CreateMemberManagerCore();
        }

        public VideoManagerBase CreateVideoManager()
        {
            return CreateVideoManagerCore();
        }

        public EmailServiceBase CreateEmailService()
        {
            return CreateEmailServiceCore();
        }

        public ConfigurationProviderBase CreateConfigurationProvider()
        {
            return CreateConfigurationProviderCore();
        }

        protected abstract DataServiceBase CreateDataServiceNamedCore(string identifer);
        protected abstract MemberManagerBase CreateMemberManagerCore();
        protected abstract VideoManagerBase CreateVideoManagerCore();
        protected abstract EmailServiceBase CreateEmailServiceCore();
        protected abstract ConfigurationProviderBase CreateConfigurationProviderCore();
    }
}

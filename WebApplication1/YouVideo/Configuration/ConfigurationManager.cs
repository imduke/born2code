using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.DomainLayer.ServiceLocator;

namespace YouVideo.Configuration
{
    internal static class ConfigurationManager
    {
        private static DomainFacadeServiceLocatorBase serviceLocator;
        internal static DomainFacadeServiceLocatorBase ServiceLocator { get { return serviceLocator ?? (serviceLocator = DomainFacadeServiceLocatorResolver.Resolve()); } }

        /// <summary>
        /// This class is intentionally called ConfigurationManager so that if you see a "conflict"
        /// when using ConfigurationManager is any "unit" you'll know you're referencing the .NET
        /// version of the confiuration manager class.
        /// <para>Used For Unit Testing Purposes, wherein you can assign this Factory and have it
        /// return an instance of a ConfigurationProviderBase descendant of your choosing</para>
        /// </summary>
        public static Func<ConfigurationProviderBase> ConfigurationProviderFactory;

        public static ConfigurationProviderBase ConfigurationProvider
        {
            get
            {
                if (ConfigurationProviderFactory == null)
                    return ServiceLocator.CreateConfigurationProvider();
                else
                    return ConfigurationProviderFactory();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouVideo.Configuration;
using YouVideo.DataLayer;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.DomainLayer.ServiceLocator;

namespace YouVideo.Providers
{
    /// <summary>
    /// In this application there are two possible DataServiceBase class descendants.
    /// 1. DataService
    /// 2. DataServiceGateway
    /// A configuration setting determines which instance the DataServiceProvide (this class) will "Provide"
    /// A use case for this is that different deployments may require to be configured to use either one
    /// or the other instance.
    /// </summary>
    internal static class DataServiceProvider
    {
        public static readonly string DefaultDataServiceClassName = "DataService";

        private static DomainFacadeServiceLocatorBase serviceLocator;
        private static DomainFacadeServiceLocatorBase ServiceLocator { get { return serviceLocator ?? (serviceLocator = DomainFacadeServiceLocatorResolver.Resolve()); } }

        public static DataServiceBase ProvideDataService()
        {            
            var behavior = ConfigurationManager.ConfigurationProvider.BehaviorDataService;
            if (string.IsNullOrEmpty(behavior) || string.Equals(DefaultDataServiceClassName, behavior, StringComparison.OrdinalIgnoreCase))
                return ServiceLocator.CreateDataServiceNamed(DefaultDataServiceClassName);
            else
                return ServiceLocator.CreateDataServiceNamed(behavior);
        }
    }
}

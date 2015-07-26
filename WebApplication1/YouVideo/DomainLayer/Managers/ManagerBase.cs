using YouVideo.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DomainLayer.ServiceLocator;
using YouVideo.Providers;

namespace YouVideo.DomainLayer.Managers
{
    internal class ManagerBase
    {
        #region Protected Properties

        private DomainFacadeServiceLocatorBase serviceLocator;
        protected DomainFacadeServiceLocatorBase ServiceLocator { get { return serviceLocator ?? (serviceLocator = MakeServiceLocator()); } }

        private DataServiceBase dataService;
        protected DataServiceBase DataService { get { return dataService ?? (dataService = MakeDataService()); } }

        #endregion Protected Properties

        protected virtual DomainFacadeServiceLocatorBase MakeServiceLocator()
        {
            return DomainFacadeServiceLocatorResolver.Resolve();
        }

        protected virtual DataServiceBase MakeDataService()
        {
            return DataServiceProvider.ProvideDataService();
        }
    }
}

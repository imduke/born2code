using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DataLayer;
using YouVideo.DomainLayer.Managers;
using YouVideo.DomainLayer.ServiceLocator;
using YouVideo.DomainLayer.Services;

namespace YouVideo.DomainLayer
{
    public sealed partial class DomainFacade
    {
        #region Protected Properties

        private DomainFacadeServiceLocatorBase serviceLocator;
        private DomainFacadeServiceLocatorBase ServiceLocator { get { return serviceLocator ?? (serviceLocator = DomainFacadeServiceLocatorResolver.Resolve()); } }

        private MemberManagerBase memberManager;
        private MemberManagerBase MemberManager { get { return memberManager ?? (memberManager = ServiceLocator.CreateMemberManager()); } }

        private VideoManagerBase videoManager;
        private VideoManagerBase VideoManager { get { return videoManager ?? (videoManager = ServiceLocator.CreateVideoManager()); } }

        #endregion Protected Properties
    }
}

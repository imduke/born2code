using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YouVideo.DomainLayer.Exceptions;

namespace YouVideo.DomainLayer.ServiceLocator
{
    /// <summary>
    /// This class is reponsible for Resolving and thus providing the system-wide "ServiceLocator"
    /// class. By default this class will return the "Production" version of the ServiceLocator
    /// </summary>
    internal static class DomainFacadeServiceLocatorResolver
    {
        private static DomainFacadeServiceLocatorBase domainFacadeServiceLocator;

        /// <summary>
        /// This Delegate is used During Testing and should not be used in production
        /// </summary>
        public static Func<DomainFacadeServiceLocatorBase> DomainFacadeServiceLocatorFactory;

        /// <summary>
        /// This method Resolves and returns in instance of a <see cref="DomainFacadeServiceLocatorBase"/>.
        /// Without any configuration, this method will return an instance of <see cref="DomainFacadeServiceLocatorProduction"/>. This is the default behavior.
        /// <para>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>If the <see cref="DomainFacadeServiceLocatorFactory"/> public static member is assigned, it will call this delegate</description>
        ///         </item>
        ///         <item>
        ///             <description>Otherwise it will resolve besed on the config file, appSettings key: domainFacadeServiceLocatorClass</description>
        ///         </item>
        ///     </list>
        /// </para>
        /// </summary>
        /// <returns>An instance of a <see cref="DomainFacadeServiceLocatorBase"/></returns>
        public static DomainFacadeServiceLocatorBase Resolve()
        {
            if (domainFacadeServiceLocator == null)
            {
                if (DomainFacadeServiceLocatorFactory != null)
                    domainFacadeServiceLocator = DomainFacadeServiceLocatorFactory();
                else
                    domainFacadeServiceLocator = ResolveFromConfigFile();
            }
            return domainFacadeServiceLocator;
        }

        private static DomainFacadeServiceLocatorBase ResolveFromConfigFile()
        {
            var domainFacadeServiceLocatorClass = GetDomainFacadeServiceLocatorClass();

            if (DomainFacadeServiceLocatorClassIsProductionVersion(domainFacadeServiceLocatorClass))
                return new DomainFacadeServiceLocatorProduction();
            else
            {
                var commaPos = domainFacadeServiceLocatorClass.IndexOf(',');
                string domainFacadeServiceLocatorAssemblyName = null;

                if (commaPos != -1)
                {
                    domainFacadeServiceLocatorAssemblyName = domainFacadeServiceLocatorClass.Substring(0, commaPos);
                    domainFacadeServiceLocatorClass = domainFacadeServiceLocatorClass.Substring(commaPos + 1);
                }

                var executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
                var files = Directory.EnumerateFiles(executingDirectory, domainFacadeServiceLocatorAssemblyName + ".*").Where(fname => Path.GetExtension(fname) == ".exe" || Path.GetExtension(fname) == ".dll").ToArray();
                if (files.Length == 0)
                    throw new ServiceLocatorConfigurationInvalidException("The Assembly specified in domainFacadeServiceLocatorClass appConfig setting in the configuration file was not be found");

                var fullAssemblyName = files.First();
                var assembly = Assembly.LoadFile(fullAssemblyName);
                var serviceLocatorType = assembly.GetType(domainFacadeServiceLocatorClass);
                if (serviceLocatorType == null)
                    throw new ServiceLocatorConfigurationInvalidException(string.Format("The domainFacadeServiceLocatorClass's type name: \"{0}\", found in configuration file's appSetting section was not found in the assembly specified", domainFacadeServiceLocatorClass));

                return Activator.CreateInstance(serviceLocatorType) as DomainFacadeServiceLocatorBase;
            }
        }

        private static string GetDomainFacadeServiceLocatorClass()
        {
            return ConfigurationManager.AppSettings["domainFacadeServiceLocatorClass"];
        }

        private static bool DomainFacadeServiceLocatorClassIsProductionVersion(string domainFacadeServiceLocatorClass)
        {
            return (string.IsNullOrEmpty(domainFacadeServiceLocatorClass) || domainFacadeServiceLocatorClass.Equals("DomainFacadeServiceLocatorProduction", StringComparison.OrdinalIgnoreCase));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Asp.net_MVC_Debuger.Core
{
    public class ReflelctionControllerFactory : IControllerFactory
    {
        //其他成员
        private static List<Type> controllerTypes;
        static ReflelctionControllerFactory()
        {
            controllerTypes = new List<Type>();
            foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
            {
                controllerTypes.AddRange(assembly.GetTypes().Where(type => typeof(IController).IsAssignableFrom(type)));
            }
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type controllerType = this.GetControllerType(requestContext.RouteData, controllerName);
            if (null == controllerType)
            {
                throw new HttpException(404, "No controller found");
            }
            return (IController)Activator.CreateInstance(controllerType);
        }

        private static bool IsNamespaceMatch(string requestedNamespace, string targetNamespace)
        {
            if (!requestedNamespace.EndsWith(".*", StringComparison.OrdinalIgnoreCase))
            {
                return string.Equals(requestedNamespace, targetNamespace, StringComparison.OrdinalIgnoreCase);
            }
            requestedNamespace = requestedNamespace.Substring(0, requestedNamespace.Length - ".*".Length);
            if (!targetNamespace.StartsWith(requestedNamespace, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return ((requestedNamespace.Length == targetNamespace.Length) || (targetNamespace[requestedNamespace.Length] == '.'));
        }

        private Type GetControllerType(IEnumerable<string> namespaces, Type[] controllerTypes)
        {
            var types = (from type in controllerTypes
                         where namespaces.Any(ns => IsNamespaceMatch(ns, type.Namespace))
                         select type).ToArray();
            switch (types.Length)
            {
                case 0: return null;
                case 1: return types[0];
                default: throw new InvalidOperationException("Multiple types were found that match the requested controller name.");
            }
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            Type controllerType = this.GetControllerType(requestContext.RouteData, controllerName);
            if (null == controllerType)
            {
                return SessionStateBehavior.Default;
            }
            SessionStateAttribute attribute = controllerType.GetCustomAttributes(true).OfType<SessionStateAttribute>().FirstOrDefault();
            attribute = attribute ?? new SessionStateAttribute(SessionStateBehavior.Default);
            return attribute.Behavior;
        }

        public void ReleaseController(IController controller)
        {

        }

        protected virtual Type GetControllerType(RouteData routeData, string controllerName)
        {
            var types = controllerTypes.Where(type => string.Compare(controllerName + "Controller", type.Name, true) == 0).ToArray();
            if (types.Length == 0)
            {
                return null;
            }

            var namespaces = routeData.DataTokens["Namespaces"] as IEnumerable<string>;
            namespaces = namespaces ?? new string[0];
            Type contrllerType = this.GetControllerType(namespaces, types);
            if (null != contrllerType)
            {
                return contrllerType;
            }

            bool useNamespaceFallback = true;
            if (null != routeData.DataTokens["UseNamespaceFallback"])
            {
                useNamespaceFallback = (bool)(routeData.DataTokens["UseNamespaceFallback"]);
            }

            if (!useNamespaceFallback)
            {
                return null;
            }

            contrllerType = this.GetControllerType(ControllerBuilder.Current.DefaultNamespaces, types);
            if (null != contrllerType)
            {
                return contrllerType;
            }

            if (types.Length == 1)
            {
                return types[0];
            }

            throw new InvalidOperationException("Multiple types were found that match the requested controller name.");
        }
    }
}
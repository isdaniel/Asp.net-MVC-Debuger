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
    public interface IModelBinder1
    {
        object BindModel(ControllerContext controllerContext, string modelName, Type modelType);
    }
     public class DefaultModelBinder1 : IModelBinder1
     {
         public object BindModel(ControllerContext controllerContext, string modelName, Type modelType)
         {
             if (modelType.IsValueType || typeof(string) == modelType)
             {
                 object instance;
                 if (GetValueTypeInstance(controllerContext, modelName, modelType, out instance))
                {
                     return instance;
                 };
                 return Activator.CreateInstance(modelType);
             }
             object modelInstance = Activator.CreateInstance(modelType);
             foreach (PropertyInfo property in modelType.GetProperties())
             {
                 if (!property.CanWrite || (!property.PropertyType.IsValueType && property.PropertyType!= typeof(string)))
                 {
                     continue;
                 }
                 object propertyValue;
                 if (GetValueTypeInstance(controllerContext, property.Name, property.PropertyType, out propertyValue))
                 {
                     property.SetValue(modelInstance, propertyValue, null);
                 }
             }
             return modelInstance;
         }
         private  bool GetValueTypeInstance(ControllerContext controllerContext, string modelName, Type modelType, out object value)
         {
             var form = HttpContext.Current.Request.Form;
             string key;
             if (null != form)
             {
                 key = form.AllKeys.FirstOrDefault(k => string.Compare(k, modelName, true) == 0);
                 if (key != null)
                 {
                     value =  Convert.ChangeType(form[key], modelType);
                     return true;
                 }
             }
      
             key = controllerContext.RequestContext.RouteData.Values
                 .Where(item => string.Compare(item.Key, modelName, true) == 0)
                 .Select(item => item.Key).FirstOrDefault();
             if (null != key)
             {
                 value = Convert.ChangeType(controllerContext.RequestContext.RouteData.Values[key], modelType);
                 return true;
             }
      
             key = controllerContext.RequestContext.RouteData.DataTokens
                 .Where(item => string.Compare(item.Key, modelName, true) == 0)
                 .Select(item => item.Key).FirstOrDefault();
             if (null != key)
             {
                 value = Convert.ChangeType(controllerContext.RequestContext.RouteData.DataTokens[key], modelType);
                 return true;
             }
             value = null;
             return false;
         }
     }
    public class ReflectionControllerFactory : IControllerFactory
    {
        private static readonly IEnumerable<Type> controllerTypes;

        static ReflectionControllerFactory()
        {
            controllerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(IController).IsAssignableFrom(type));
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
            var types = controllerTypes.Where(type => string.Compare(controllerName, type.Name, StringComparison.OrdinalIgnoreCase) == 0).ToArray();

            if (types.Length == 1)
            {
                return types[0];
            }

            return null;
        }
    }
}
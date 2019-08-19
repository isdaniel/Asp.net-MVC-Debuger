using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.net_MVC_Debuger.Models;
using Newtonsoft.Json;

namespace Asp.net_MVC_Debuger.Controllers
{
    public class ResultModel
    {
        public string Content { get; set; }
    }

    public class ModelBindingController : Controller
    {

        public ActionResult ShowModelBinder(ModelBindDemo modelBindDemo)
        {
            IModelBinder binding = ModelBinders.Binders.GetBinder(typeof(ModelBindDemo));

            ResultModel result = new ResultModel
            {
                Content = $"modelBindDemo:{binding.GetType().Name}"
            };

            return View("Content",result);
        }
    }

    public class ModelBindDemo
    {
        public string Name{ get; set; }
    }

    public class FoolModelBinding : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new FooModelBinder();
        }
    }

    public class FooModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
           return null;
        }
    }
}
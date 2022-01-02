using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NEAproject.Services
{
    public class ViewRenderService : IViewRenderService
    {
        //inheritance via interface
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IHttpContextAccessor _accessor;
        public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IHttpContextAccessor accessor)
        {
            //parameterised constructor with dependency injection
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _accessor = accessor;

        }
        //implementing the method for interface iviewrenderservice
        public async Task<string> RenderToStringAsync(string Viewname, object model)
            //finds view exists or not and converts razor page into string and returns the data and  the string value
        {
            var actioncontext = new ActionContext(_accessor.HttpContext, _accessor.HttpContext.GetRouteData(), new ActionDescriptor());
            await using var sw = new StringWriter();
            var ViewResult = findview(actioncontext, Viewname);
            if(ViewResult == null)
            {
                throw new ArgumentNullException(Viewname + " doesn't match any available view");
            }
            var viewdictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            var viewcontext = new ViewContext(actioncontext, ViewResult, viewdictionary, new TempDataDictionary(actioncontext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions());
            await ViewResult.RenderAsync(viewcontext);
            return sw.ToString();
        }
        private IView findview(ActionContext actionContext, string viewname)
            //it tries to find a view razor page in multiiple locations according to priority
        {
            var getviewresult = _razorViewEngine.GetView(executingFilePath: null, viewPath: viewname, isMainPage: true);
            if (getviewresult.Success)
            {
                return getviewresult.View;
            }
            var findviewresult = _razorViewEngine.FindView(actionContext, viewname, isMainPage: true);
            if (findviewresult.Success)
            {
                return findviewresult.View;
            }
            var searchedlocations = getviewresult.SearchedLocations.Concat(findviewresult.SearchedLocations);
            var errormessage = string.Join(Environment.NewLine, new[] { $"unable to find the view {viewname}, following locations are searched: " }.Concat(searchedlocations));
            throw new InvalidOperationException(errormessage);
        }

    }
    //public class ViewRenderService:IViewRenderService
    //{
    //    private readonly IRazorViewEngine _razorViewEngine;
    //    private readonly ITempDataProvider _tempDataProvider;
    //    private readonly IServiceProvider _serviceProvider;
    //    //dependency injection - dont need a new object we inject an existing one.
    //    public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
    //    {
    //        _razorViewEngine = razorViewEngine;
    //        _tempDataProvider = tempDataProvider;
    //        _serviceProvider = serviceProvider;

    //    }
    //    public async Task<string> RenderToStringAsync(string Viewname, object model)
    //    {
    //        var httpcontext = new DefaultHttpContext { RequestServices = _serviceProvider };
    //        //hhtpcontext means the context of the web page. it will hold all information aboout it
    //        var actioncontext = new ActionContext(httpcontext, new RouteData(), new ActionDescriptor());
    //        //action means the actual page name. Home becomes controller context and about, comparison and index become action context.
    //        using(var stringwriter = new StringWriter())
    //        {
    //            var viewresult = _razorViewEngine.FindView(actioncontext, Viewname, false);
    //            if(viewresult.View == null)
    //            {
    //                throw new ArgumentNullException(Viewname + " that viewname is not available");
    //            }
    //            var viewdictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
    //            {
    //                Model = model
    //            };
    //            var viewcontext = new ViewContext(actioncontext, viewresult.View, viewdictionary, new TempDataDictionary(actioncontext.HttpContext, _tempDataProvider), stringwriter, new HtmlHelperOptions());
    //            await viewresult.View.RenderAsync(viewcontext);
    //            //await viewresult.View.RenderAsync(viewcontext).result;
    //            return stringwriter.ToString();
    //            //failing at line 48
    //        }
    //    }
    //}
}

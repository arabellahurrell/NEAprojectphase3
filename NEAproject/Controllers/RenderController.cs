using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEAproject.Services;

namespace NEAproject.Controllers
{
    [Route("render")]
    //defines the routing for the page 
    public class RenderController : Controller
    {
        private readonly IViewRenderService _renderService;
        public RenderController(IViewRenderService renderService)
            //parameterised constructor with a dependency injection with object scoped 
        {
            _renderService = renderService;
        }
        [Route("index")]
        //defining routing 
        public async Task<IActionResult> Index()
            //http method for index page 
        {
            //it converts the view page into string 
            var result =  await _renderService.RenderToStringAsync("Home/Index", null);
           
            return Content(result);
        }
    }
}

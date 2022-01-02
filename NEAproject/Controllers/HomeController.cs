using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NEAproject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NEAproject.Services;
using Microsoft.AspNetCore.Identity;
using NEAproject.Data;

namespace NEAproject.Controllers
{
    public class HomeController : Controller
    {
        private NEAdbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _enviroment;
        private readonly IViewRenderService _renderService;

        //parameterised constructor with dependency injection
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment enviroment, IViewRenderService renderService, UserManager<IdentityUser> userManager, NEAdbContext context)
        {
            _logger = logger;
            _enviroment = enviroment;
            _renderService = renderService;
            _userManager = userManager;
            _context = context;


        }

        //public IActionResult Index(bool? loginsuccess=false)
        //{

        //    //fileoperation.converttopdf(_enviroment.WebRootPath + "\\test.pdf");
        //    var output = (Helper.insertionsort(new int[] { 1, 8, 8, 4, 3, 6, 5, 7, 2 }));
        //    List<datapoint> datapointlist = new List<datapoint>();
        //    datapointlist.Add(new datapoint("0", 0));
        //    datapointlist.Add(new datapoint("1", 1));
        //    datapointlist.Add(new datapoint("2", 2));
        //    datapointlist.Add(new datapoint("3", 3));
        //    datapointlist.Add(new datapoint("4", 4));
        //    datapointlist.Add(new datapoint("5", 5));
        //    datapointlist.Add(new datapoint("6", 6));
        //    datapointlist.Add(new datapoint("7", 7));
        //    datapointlist.Add(new datapoint("8", 8));
        //    datapointlist.Add(new datapoint("9", 9));
        //    datapointlist.Add(new datapoint("10", 10));
        //    datapointlist.Add(new datapoint("11", 11));
        //    datapointlist.Add(new datapoint("12", 12));
        //    datapointlist.Add(new datapoint("13", 13));
        //    datapointlist.Add(new datapoint("14", 14));
        //    datapointlist.Add(new datapoint("15", 15));

            

        //    ViewBag.datapointlist = JsonConvert.SerializeObject(datapointlist);
        //    var model = new homemodel();
        //    if (loginsuccess == true)
        //    {
        //        ViewData["Loginsuccessful"] = true;
        //    }
            
        //    return View(model);
        //}
        //[HttpPost]
        public ActionResult Index(homemodel homemodel, bool? loginsuccess = false, string Save = null)
            //ActionResult adds another task to render the page on the browser in this instance. bridge between CSS/HTML page and browser
            //building up tags and json data for in viewbag to populate datapoints in the page
        {
            //to the code
            if (homemodel.selectcomplexity == "getLineardatapoints")
                //binds the json data into viewbag for linear complexity 
            {
                ViewBag.datapointlist = JsonConvert.SerializeObject(Helper.getLineardatapoints(homemodel.valueofn));
            }
            else if (homemodel.selectcomplexity == "getNtothe2points")
            //binds the json data into viewbag for polynomial complexity 
            {
                ViewBag.datapointlist = JsonConvert.SerializeObject(Helper.getNtothe2points(homemodel.valueofn));
            }
            else if (homemodel.selectcomplexity == "get2totheNpoints")
            //binds the json data into viewbag for exponential complexity 
            {
                ViewBag.datapointlist = JsonConvert.SerializeObject(Helper.get2totheNpoints(homemodel.valueofn));
            }
            else if (homemodel.selectcomplexity == "getlogn")
            //binds the json data into viewbag for log complexity 
            {
                ViewBag.datapointlist = JsonConvert.SerializeObject(Helper.getlogn(homemodel.valueofn));
            }
            else if (homemodel.selectcomplexity == "getnlogn")
            //binds the json data into viewbag for nlogn complexity 
            {
                ViewBag.datapointlist = JsonConvert.SerializeObject(Helper.getnlogn(homemodel.valueofn));
            }
            else
            {
                //otherwise it will bind this data
                List<datapoint> datapointlist = new List<datapoint>();
                datapointlist.Add(new datapoint("0", 0));
                datapointlist.Add(new datapoint("1", 1));
                datapointlist.Add(new datapoint("2", 2));
                datapointlist.Add(new datapoint("3", 3));
                datapointlist.Add(new datapoint("4", 4));
                datapointlist.Add(new datapoint("5", 5));
                datapointlist.Add(new datapoint("6", 6));
                datapointlist.Add(new datapoint("7", 7));
                datapointlist.Add(new datapoint("8", 8));
                datapointlist.Add(new datapoint("9", 9));
                datapointlist.Add(new datapoint("10", 10));
                datapointlist.Add(new datapoint("11", 11));
                datapointlist.Add(new datapoint("12", 12));
                datapointlist.Add(new datapoint("13", 13));
                datapointlist.Add(new datapoint("14", 14));
                datapointlist.Add(new datapoint("15", 15));



                ViewBag.datapointlist = JsonConvert.SerializeObject(datapointlist);
            }
            //viewdata for showing the successful login message on the page
            if (loginsuccess == true)
            {
                ViewData["Loginsuccessful"] = true;
            }
            if (!string.IsNullOrEmpty(Save))
            {
                //code for storing the history in the database. if searchhistory already exists for user fill up view data to show error message 
                var existingrecord = _context.Searchhistory.Where(x => x.Algorithmname == homemodel.selectcomplexity && x.Valueofn == homemodel.valueofn && x.Pagename.Contains("Index")).FirstOrDefault();
                if (existingrecord != null)
                {

                    ViewData["historyalreadyexist"] = "This value and complexity has already been saved";
                }
                //binds the error message in the view data for history already exists
                else
                {
                    //create a new object of search history

                    Searchhistory addnew = new Searchhistory();

                    var user = _userManager.GetUserAsync(User);
                    addnew.Userid = user.Result?.Id;
                    //aaplying question mark means that if user.result is null then it will just access an empty string otherwise it will asign actual id
                    addnew.Valueofn = homemodel.valueofn;
                    addnew.Algorithmname = homemodel.selectcomplexity;
                    addnew.Timecreated = DateTime.Now;
                    addnew.Pagename = "Index";
                    _context.Searchhistory.Add(addnew);
                    _context.SaveChanges();
                    //history into the database and save


                    return RedirectToAction("viewsearchhistory", "Home");
                }
            }
            return View(homemodel);
        }

        public async Task<ActionResult> RenderGraph(string selectcomplexity, int valueofn)
        {
            homemodel model = new homemodel();
            model.selectcomplexity = selectcomplexity;
            model.valueofn = valueofn;

            var result = await _renderService.RenderToStringAsync("Home/Index", model);
            //calls the custom middleware to convert razor page into string 
            fileoperation.converttopdf2(DateTime.Now.ToString("ddMMMyyyyhhmmss") + ".pdf", _enviroment.WebRootPath, User.Identity.Name, result);
            //date before time normally you can put time first tho
            //day can be integer as well as 3 character from day name such as sunday = sun dd return day as integer in 2 digits but ddd returns 3 characters from name of day as a string dddd full name 
            //also could only use 2 for year tho not as accurate because it can be wrong but current year it would be fine
            //when we apply only 2 time MM it will return month as integer with 2 digits when we apply MMM 3 times month value will be returned in month name but 3 charchter for exaple march is mar
            //if storing in database then more memory will be occupied which is why we use MMM instead of MMMM and dd instead of dddd
            //however for displaying purposes it is nice to have full names 
            return RedirectToAction("Index","Home");
            return Content(result);
        }
        [Authorize]
        //public IActionResult Comparison()
        //{
        //    ViewBag.Message = "Your comparison page.";
        //    return View(new homemodel());
        //}
        //[HttpPost]
        //attribute specifies that this action result is specifically just for post when this form is being submitted
        public ActionResult Comparison(homemodel homemodel, string Save = null)
        {
            //to the code
            ViewBag.Message = "Your comparison page.";
            if (homemodel != null)
                //fill up json data in viewbag to display on screen for comparison
            {

                ViewBag.datapointlist = JsonConvert.SerializeObject(Helper.getLineardatapoints(homemodel.valueofn));

                ViewBag.datapointlist1 = JsonConvert.SerializeObject(Helper.getNtothe2points(homemodel.valueofn));

                ViewBag.datapointlist2 = JsonConvert.SerializeObject(Helper.get2totheNpoints(homemodel.valueofn));

                ViewBag.datapointlist3 = JsonConvert.SerializeObject(Helper.getlogn(homemodel.valueofn));

                ViewBag.datapointlist4 = JsonConvert.SerializeObject(Helper.getnlogn(homemodel.valueofn));
            }

            if (!string.IsNullOrEmpty(Save))
            {
                //storing search history if it already exists show error message that it already exists
                //binds the error message into viewdata for history already exists 
                var existingrecord = _context.Searchhistory.Where(x => x.Valueofn == homemodel.valueofn && x.Pagename.Contains("Comparison")).FirstOrDefault();
                if (existingrecord != null)
                {

                    ViewData["historyalreadyexist"] = "This value and complexity has already been saved";
                }
                else
                {

                    Searchhistory addnew = new Searchhistory();
                    //creates a new object fro search history

                    var user = _userManager.GetUserAsync(User);
                    addnew.Userid = user.Result?.Id;
                    //aaplying question mark means that if user.result is null then it will just access an empty string otherwise it will asign actual id
                    addnew.Valueofn = homemodel.valueofn;
                    addnew.Timecreated = DateTime.Now;
                    addnew.Pagename = "Comparison";
                    _context.Searchhistory.Add(addnew);
                    //adds some search history into the database for comparison and saves it 
                    _context.SaveChanges();


                    return RedirectToAction("viewsearchhistory", "Home");
                    //redirect to viewsearchistory page
                }
            }
            return View(homemodel);
        }
        //viewsearchhistory
        [Authorize]
        public IActionResult viewsearchhistory()
            //displays all search history for logged in user
        {
            var user = _userManager.GetUserAsync(User);
            string Userid = user.Result?.Id;
            List<Searchhistory> model = _context.Searchhistory.Where(x => x.Userid == Userid).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult viewsearchhistory(string searchid)
            //redirect to page to show a particular selected search or comparison
        {
            if (!string.IsNullOrEmpty(searchid))
            {
                int sid = Convert.ToInt32(searchid);
                var record = _context.Searchhistory.Where(x => x.Searchid == sid).FirstOrDefault();
                //finds the saved search history from the database 
                homemodel homemodel = new homemodel();
                homemodel.valueofn = record.Valueofn.Value;
                if(record.Pagename.Contains( "Index"))
                {
                    homemodel.selectcomplexity = record.Algorithmname;
                    return RedirectToAction("Index", "Home", homemodel);
                }
                else
                {
                    return RedirectToAction("Comparison", "Home", homemodel);
                }
                

            }
            var user = _userManager.GetUserAsync(User);
            string Userid = user.Result?.Id;
            List<Searchhistory> model = _context.Searchhistory.Where(x => x.Userid == Userid).ToList();
            return View(model);
        }
        public async Task<ActionResult> RenderComparison(int valueofn)
        {
            homemodel model = new homemodel();
            //model.selectcomplexity = "getLineardatapoints";
            model.valueofn = valueofn;

            var result = await _renderService.RenderToStringAsync($"/Views/Home/Comparison.cshtml", model);
            //calls the bespoke middleware to convert razor page into string 
            fileoperation.converttopdf2(DateTime.Now.ToString("ddMMMyyyyhhmmss") + ".pdf", _enviroment.WebRootPath, User.Identity.Name, result);
            //date before time normally you can put time first tho
            //day can be integer as well as 3 character from day name such as sunday = sun dd return day as integer in 2 digits but ddd returns 3 characters from name of day as a string dddd full name 
            //also could only use 2 for year tho not as accurate because it can be wrong but current year it would be fine
            //when we apply only 2 time MM it will return month as integer with 2 digits when we apply MMM 3 times month value will be returned in month name but 3 charchter for exaple march is mar
            //if storing in database then more memory will be occupied which is why we use MMM instead of MMMM and dd instead of dddd
            //however for displaying purposes it is nice to have full names 
            return RedirectToAction("Comparison", "Home", model);
            return Content(result);
        }


        public IActionResult About()
        {
            //http method fro about page
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //http error method in case of any exception thrown. exception isn't cached 
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}

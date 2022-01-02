using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEAproject.Controllers
{
    public class BaseController : Controller
    {
        public string username { get { return User.Identity.Name; } }
    }
}
// shared controller where we register or declare or define common properties or functionalities that we want to use. best practice.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NEAproject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class UserConfirmationModel : PageModel
    {
        public void OnGet()
        {
            //http get method 
        }
    }
}

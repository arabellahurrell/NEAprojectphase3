using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace NEAproject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
            //parameterised ocnstructor with dependency injection
        {
            _userManager = userManager;
        }

        [BindProperty]
        //binds the input view model to the page
        public InputModel Input { get; set; }

        public class InputModel
            //view model class
        {
            [Required]
            [EmailAddress]
            //data annotations for validation 
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            //data annotations fro validation for max string length 
            [DataType(DataType.Password)]
            //display dots instead of plaintext for privacy purpose
            public string Password { get; set; }

            [DataType(DataType.Password)]
            //display dots instead of plaintext 
            [Display(Name = "Confirm password")]
            //used for label 
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //data annotation for comparing password and confirm password fields 
            public string ConfirmPassword { get; set; }
            

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
            //http get method 
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
                //if reset password code is null or invalid then this message will be displayed and exception will be thrown 
            }
            else
            {
                Input = new InputModel
                {
                    //creates a new view model with correct reset password code 
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
            //http post method 
        {
            if (!ModelState.IsValid)
                //check for whether the form submitted by user is valid or not 
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            //tries the user from the database if exist 
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");

            }
            //if user exists then it will reset the user password in the database 

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
                //fill up all the error messages in the model state
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace NEAproject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            //parametrised constrcutor with dependency injection 
            //which is where we will not create new objects but we will inject the object that will be shared around the application based on how we registered them in the startup.cs
            //it has 3 different scope of sharing the object. singleton, transient and scoped
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        //bind the page with the class. so when submit button is pressed all values will be bound to the page
        public InputModel Input { get; set; }
        //view model 
        public class InputModel
        {

            [Required]
            [EmailAddress]
            //data annotation for validation 
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
            //http post action
        {
            if (ModelState.IsValid)
                //check whether the form which was submitted has the correct value or whether it has a validation error
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                //checking user exists in database
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //create a code for reset password.
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);
                //send email of reset password with the link 
                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}

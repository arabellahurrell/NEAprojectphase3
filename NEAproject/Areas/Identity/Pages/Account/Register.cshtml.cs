using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NEAproject.Models;

namespace NEAproject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        //dependency injection - solid principle 
        public RegisterModel(
          
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        //bind input viewmodel to the page
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        //view model for page with data annotations for validation 
        public class InputModel
            //view model to the page (class)
        {
            [Required]
            [EmailAddress]
            //data annotations for validation 
            [Display(Name = "Email")]
            //data used for label. data annotation 
            public string Email { get; set; }

            [Required]
            //data annotation for validation 
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            //instead of showing plaintext will show dots for privacy purpose 
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            //shows dots instead of plaintext fro privacy reasons.
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //data annotation fro validation of comparing password and compare password fields.
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
            //http get method 
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            bool emailsent = EmailSender.sendtestemail("neha.padia1220@gmail.com", "test email", "test email");
            //remove before submit
            if (emailsent == false)
            {
                //failed
            }
            else
            {
                //success
            }
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
            //http post method. after submit button it will come through here
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            bool validemail = isValidemail(Input.Email);
            //check doman exists or not by using SMTP mail address
            if (validemail == false)
            {
                ModelState.AddModelError("", "This email address is not correct");
                //binds the error message with the model 
            }
            if (ModelState.IsValid)
                //checks if the model has any error for validation 
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                //create new object for the user 
                var result = await _userManager.CreateAsync(user, Input.Password);
                //will create new user
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //sends confirmation email and will redirect user to user confirmation page if user email address is not an actual valid one
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    string message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                    string subject = "confirm your email";
                    await _emailSender.SendEmailAsync(Input.Email, subject, message
                        );
                    bool emailsent = EmailSender.sendtestemail(Input.Email, message, subject);
                    if (emailsent == false)
                    {
                        // "its not a real email";
                        return RedirectToPage("UserConfirmation");
                    }
                    else
                    {

                    }
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                    //fills up all errors in summary 
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


        private bool isValidemail(string emailaddress)
            //checks that the email address is valid mail address object
        {
            try
                //for exception handling stops application from crashing by capturing solution 
            {
                var address = new System.Net.Mail.MailAddress(emailaddress);
                return address.Address == emailaddress;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}

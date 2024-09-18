using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using My_MVC_App.Helper;
using My_MVC_App.ViewModels;
using System.Threading.Tasks;

namespace My_MVC_App.Controllers
{
    public class AccountController : Controller
    {
		public UserManager<ApplicationUser> _userManager { get; }
		public SignInManager<ApplicationUser> _signInManager { get; }
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region Sign Up
        [HttpGet]
		public IActionResult SignUp() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {


            if (ModelState.IsValid) //server side validation
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if(user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        //Manual Mapping
						user = new ApplicationUser
						{
							UserName = model.UserName,
							Email = model.Email,
							FirstName = model.FirstName,
							LastName = model.LastName,
							isAgree = model.IsAgree

						};

						var result = await _userManager.CreateAsync(user, model.Password);
                        if(result.Succeeded)
                        {
                            return RedirectToAction(nameof(SignIn));
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
					}
                }

                ModelState.AddModelError(string.Empty, "User already exists");

            }
            return View(model);
        }
        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag is true)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if(result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login");
            }

			return View(model);
		}


        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    //generate token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					//create URL which is used in the body of the email 
					var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

                    //create email
                    var email = new Email()
                    {
                        Subject="Reset your password",
                        Recipient = model.Email,
                        Body = url
                    };

                    // Send Email
                    EmailSettings.SendEmail(email);


                    // redirect to action
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                ModelState.AddModelError(string.Empty, "Invalid Email");

            }
            return View(nameof(ForgetPassword));
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {

                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}

				}
                ModelState.AddModelError(string.Empty, "Invalid Reset Password");

            }
            
            return View();
		}


		#endregion
	}
}

using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IdyllicWeb.Areas.Admin.View_Models;
using IdyllicWeb.Areas.Admin.Models;
using AutoMapper;
using System.Collections.Generic;
using IdyllicWeb.Persistence;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using System.IO;
using IdyllicWeb.Infrastructure.Helpers.Account;
using System.Security.Cryptography;
using IdyllicWeb.Infrastructure.Constants;
using System.Text;
using IdyllicWeb.Infrastructure.Helpers.Email;

namespace IdyllicWeb.Areas.Auth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        public HomeController()
        {
        }

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}
        //public AccountController(ApplicationUserManager userManager,
        //    ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ApplicationRoleManager roleManager)
        //{
        //    UserManager = userManager;
        //    AccessTokenFormat = accessTokenFormat;
        //    RoleManager = roleManager;
        //}
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            //AccessTokenFormat = accessTokenFormat;
            RoleManager = roleManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel msm = new LoginViewModel();
            secConst.cSalt = GetSalt();
            msm.hdns = secConst.cSalt.ToString();
            return View(msm);
        }
        public string GetSalt()
        {
            RNGCryptoServiceProvider rndm = new RNGCryptoServiceProvider();
            return rndm.GetHashCode().ToString();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //if (!this.IsCaptchaValid("Validate your captcha"))
            //{
            //    ViewBag.ReturnUrl = HttpUtility.HtmlEncode(returnUrl);
            //    ViewBag.CaptchaErrorMessage = "Invalid Captcha";
            //    ModelState.AddModelError("", "Invalid verification code. Please try again.");
            //    return View(model);
            //}
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = await UserManager.FindAsync(model.UserName, model.Password);
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            if (user != null)
            {
                Session["UserID"] = user.Id;

                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    //UserActivityHelper.SaveUserActivity("Account has been locked out for {0} minutes due to multiple failed login attempts", Request.Url.ToString());
                    ModelState.AddModelError("", string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", 15)); //ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
                }

                await UserManager.UpdateSecurityStampAsync(user.Id);
                
                if (UserManager.IsInRole(user.Id, "Admin"))
                {
                    //UserActivityHelper.SaveUserActivity("Admin Login Successfull", Request.Url.ToString());
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                if (UserManager.IsInRole(user.Id, "User"))
                {
                    //UserActivityHelper.SaveUserActivity("Login Successfull", Request.Url.ToString());

                    if (DateTime.Now.Month == 11 || DateTime.Now.Month == 12 || DateTime.Now.Month == 1)
                    {
                        return RedirectToAction("Dashboard", "Home", new { Area = "Member" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { Area = "Member" });
                    }

                }
                else if (UserManager.IsInRole(user.Id, "Staff"))
                {
                    UserActivityHelper.SaveUserActivity("Staff Login Successfull", Request.Url.ToString());
                    return RedirectToAction("Index", "Home", new { Area = "Staff" });
                }
                else if (UserManager.IsInRole(user.Id, "Guest"))
                {
                    UserActivityHelper.SaveUserActivity("Guest Login Successfull", Request.Url.ToString());
                    return RedirectToAction("Index", "Home", new { Area = "guest" });
                }

                //if (String.IsNullOrEmpty(returnUrl))
                //{
                //    if (UserManager.IsInRole(user.Id, "Candidate"))
                //    {
                //        if (DateTime.Now.Month == 11 || DateTime.Now.Month == 12 || DateTime.Now.Month == 1)
                //        {
                //            return RedirectToAction("Dashboard", "Home", new { Area = "Member" });
                //        }
                //        else
                //        {
                //            return RedirectToAction("Index", "Home", new { Area = "Member" });
                //        }

                //    }
                //    else if (UserManager.IsInRole(user.Id, "Admin"))
                //    {
                //        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                //    }
                //    else if (UserManager.IsInRole(user.Id, "Staff"))
                //    {
                //        return RedirectToAction("Index", "Home", new { Area = "Staff" });
                //    }
                //}
                //else
                //{
                //    return RedirectToLocal(returnUrl);
                //}
            }
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(HttpUtility.HtmlEncode(returnUrl));
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("", "Failed!. Status returned [6185526]!");
                    return View(model);
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid Username or Password. Please try again.");
                    UserActivityHelper.SaveUserActivity("Login attempted with wrong credentials(by " + model.UserName + ")", Request.Url.ToString());
                    return View(model);

                    //ModelState.AddModelError("", "Invalid Username or Password. Please try again.");
                    //return View(model);
            }
            //return View(model);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        //[Authorize(Roles = CustomRoles.Admin)]
        public ActionResult Register()
        {
            //ViewBag.Roles = RoleManager.Roles.Select(r => new SelectListItem() { Value = "Id", Text = "Name" }).ToList();
            ViewBag.Roles = RoleManager.Roles.Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            ViewBag.Role = "User";
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = CustomRoles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FName = model.FName,
                    Email = model.Email
                };
                //string rolname = "User";

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    //await this.UserManager.AddToRoleAsync(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("login", "home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize(Roles = CustomRoles.Admin)]
        public ActionResult AppUsersList()
        {
            //var allusers = UserManager.Users.ToList();
            //var users = allusers.Where(x => x.Roles.Select(role => role.).Contains("User")).ToList();
            //var userVM = users.Select(user => new UserViewModel { Username = user.FullName, Roles = string.Join(",", user.Roles.Select(role => role.Name)) }).ToList();


            var users = UserManager.Users.ToList();
            //var u = users.SelectMany(x => x.Roles.Where(y => y.UserId == x.Id));

            //var usersWithRoles = (from user in UserManager.Users
            //                      select new
            //                      {
            //                          UserId = user.Id,
            //                          Username = user.UserName,
            //                          Email = user.Email,
            //                          RoleNames = (from userRole in user.Roles
            //                                       join role in context.Roles on userRole.RoleId
            //                                       equals role.Id
            //                                       select role.Name).ToList()
            //                      }).ToList().Select(p => new Users_in_Role_ViewModel()

            //                      {
            //                          UserId = p.UserId,
            //                          Username = p.Username,
            //                          Email = p.Email,
            //                          Role = string.Join(",", p.RoleNames)
            //                      });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IEnumerable<ApplicationUser>, List<AppUserViewModel>>();
            });
            IMapper mapper = config.CreateMapper();
            var indexDto = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<AppUserViewModel>>(users).ToList();
            return View(indexDto);
        }
        //
        // GET: /Account/ConfirmEmail
        [Authorize(Roles = CustomRoles.Admin)]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == default(int) || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == default(int))
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon();
            //return RedirectToAction("Login", "Account");
            return Redirect("/");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        private static Random RNG = new Random();
        public string Create16DigitString()
        {
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }
        #endregion


        #region Change Password
        public ActionResult ChangeUserPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            string userName = User.Identity.GetUserName();
            //string userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Login", "Account");
            }
            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var result = await UserManager.ResetPasswordAsync(user.Id, token, model.NewPassword);

            //var result = await UserManager.ChangePasswordAsync(user.Id, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }
        #endregion
    }
}
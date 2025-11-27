using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task13_v2.Models;
using Mapster;
using Task13_v2.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Task13_v2.Repositories.IRepositories;

namespace Task13_v2.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private IRepository<ApplicationOtp> _otpRepository;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IRepository<ApplicationOtp> otpRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _otpRepository = otpRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            //var appUser = register.Adapt<ApplicationUser>();
            var newUser = new ApplicationUser() {
                UserName = register.UserName,
                Email = register.Email,
                Name = register.Name,
                Address = register.Address
            };
            try
            {
            var stats = await _userManager.CreateAsync(newUser, register.Password);
            if(!stats.Succeeded)
            {
                foreach (var item in stats.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Code);
                }
                return View(register);
            }
            }
            catch(InvalidOperationException e)
            {
                ModelState.AddModelError("Email", "This Email is used before");
                return View(register);
            }
            ///////// Send Confirmation Email \\\\\\\\\\
            
            var confEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confLink = Url.Action("Confirm","Account",new {area = "Identity", confEmailToken, newUser.Id}, Request.Scheme);
            await _emailSender.SendEmailAsync(newUser.Email, "Email Confirmation", $"<h1> To Approve Your Email, Please Click <a href='{confLink}'> here</a></h1>");
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(string token, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if(!ModelState.IsValid)
                return View(login);
            var user = await _userManager.FindByEmailAsync(login.UserNameOrEmailOrPhoneNumber) ?? await _userManager.FindByNameAsync(login.UserNameOrEmailOrPhoneNumber);
            if(user == null)
            {
                ModelState.AddModelError("UserNameOrEmailOrPhone", "User not found");
                ModelState.AddModelError("Password", "Invalid Password");
                return View(login);
            }
            var stats = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe,true);

            if(!stats.Succeeded)
            {
                if(stats.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Too many trials... please try again later");
                    // Email
                }
                if(stats.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "Account is not verified");
                }
                return View(login);
            }
            return RedirectToAction(nameof(Register));
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPaswordVM forgotPaswordVM)
        {
            if (!ModelState.IsValid)
                return View(forgotPaswordVM);
            var user = await _userManager.FindByEmailAsync(forgotPaswordVM.EmailOrUsername) ?? await _userManager.FindByNameAsync(forgotPaswordVM.EmailOrUsername);

            if (user is null)
            {
                ModelState.AddModelError("EmailOrUsername", "Email Or Usename Does not exist");
                return View(forgotPaswordVM);
            }
            var otp = (new Random()).Next(1000, 9999);
            await _otpRepository.CreateAsync(new()
            {
                Otp = otp,
                UserId = user.Id,
                IsValid = true
            });
            await _otpRepository.CommitAsync();
            await _emailSender.SendEmailAsync(user.Email, "Reset - OTP", $"<h1>Your OTP for Password Reset is <u>{otp}</u><br>Please Do not share it</h1>");

            return RedirectToAction(nameof(VerifyOtp));
        }
        [HttpGet]
        public IActionResult VerifyOtp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyOtp(VerifyOtpVM verifyOtpVM)
        {
            if (!ModelState.IsValid)
                { return View(verifyOtpVM); }
            var otp = await _otpRepository.GetOneAsync(o => o.Otp == verifyOtpVM.Otp);
            if (otp == null)
            {
                ModelState.AddModelError("Otp", "invalid OTP");
                return View(verifyOtpVM);
            }
            return RedirectToAction(nameof(ResetPassword));
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}

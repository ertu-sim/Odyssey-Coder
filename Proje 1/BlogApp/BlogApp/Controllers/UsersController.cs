using System.Security.Claims;
using BlogApp.Data.Abstrack;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.Conrollers
{
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;


        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");  //kullanıcı giriş  yaptıysa login sayfasına giremez 
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isuser = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
                // var isuser=await _userRepository.Users.FirstOrDefaultAsync(x=> x.UserName == model.UserName && xx.password == model.password); gibi 

                if (isuser != null)
                {
                    var UserClaims = new List<Claim>();    //UserClaims kullanıcının özelliklerini barındırıyor ve uygulamaya taşınıyor
                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, isuser.UserId.ToString())); //idye karşılık gelen kısım 
                    UserClaims.Add(new Claim(ClaimTypes.Name, isuser.UserName ?? "")); //username karşılık gelen kısım burası
                    UserClaims.Add(new Claim(ClaimTypes.UserData, isuser.Image ?? "")); //username karşılık gelen kısım burası


                    if (isuser.UserName == "ertuğrul şimşek") //admin rolü için
                    {
                        UserClaims.Add(new Claim(ClaimTypes.Role, "admin")); //role göre
                    }

                    var claimsIdentity = new ClaimsIdentity(UserClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true  // beni hatırla özelliği
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Posts");

                }
                else
                {
                    ModelState.AddModelError("", "kullanıcı adı yanlış");
                }
            }


            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Posts");

        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var security = _userRepository.Users.Any(u => u.UserName == model.UserName);
                // var security = _userRepository.Users.FirstOrDefault(x=>x.UserName==model.UserName || x.email==model.email) sorgulaması da  yapılabilir

                if (security == false)
                {
                    // _userRepository.CreateUser(model); şeklinde tanımlama da yapılabilr 
                    _userRepository.CreateUser(new User
                    {
                        UserName = model.UserName,
                        Image = "man.jpg"

                    });
                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "kullanıcı adı zaten kullanılıyor");
                }
            }
            return View(model);
        }


    }
}
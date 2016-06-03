using Data.Tenants.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Models.Tenants;
using Site.Subscription.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using Site.Subscription.Infrastructure;

namespace Site.Subscription.Controllers
{
    public class AccountController : Controller
    {
        ITenantRepository repository = new TenantRepository();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Login(LoginViewModel model)
        {
            if ((!ModelState.IsValid) || (repository.FindByCredentials(model.Username, model.Password) != null))
            {
                ViewBag.ErrorMessage = "Verifique seu usuário e senha.";
                return View(model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.StayConnected);
                var userManager = HttpContext.GetOwinContext().GetUserManager<SubscriptionTenantManager>();
                var user = await userManager.FindByEmailAsync(model.Email);
                user.SecurityStamp = "AHSAUKPLCAUE";
                var sign = HttpContext.GetOwinContext().Get<SubscriptionSignInManager>();
                await sign.SignInAsync(user, true, model.StayConnected);

                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Tenant model)
        {
            if (ModelState.IsValid)
            {
                repository.Add(model);
            }
            else
            {
                ModelState.AddModelError("Error", "Dados inválidos.");
                return View(model);
            }

            return RedirectToAction("Login");
        }
    }
}
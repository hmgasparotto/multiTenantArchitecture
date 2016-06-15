using Data.Tenants.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Models.Tenants;
using Site.Subscription.Models;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Site.Subscription.Infrastructure;
using Common.Infrastructure.Cryptography;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Data.IoT.Repositories;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using Domain.Models.Things;

namespace Site.Subscription.Controllers
{
    public class AccountController : Controller
    {
        private IThingRepository<ContainerLevelMeasurer> _clmRepository;
        private IThingRepository<PublicIlluminationController> _picRepository;

        private SubscriptionSignInManager _signInManager;
        private SubscriptionTenantManager _tenantManager;

        public AccountController()
        {
        }

        public AccountController(SubscriptionTenantManager tenantManager, SubscriptionSignInManager signInManager)
        {
            TenantManager = tenantManager;
            SignInManager = signInManager;
        }

        public SubscriptionSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SubscriptionSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        
        public SubscriptionTenantManager TenantManager
        {
            get
            {
                return _tenantManager ?? HttpContext.GetOwinContext().GetUserManager<SubscriptionTenantManager>();
            }
            private set
            {
                _tenantManager = value;
            }
        }

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
        public async System.Threading.Tasks.Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Verifique seu usuário e senha.";
                return View(model);
            }

            var user = await TenantManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "E-mail incorreto.");
                return View(model);
            }
            var hasher = new SimplePasswordHasher();
            user = await TenantManager.FindAsync(user.UserName, hasher.HashPassword(model.Password));
            if (user == null)
            {
                ModelState.AddModelError("", "Senha incorreta.");
                return View(model);
            }
            await SignInManager.SignInAsync(user, true, model.StayConnected);
            return RedirectToLocal(returnUrl);
        }

        [AllowAnonymous]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateUser(TenantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hasher = new SimplePasswordHasher();
                var tenant = new Tenant() { UserName = model.Username, Email = model.Email, PasswordHash = hasher.HashPassword(model.Password) };
                var result = await TenantManager.CreateAsync(tenant, tenant.PasswordHash);
                if (result.Succeeded)
                {
                    // CRIA BASE DE DADOS
                    switch(model.Type)
                    {
                        case ThingType.ContainerLevelMeasurer:
                            _clmRepository = new ContainerLevelMeasurerRepository(tenant.Id.ToString(), new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString));
                            break;
                        case ThingType.PublicIlluminationController:
                            //_picRepository = new PublicIlluminationControllerRepository(tenant.Id.ToString(), new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString));
                            break;
                        default:
                            break;
                    }

                    await SignInManager.SignInAsync(tenant, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }
}
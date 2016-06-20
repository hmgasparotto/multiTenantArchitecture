using Domain.Interfaces.Repositories;
using Domain.Models.Tenants;
using Site.Subscription.Models;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Site.Subscription.Infrastructure;
using Common.Infrastructure.Cryptography;
using Microsoft.AspNet.Identity;
using Data.IoT.Repositories;
using System.Data.SqlClient;
using System.Configuration;
using Domain.Models.Things;

namespace Site.Subscription.Controllers
{
    public class AccountController : Controller
    {
        private IThingRepository<ContainerLevelMeasurer> _clmRepository;
        private IThingRepository<PublicIlluminationController> _picRepository;

        private SubscriptionTenantManager _tenantManager;

        public AccountController()
        {
        }

        public AccountController(SubscriptionTenantManager tenantManager)
        {
            TenantManager = tenantManager;
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
                var tenant = new Tenant() { UserName = model.Username, Email = model.Email, Type = model.Type.ToString() };
                var result = await TenantManager.CreateAsync(tenant, model.Password);
                if (result.Succeeded)
                {
                    switch(model.Type)
                    {
                        case ThingType.ContainerLevelMeasurer:
                            _clmRepository = ContainerLevelMeasurerRepository.Create(tenant.Id.ToString(), new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString));
                            break;
                        case ThingType.PublicIlluminationController:
                            _picRepository = PublicIlluminationControllerRepository.Create(tenant.Id.ToString(), new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString));
                            break;
                        default:
                            break;
                    }

                    return Redirect("http://tccapplication.azurewebsites.net/");
                }
                AddErrors(result);
            }

            return View(model);
        }

        #region Helpers
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
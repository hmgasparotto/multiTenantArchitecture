using Application.Services;
using Application.Services.Interfaces;
using Domain.Models.Tenants;
using Domain.Models.Things;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Site.Application.Infrastructure;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Site.Application.Controllers
{
    [Authorize]
    public class ThingController : Controller
    {
        private IContainerLevelMeasurerAppService _clmService;
        private IPublicIlluminationControllerAppService _picService;

        private ApplicationTenantManager _tenantManager;
        private Tenant _currentTenant;

        public ApplicationTenantManager TenantManager
        {
            get
            {
                return _tenantManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationTenantManager>();
            }
            private set
            {
                _tenantManager = value;
            }
        }

        // GET: Thing
        public ActionResult Index()
        {
            RepositoryGetter();
            if (_currentTenant.Type.Contains("ContainerLevel"))
            {
                return View("IndexContainerLevelMeasurer", _clmService.GetAll());
            }
            else if (_currentTenant.Type.Contains("PublicIllumination"))
            {
                return View("IndexPublicIlluminationController", _picService.GetAll());
            }
            return View();
        }

        #region ContainerLevelMeasurer
        public ActionResult CreateContainerLevelMeasurer()
        {
            RepositoryGetter();
            return View();
        }

        [HttpPost]
        public ActionResult CreateContainerLevelMeasurer(ContainerLevelMeasurer model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _clmService.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult EditContainerLevelMeasurer(int id)
        {
            RepositoryGetter();
            return View(_clmService.Find(id));
        }

        [HttpPost]
        public ActionResult EditContainerLevelMeasurer(ContainerLevelMeasurer model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _clmService.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DetailsContainerLevelMeasurer(int id)
        {
            RepositoryGetter();
            return View(_clmService.Find(id));
        }

        public ActionResult DeleteContainerLevelMeasurer(int id)
        {
            RepositoryGetter();
            return View(_clmService.Find(id));
        }

        [HttpPost]
        public ActionResult DeleteContainerLevelMeasurer(ContainerLevelMeasurer model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _clmService.Remove(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region PublicIlluminationController
        public ActionResult CreatePublicIlluminationController()
        {
            RepositoryGetter();
            return View();
        }

        [HttpPost]
        public ActionResult CreatePublicIlluminationController(PublicIlluminationController model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _picService.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult EditPublicIlluminationController(int id)
        {
            RepositoryGetter();
            return View(_picService.Find(id));
        }

        [HttpPost]
        public ActionResult EditPublicIlluminationController(PublicIlluminationController model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _picService.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DetailsPublicIlluminationController(int id)
        {
            RepositoryGetter();
            return View(_picService.Find(id));
        }

        public ActionResult DeletePublicIlluminationController(int id)
        {
            RepositoryGetter();
            return View(_picService.Find(id));
        }

        [HttpPost]
        public ActionResult DeletePublicIlluminationController(PublicIlluminationController model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _picService.Remove(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region Helpers
        public void RepositoryGetter()
        {
            if ((HttpContext != null) && (_clmService == null) & (_picService == null))
            {
                var tenant = Task.Run(() => TenantManager.FindByIdAsync(HttpContext.User.Identity.GetUserId()));
                _currentTenant = tenant.Result;
                if (_currentTenant.Type.Contains("ContainerLevel"))
                {
                    _clmService = ContainerLevelMeasurerAppService.Factory(_currentTenant.Id, new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString), false);
                }
                else if (_currentTenant.Type.Contains("PublicIllumination"))
                {
                    _picService = PublicIlluminationControllerAppService.Factory(_currentTenant.Id, new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString), false);
                }
            }
        }
        #endregion
    }
}
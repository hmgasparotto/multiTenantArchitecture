using Data.IoT.Repositories;
using Domain.Interfaces.Repositories;
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
        private IThingRepository<ContainerLevelMeasurer> _clmRepository;
        private IThingRepository<PublicIlluminationController> _picRepository;

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
                return View("IndexContainerLevelMeasurer", _clmRepository.GetAll());
            }
            else if (_currentTenant.Type.Contains("PublicIllumination"))
            {
                return View("IndexPublicIlluminationController", _picRepository.GetAll());
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
                _clmRepository.Add(model);
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
            return View(_clmRepository.Find(id));
        }

        [HttpPost]
        public ActionResult EditContainerLevelMeasurer(ContainerLevelMeasurer model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _clmRepository.Add(model);
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
            return View(_clmRepository.Find(id));
        }

        public ActionResult DeleteContainerLevelMeasurer(int id)
        {
            RepositoryGetter();
            return View(_clmRepository.Find(id));
        }

        [HttpPost]
        public ActionResult DeleteContainerLevelMeasurer(ContainerLevelMeasurer model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _clmRepository.Remove(model);
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
                _picRepository.Add(model);
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
            return View(_picRepository.Find(id));
        }

        [HttpPost]
        public ActionResult EditPublicIlluminationController(PublicIlluminationController model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _picRepository.Add(model);
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
            return View(_picRepository.Find(id));
        }

        public ActionResult DeletePublicIlluminationController(int id)
        {
            RepositoryGetter();
            return View(_picRepository.Find(id));
        }

        [HttpPost]
        public ActionResult DeletePublicIlluminationController(PublicIlluminationController model)
        {
            RepositoryGetter();
            if (ModelState.IsValid)
            {
                _picRepository.Remove(model);
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
            if ((HttpContext != null) && (_clmRepository == null) & (_picRepository == null))
            {
                var tenant = Task.Run(() => TenantManager.FindByIdAsync(HttpContext.User.Identity.GetUserId()));
                _currentTenant = tenant.Result;
                if (_currentTenant.Type.Contains("ContainerLevel"))
                {
                    _clmRepository = ContainerLevelMeasurerRepository.Get(_currentTenant.Id, new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString));
                }
                else if (_currentTenant.Type.Contains("PublicIllumination"))
                {
                    _picRepository = PublicIlluminationControllerRepository.Get(_currentTenant.Id, new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString));
                }
            }
        }
        #endregion
    }
}
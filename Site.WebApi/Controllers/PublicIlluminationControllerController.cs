using Application.Services;
using Application.Services.Interfaces;
using Data.Tenants.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Models.Tenants;
using Domain.Models.Things;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace Site.WebApi.Controllers
{
    [Authorize]
    public class PublicIlluminationControllerController : ApiController
    {
        private IPublicIlluminationControllerAppService _picService;

        private ITenantRepository _tenantRepository;

        private Tenant _currentTenant;

        public PublicIlluminationControllerController()
        {
            _tenantRepository = new TenantRepository();
        }

        // GET api/thing
        public IEnumerable<PublicIlluminationController> Get()
        {
            IdentityGetter();
            RepositoryGetter();

            if (_picService != null)
                return _picService.GetAll();
            else
                return null;
        }

        // GET api/thing/5
        [ResponseType(typeof(PublicIlluminationController))]
        public IHttpActionResult Get(int id)
        {
            IdentityGetter();
            RepositoryGetter();

            if (_picService != null)
            {
                PublicIlluminationController thing = _picService.Find(id);
                if (thing != null)
                {
                    return Ok(thing);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Invalid access token");
            }
        }

        // POST api/thing
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(PublicIlluminationController value)
        {
            if (ModelState.IsValid)
            {
                IdentityGetter();
                RepositoryGetter();

                if (_picService != null)
                {
                    _picService.Add(value);

                    return CreatedAtRoute("DefaultApi", new { id = value.Id }, value);
                }
                else
                {
                    return BadRequest("Invalid access token");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/thing/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, PublicIlluminationController value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            IdentityGetter();
            RepositoryGetter();

            if (_picService != null)
            {
                try
                {
                    _picService.Update(value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_picService.GetAll().Contains(value))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest("Invalid access token");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/thing/5
        [ResponseType(typeof(PublicIlluminationController))]
        public IHttpActionResult Delete(int id)
        {
            IdentityGetter();
            RepositoryGetter();

            if (_picService != null)
            {
                PublicIlluminationController thing = _picService.Find(id);
                if (thing == null)
                {
                    return NotFound();
                }

                _picService.Remove(thing);

                return Ok(thing);
            }
            return BadRequest("Invalid access token");
        }

        #region Helpers
        public void RepositoryGetter()
        {
            if (_currentTenant.Type.Contains("PublicIllumination"))
            {
                _picService = PublicIlluminationControllerAppService.Factory(_currentTenant.Id, new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString), false);
            }
        }

        public void IdentityGetter()
        {
            var userIdentity = (ClaimsIdentity)RequestContext.Principal.Identity;
            var userName = userIdentity.Claims.FirstOrDefault().Value;

            _currentTenant = _tenantRepository.GetAll().Where(t => t.UserName == userName).FirstOrDefault();
        }
        #endregion
    }
}

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
    public class ContainerLevelMeasurerController : ApiController
    {
        private IContainerLevelMeasurerAppService _clmService;

        private ITenantRepository _tenantRepository;

        private Tenant _currentTenant;

        public ContainerLevelMeasurerController()
        {
            _tenantRepository = new TenantRepository();
        }

        // GET api/thing
        public IEnumerable<ContainerLevelMeasurer> Get()
        {
            IdentityGetter();
            RepositoryGetter();

            if (_clmService != null)
                return _clmService.GetAll();
            else
                return null;
        }

        // GET api/thing/5
        [ResponseType(typeof(ContainerLevelMeasurer))]
        public IHttpActionResult Get(int id)
        {
            IdentityGetter();
            RepositoryGetter();

            if (_clmService != null)
            {
                ContainerLevelMeasurer thing = _clmService.Find(id);
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
        public IHttpActionResult Post(ContainerLevelMeasurer value)
        {
            if (ModelState.IsValid)
            {
                IdentityGetter();
                RepositoryGetter();

                if (_clmService != null)
                {
                    _clmService.Add(value);

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
        public IHttpActionResult Put(int id, ContainerLevelMeasurer value)
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

            if (_clmService != null)
            {
                try
                {
                    _clmService.Update(value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_clmService.GetAll().Contains(value))
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
        [ResponseType(typeof(ContainerLevelMeasurer))]
        public IHttpActionResult Delete(int id)
        {
            IdentityGetter();
            RepositoryGetter();

            if (_clmService != null)
            {
                ContainerLevelMeasurer thing = _clmService.Find(id);
                if (thing == null)
                {
                    return NotFound();
                }

                _clmService.Remove(thing);

                return Ok(thing);
            }
            return BadRequest("Invalid access token");
        }

        #region Helpers
        public void RepositoryGetter()
        {
            if (_currentTenant.Type.Contains("ContainerLevel"))
            {
                _clmService = ContainerLevelMeasurerAppService.Factory(_currentTenant.Id, new SqlConnection(ConfigurationManager.ConnectionStrings["IoTDatabase"].ConnectionString), false);
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

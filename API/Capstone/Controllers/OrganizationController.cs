using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationDAO organizationSqlDAO;
        private IProjectDAO projectDAO;

        public OrganizationController(IOrganizationDAO organizationSqlDAO, IProjectDAO projectDAO)
        {
            this.organizationSqlDAO = organizationSqlDAO;
            this.projectDAO = projectDAO;
        }

        [HttpGet("{userId}")]
        public ActionResult<Organization> GetOrganizationInfo(int userId)
        {
            Organization org = organizationSqlDAO.getOrganizationOnLogin(userId);
            org.OrgProjects = projectDAO.getProjectByUserId(userId);

            if (org == null)
            {
                return NotFound();
            }

            return Ok(org);
        }

        [HttpPost]
        public ActionResult<Profile> CreateOrganization(Organization org)
        {
            organizationSqlDAO.CreateOrganization(org);

            if (org == null)
            {
                return NotFound();
            }

            return Created($"/profiles/{org.UserId}", org);
        }

        //[Authorize]
        //[HttpGet("auth")]
        //public ActionResult GetUserInfo()
        //{
        //    string result = $"Your user name is {UserName}, user id is {UserId}, and your role is {UserRole}.";
        //    return Ok(result);
        //}
    }
}
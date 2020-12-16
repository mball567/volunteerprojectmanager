using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ICauseDAO causeDAO;
        private IOrganizationDAO organizationDAO;
        private IProfileDAO profileDAO;
        private IProjectDAO projectDAO;
        private ITeamDAO teamDAO;

        public SearchController(ICauseDAO causeDAO, IOrganizationDAO organizationDAO, IProfileDAO profileDAO, IProjectDAO projectDAO, ITeamDAO teamDAO)
        {
            this.causeDAO = causeDAO;
            this.organizationDAO = organizationDAO;
            this.profileDAO = profileDAO;
            this.projectDAO = projectDAO;
            this.teamDAO = teamDAO;
        }

        [HttpPost]
        public ActionResult<SearchResults> Search(SearchRequest request)
        {
            SearchResults results = new SearchResults();

            if (request.SearchOrganization)
            {
                if (request.Name.Length > 0)
                {
                    results.Organizations = organizationDAO.SearchByName(request.Name);
                }
                else
                {
                    results.Organizations = organizationDAO.SearchByCause(request.CauseIds);
                }
            }
            if (request.SearchProfile)
            {
                if (request.Name.Length > 0)
                {
                    results.Profiles = profileDAO.SearchByName(request.Name);
                }
                else
                {
                    results.Profiles = profileDAO.SearchByCause(request.CauseIds);
                }
            }
            if (request.SearchProject)
            {
                if (request.Name.Length > 0)
                {
                    results.Projects = projectDAO.SearchByName(request.Name);
                }
                else
                {
                    results.Projects = projectDAO.SearchByCause(request.CauseIds);
                }
            }
            if (request.SearchTeam)
            {
                if (request.Name.Length > 0)
                {
                    results.Teams = teamDAO.SearchByName(request.Name);
                }
                else
                {
                    results.Teams = teamDAO.SearchByCause(request.CauseIds);
                }
            }

            return Ok(results);
        }
    }
}
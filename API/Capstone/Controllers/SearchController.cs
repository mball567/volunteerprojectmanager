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
                results.Organizations = organizationDAO.SearchByName(request.Name);
            }
            else if (request.SearchProfile)
            {
                results.Profiles = profileDAO.SearchByName(request.Name);
            }
            else if (request.SearchProject)
            {
                results.Projects = projectDAO.SearchByName(request.Name);
            }
            else if (request.SearchTeam)
            {
                results.Teams = teamDAO.SearchByName(request.Name);
            }

            return Ok(results);
        }
    }
}
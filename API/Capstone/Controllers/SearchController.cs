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

        public SearchController(ICauseDAO causeDAO, IOrganizationDAO organizationDAO)
        {
            this.causeDAO = causeDAO;
            this.organizationDAO = organizationDAO;
        }

        [HttpPost]
        public ActionResult<SearchResults> Search(SearchRequest request)
        {
            SearchResults results = new SearchResults();

            if (request.SearchOrganization)
            {
                results.Organizations = organizationDAO.SearchByName(request.Name);
            }

            return Ok(results);
        }
    }
}
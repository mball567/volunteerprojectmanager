using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("/causes")]
    [ApiController]
    public class CauseController : ControllerBase
    {
        private ICauseDAO causeDAO;

        public CauseController(ICauseDAO causeDAO)
        {
            this.causeDAO = causeDAO;
        }

        [HttpGet]
        public ActionResult<Cause[]> GetOrganizationInfo()
        {
            Cause[] causes = causeDAO.getCauses();

            if (causes == null)
            {
                return NotFound();
            }

            return Ok(causes);
        }
    }
}

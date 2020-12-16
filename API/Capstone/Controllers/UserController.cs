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
    [Route("/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserDAO userDAO;

        public UserController(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUserInfo(int userId)
        {
            User user = userDAO.GetUserFromId(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
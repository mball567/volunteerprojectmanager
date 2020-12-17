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
    [Route("/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectDAO projectDAO;

        public ProjectController(IProjectDAO projectDAO)
        {
            this.projectDAO = projectDAO;
        }

        [HttpGet("{projId}")]
        public ActionResult<Project> GetProjectInfo(int projId)
        {
            Project proj = projectDAO.getProject(projId);

            if (proj == null)
            {
                return NotFound();
            }

            return Ok(proj);
        }

        [HttpPost]
        public ActionResult<Project> CreateProject(Project project)
        {
            projectDAO.CreateProject(project);

            if (project == null)
            {
                return NotFound();
            }

            return Created($"/projects/{project.ProjId}", project);
        }

        [HttpPost("{myEvent.ProjId}/events")]
        public ActionResult<Event> CreateEvent(Event myEvent)
        {
            projectDAO.CreateEvent(myEvent);

            if (myEvent == null)
            {
                return NotFound();
            }

            return Created($"/projects/{myEvent.ProjId}/events", myEvent);
        }

        [HttpPost("events")]
        public ActionResult<Event> SignUpForEvent(Event myEvent)
        {
            projectDAO.EventSignUp(myEvent.EventId, myEvent.UserId);

            if (myEvent == null)
            {
                return NotFound();
            }

            return Created($"/events", myEvent);
        }

        [HttpGet("events/{eventId}")]
        public ActionResult<Event> GetEvent(int eventId)
        {
            Event myEvent = projectDAO.getEvent(eventId);

            if (myEvent == null)
            {
                return NotFound();
            }

            return Ok(myEvent);
        }
    }
}
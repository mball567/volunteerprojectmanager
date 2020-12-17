using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IProjectDAO
    {
        bool CreateEvent(Event myEvent);

        bool CreateProject(Project project);

        bool EventSignUp(int eventId, int userId);

        Event getEvent(int eventId);

        Project getProject(int projId);

        List<Project> getProjectByUserId(int userId);
        List<Event> getProjectEvents(int projectId);
        List<Project> SearchByCause(int[] causeIds);

        List<Project> SearchByName(string name);
    }
}
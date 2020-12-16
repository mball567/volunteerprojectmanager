using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IProjectDAO
    {
        bool CreateEvent(Event myEvent);

        bool CreateProject(Project project);

        bool EventSignUp(int eventId, int userId);

        Project getProject(int projId);

        List<Project> SearchByCause(int[] causeIds);

        List<Project> SearchByName(string name);
    }
}
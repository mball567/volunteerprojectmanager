using Capstone.Models;

namespace Capstone.DAO
{
    public interface IProjectDAO
    {
        bool CreateProject(Project project);

        Project getProject(int projId);
    }
}
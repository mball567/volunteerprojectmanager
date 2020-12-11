using Capstone.Models;

namespace Capstone.DAO
{
    public interface ITeamDAO
    {
        bool CreateTeam(Team team);
        Team getTeam(int teamID);
    }
}
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ITeamDAO
    {
        bool CreateTeam(Team team);
        Team getTeam(int teamID);
        List<Team> SearchByName(string name);
    }
}
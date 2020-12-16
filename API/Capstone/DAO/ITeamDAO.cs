using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ITeamDAO
    {
        bool CreateTeam(Team team);

        Team getTeam(int teamID);

        List<Team> SearchByCause(int[] causeIds);

        List<Team> SearchByName(string name);
    }
}
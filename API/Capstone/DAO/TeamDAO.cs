using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class TeamDAO : ITeamDAO
    {
        private readonly string connectionString;
        private ICauseDAO causeDAO;

        public TeamDAO(string dbConnectionString, ICauseDAO causeDAO)
        {
            connectionString = dbConnectionString;
            this.causeDAO = causeDAO;
        }

        public bool CreateTeam(Team team)
        {
            string sql = @"INSERT into teams (team_name, team_image, team_bio, team_zipcode, team_city, team_state, team_contact_email)
                           VALUES (@teamName, @teamImg, @teamBio, @teamZipCode, @teamCity, @teamState, @teamContactEmail);
                           Select @@IDENTITY";
            string profTeamSql = @"INSERT into profiles_teams (team_id, profile_id)
                                   VALUES (@@IDENTITY, @profileID)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teamName", team.TeamName);
                    cmd.Parameters.AddWithValue("@teamImg", team.TeamImage);
                    cmd.Parameters.AddWithValue("@teamBio", team.TeamBio);
                    cmd.Parameters.AddWithValue("@teamZipCode", team.TeamZip);
                    cmd.Parameters.AddWithValue("@teamCity", team.TeamCity);
                    cmd.Parameters.AddWithValue("@teamState", team.TeamState);
                    cmd.Parameters.AddWithValue("@teamContactEmail", team.TeamContactEmail);

                    int teamId = Convert.ToInt32(cmd.ExecuteScalar());

                    SqlCommand cmdProfTeam = new SqlCommand(profTeamSql, conn);
                    cmdProfTeam.Parameters.AddWithValue("@profileID", team.CreatedBy);
                    cmdProfTeam.ExecuteNonQuery();

                    return causeDAO.AddCausesToRelationalTable(team.TeamCauses, teamId, "teams", "team");
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        //TODO: unsure of parameter. might need to pass in something else.
        public Team getTeam(int teamID)
        {
            string sql = @"Select * from teams where team_id = @teamID";
            Team team = new Team();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teamID", teamID);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        team.TeamId = Convert.ToInt32(rdr["team_id"]);
                        team.TeamName = Convert.ToString(rdr["team_name"]);
                        team.TeamImage = Convert.ToString(rdr["team_image"]);
                        team.TeamBio = Convert.ToString(rdr["team_bio"]);
                        team.TeamZip = Convert.ToInt32(rdr["team_zipcode"]);
                        team.TeamCity = Convert.ToString(rdr["team_city"]);
                        team.TeamState = Convert.ToString(rdr["team_state"]);
                        team.TeamContactEmail = Convert.ToString(rdr["team_contact_email"]);
                    }
                    return team;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
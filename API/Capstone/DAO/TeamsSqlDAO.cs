using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class TeamsSqlDAO
    {
        private readonly string connectionString;

        public TeamsSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CreateTeam(Team team)
        {
            string sql = @"Insert into teams (team_id, team_name, team_image, team_bio, team_zipcode, team_city, team_state, team_contact_email)
                           VALUES (@teamID, @teamName, @teamImg, @teamBio, @teamZipCode, @teamCity, @teamState, @teamContactEmail)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teamName", team.teamName);
                    cmd.Parameters.AddWithValue("@teamImg", team.teamImage);
                    cmd.Parameters.AddWithValue("@teamBio", team.teamBio);
                    cmd.Parameters.AddWithValue("@teamZipCode", team.teamZip);
                    cmd.Parameters.AddWithValue("@teamCity", team.teamCity);
                    cmd.Parameters.AddWithValue("@teamState", team.teamState);
                    cmd.Parameters.AddWithValue("@teamContactEmail", team.teamContactEmail);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                        team.teamId = Convert.ToInt32(rdr["team_id"]);
                        team.teamName = Convert.ToString(rdr["team_name"]);
                        team.teamImage = Convert.ToString(rdr["team_image"]);
                        team.teamBio = Convert.ToString(rdr["team_bio"]);
                        team.teamZip = Convert.ToInt32(rdr["team_zipcode"]);
                        team.teamCity = Convert.ToString(rdr["team_city"]);
                        team.teamState = Convert.ToString(rdr["team_state"]);
                        team.teamContactEmail = Convert.ToString(rdr["team_contact_email"]);
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

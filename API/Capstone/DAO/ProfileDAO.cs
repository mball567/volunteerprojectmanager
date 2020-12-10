using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class ProfileDAO : IProfileDAO
    {
        private readonly string connectionString;

        public ProfileDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CreateProfile(Profile profile)
        {
            string sql = @"Insert into profiles (user_id, first_name, last_name, prof_image, bio, prof_zipcode, prof_city, prof_state, prof_contact_email)
                           VALUES (@userID, @firstName, @lastName, @profImg, @bio, @profZipCode, @profCity, @profState, @profContactEmail)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", profile.UserId);
                    cmd.Parameters.AddWithValue("@firstName", profile.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", profile.LastName);
                    cmd.Parameters.AddWithValue("@profImg", profile.ProfileImage);
                    cmd.Parameters.AddWithValue("@bio", profile.Bio);
                    cmd.Parameters.AddWithValue("@profZipCode", profile.ProfZip);
                    cmd.Parameters.AddWithValue("@profCity", profile.ProfCity);
                    cmd.Parameters.AddWithValue("@profState", profile.ProfState);
                    cmd.Parameters.AddWithValue("@profContactEmail", profile.ProfContactEmail);

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

        public Profile getProfileOnLogin(int userID)
        {
            string sql = @"Select * from profiles where user_id = @userID";
            Profile profile = new Profile();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", userID);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        profile.UserId = Convert.ToInt32(rdr["user_id"]);
                        profile.FirstName = Convert.ToString(rdr["first_name"]);
                        profile.LastName = Convert.ToString(rdr["last_name"]);
                        profile.ProfileImage = Convert.ToString(rdr["prof_image"]);
                        profile.Bio = Convert.ToString(rdr["bio"]);
                        profile.ProfZip = Convert.ToInt32(rdr["prof_zipcode"]);
                        profile.ProfCity = Convert.ToString(rdr["prof_city"]);
                        profile.ProfState = Convert.ToString(rdr["prof_state"]);
                        profile.ProfContactEmail = Convert.ToString(rdr["prof_contact_email"]);
                        profile.ProfileId = Convert.ToInt32(rdr["profile_id"]);
                    }
                    return profile;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
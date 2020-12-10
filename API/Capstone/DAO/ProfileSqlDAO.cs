using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class ProfileSqlDAO : IProfileSqlDAO
    {
        private readonly string connectionString;

        public ProfileSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CreateProfile(Profile profile)
        {
            string sql = @"Insert into profiles (first_name, last_name, prof_image, bio, prof_zipcode, prof_city, prof_state, prof_contact_email)
                           VALUES (@firstName, @lastName, @profImg, @bio, @profZipCode, @profCity, @profState, @profContactEmail)";
            //user_id, @userID, 
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //cmd.Parameters.AddWithValue("@userID", profile.userId);
                    cmd.Parameters.AddWithValue("@firstName", profile.firstName);
                    cmd.Parameters.AddWithValue("@lastName", profile.lastName);
                    cmd.Parameters.AddWithValue("@profImg", profile.profileImage);
                    cmd.Parameters.AddWithValue("@bio", profile.bio);
                    cmd.Parameters.AddWithValue("@profZipCode", profile.profZip);
                    cmd.Parameters.AddWithValue("@profCity", profile.profCity);
                    cmd.Parameters.AddWithValue("@profState", profile.profState);
                    cmd.Parameters.AddWithValue("@profContactEmail", profile.profContactEmail);

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
                        profile.userId = Convert.ToInt32(rdr["user_id"]);
                        profile.firstName = Convert.ToString(rdr["first_name"]);
                        profile.lastName = Convert.ToString(rdr["last_name"]);
                        profile.profileImage = Convert.ToString(rdr["prof_image"]);
                        profile.bio = Convert.ToString(rdr["bio"]);
                        profile.profZip = Convert.ToInt32(rdr["prof_zipcode"]);
                        profile.profCity = Convert.ToString(rdr["prof_city"]);
                        profile.profState = Convert.ToString(rdr["prof_state"]);
                        profile.profContactEmail = Convert.ToString(rdr["prof_contact_email"]);
                        profile.profileId = Convert.ToInt32(rdr["profile_id"]);
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
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
        private ICauseDAO causeDAO;

        public ProfileDAO(string dbConnectionString, ICauseDAO causeDAO)
        {
            connectionString = dbConnectionString;
            this.causeDAO = causeDAO;
        }

        public bool CreateProfile(Profile profile)
        {
            string sql = @"Insert into profiles (user_id, first_name, last_name, prof_image, bio, prof_zipcode, prof_city, prof_state, prof_contact_email)
                           VALUES (@userID, @firstName, @lastName, @profImg, @bio, @profZipCode, @profCity, @profState, @profContactEmail);
                           Select @@IDENTITY";
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

                    int profileId = Convert.ToInt32(cmd.ExecuteScalar());

                    return causeDAO.AddCausesToRelationalTable(profile.ProfCauses, profileId, "profiles", "profile");
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<string> getAllCauseNames(int profileID)
        {
            string sql = @"select causes.cause_name from causes
                            join profiles_causes ON profiles_causes.cause_id = causes.cause_id
                            where profiles_causes.profile_id = @profileID";

            List<string> causes = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@profileID", profileID);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        string cause = "";
                        cause = Convert.ToString(rdr["cause_name"]);
                        causes.Add(cause);
                    }
                    return causes;
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
                    profile.ProfCauseNames = getAllCauseNames(profile.ProfileId).ToArray();
                    return profile;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        private Profile RowToObject(SqlDataReader rdr)
        {
            Profile profile = new Profile();

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

            return profile;
        }

        public List<Profile> SearchByName(string name)
        {
            string sql = @"Select * from profiles where first_name like @name or last_name like @name";

            try
            {
                List<Profile> profiles = new List<Profile>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", $"%{name}%");

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        profiles.Add(RowToObject(rdr));
                    }
                    return profiles;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Profile> SearchByCause(int[] causeIds)
        {
            string sql = @"Select * from profiles
                           Join profiles_causes ON profiles_causes.profile_id = profiles.profile_id
                           Join causes ON causes.cause_id = profiles_causes.cause_id
                           Where causes.cause_id = @causeId";

            try
            {
                List<Profile> profiles = new List<Profile>();
                List<int> profileIds = new List<int>();

                foreach (int causeId in causeIds)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@causeId", causeId);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Profile profile = RowToObject(rdr);

                            if (!profileIds.Contains(profile.ProfileId))
                            {
                                profileIds.Add(profile.ProfileId);
                                profiles.Add(profile);
                            }
                        }
                    }
                }

                return profiles;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
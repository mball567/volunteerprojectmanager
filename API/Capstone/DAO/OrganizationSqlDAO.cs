using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class OrganizationSqlDAO
    {
        private readonly string connectionString;

        public OrganizationSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CreateOrganization(Organization org)
        {
            string sql = @"Insert into organizations (user_id, org_name, org_image, org_bio, org_zipcode, org_city, org_state, org_contact_email)
                           VALUES (@userID, @orgName, @orgImg, @orgBio, @orgZipCode, @orgCity, @orgState, @orgContactEmail)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", org.userId);
                    cmd.Parameters.AddWithValue("@orgName", org.orgName);
                    cmd.Parameters.AddWithValue("@profImg", org.orgImage);
                    cmd.Parameters.AddWithValue("@orgBio", org.orgBio);
                    cmd.Parameters.AddWithValue("@orgZipCode", org.orgZip);
                    cmd.Parameters.AddWithValue("@orgCity", org.orgCity);
                    cmd.Parameters.AddWithValue("@orgState", org.orgState);
                    cmd.Parameters.AddWithValue("@orgContactEmail", org.orgContactEmail);

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

        public Organization getOrganizationOnLogin(int userID)
        {
            string sql = @"Select * from organizations where user_id = @userID";
            Organization org = new Organization();

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
                        org.userId = Convert.ToInt32(rdr["user_id"]);
                        org.orgName = Convert.ToString(rdr["org_name"]);
                        org.orgImage = Convert.ToString(rdr["org_image"]);
                        org.orgBio = Convert.ToString(rdr["org_bio"]);
                        org.orgZip = Convert.ToInt32(rdr["org_zipcode"]);
                        org.orgCity = Convert.ToString(rdr["org_city"]);
                        org.orgState = Convert.ToString(rdr["org_state"]);
                        org.orgContactEmail = Convert.ToString(rdr["org_contact_email"]);
                        org.orgId = Convert.ToInt32(rdr["org_id"]);
                    }
                    return org;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
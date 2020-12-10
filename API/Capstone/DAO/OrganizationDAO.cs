using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class OrganizationDAO : IOrganizationDAO
    {
        private readonly string connectionString;

        public OrganizationDAO(string dbConnectionString)
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
                    cmd.Parameters.AddWithValue("@userID", org.UserId);
                    cmd.Parameters.AddWithValue("@orgName", org.OrgName);
                    cmd.Parameters.AddWithValue("@profImg", org.OrgImage);
                    cmd.Parameters.AddWithValue("@orgBio", org.OrgBio);
                    cmd.Parameters.AddWithValue("@orgZipCode", org.OrgZip);
                    cmd.Parameters.AddWithValue("@orgCity", org.OrgCity);
                    cmd.Parameters.AddWithValue("@orgState", org.OrgState);
                    cmd.Parameters.AddWithValue("@orgContactEmail", org.OrgContactEmail);

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
                        org.UserId = Convert.ToInt32(rdr["user_id"]);
                        org.OrgName = Convert.ToString(rdr["org_name"]);
                        org.OrgImage = Convert.ToString(rdr["org_image"]);
                        org.OrgBio = Convert.ToString(rdr["org_bio"]);
                        org.OrgZip = Convert.ToInt32(rdr["org_zipcode"]);
                        org.OrgCity = Convert.ToString(rdr["org_city"]);
                        org.OrgState = Convert.ToString(rdr["org_state"]);
                        org.OrgContactEmail = Convert.ToString(rdr["org_contact_email"]);
                        org.OrgId = Convert.ToInt32(rdr["org_id"]);
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
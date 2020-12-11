using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class ProjectDAO : IProjectDAO
    {
        private readonly string connectionString;

        public ProjectDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CreateProject(Project project)
        {
            string sql = @"INSERT into projects (org_id, proj_name, proj_desc, proj_image, proj_zipcode, proj_city, proj_state, proj_working_hours, proj_contact_email)
                           VALUES (@orgId, @projName, @projDesc, @projImage, @projZipcode, @projCity, @projState, @projWorkingHours, @projContactEmail);";
            string realtionalSql = @"INSERT into projects (proj_name, proj_desc, proj_image, proj_zipcode, proj_city, proj_state, proj_working_hours, proj_contact_email)
                                     VALUES (@projName, @projDesc, @projImage, @projZipcode, @projCity, @projState, @projWorkingHours, @projContactEmail);
                                     INSERT into profiles_projects (project_id, profile_id)
                                     VALUES(@@IDENTITY, @profileId)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    int rowsAffected = 0;

                    conn.Open();
                    if (project.OrgId > 0)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@orgId", project.OrgId);
                        cmd.Parameters.AddWithValue("@projName", project.ProjName);
                        cmd.Parameters.AddWithValue("@projDesc", project.ProjDesc);
                        cmd.Parameters.AddWithValue("@projImage", project.ProjImage);
                        cmd.Parameters.AddWithValue("@projZipcode", project.ProjZip);
                        cmd.Parameters.AddWithValue("@projCity", project.ProjCity);
                        cmd.Parameters.AddWithValue("@projState", project.ProjState);
                        cmd.Parameters.AddWithValue("@projWorkingHours", project.ProjWorkingHours);
                        cmd.Parameters.AddWithValue("@projContactEmail", project.ProjContactEmail);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }

                    if (project.OrgId == 0)
                    {
                        SqlCommand cmdRelational = new SqlCommand(realtionalSql, conn);
                        cmdRelational.Parameters.AddWithValue("@profileId", project.ProfId);
                        cmdRelational.Parameters.AddWithValue("@projName", project.ProjName);
                        cmdRelational.Parameters.AddWithValue("@projDesc", project.ProjDesc);
                        cmdRelational.Parameters.AddWithValue("@projImage", project.ProjImage);
                        cmdRelational.Parameters.AddWithValue("@projZipcode", project.ProjZip);
                        cmdRelational.Parameters.AddWithValue("@projCity", project.ProjCity);
                        cmdRelational.Parameters.AddWithValue("@projState", project.ProjState);
                        cmdRelational.Parameters.AddWithValue("@projWorkingHours", project.ProjWorkingHours);
                        cmdRelational.Parameters.AddWithValue("@projContactEmail", project.ProjContactEmail);
                        rowsAffected = cmdRelational.ExecuteNonQuery();
                    }

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

        public Project getProject(int projId)
        {
            string sql = @"Select * from projects where proj_id = @projId";
            Project proj = new Project();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@projId", projId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        proj.ProjId = Convert.ToInt32(rdr["proj_id"]);
                        proj.OrgId = Convert.ToInt32(rdr["org_id"]);
                        proj.ProjName = Convert.ToString(rdr["proj_name"]);
                        proj.ProjDesc = Convert.ToString(rdr["proj_desc"]);
                        proj.ProjImage = Convert.ToString(rdr["proj_image"]);
                        proj.ProjZip = Convert.ToInt32(rdr["proj_zipcode"]);
                        proj.ProjCity = Convert.ToString(rdr["proj_city"]);
                        proj.ProjState = Convert.ToString(rdr["proj_state"]);
                        proj.ProjWorkingHours = Convert.ToInt32(rdr["proj_working_hours"]);
                        proj.ProjContactEmail = Convert.ToString(rdr["proj_contact_email"]);
                    }
                    return proj;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
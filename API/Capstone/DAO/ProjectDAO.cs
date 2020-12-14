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
        private ICauseDAO causeDAO;

        public ProjectDAO(string dbConnectionString, ICauseDAO causeDAO)
        {
            connectionString = dbConnectionString;
            this.causeDAO = causeDAO;
        }

        public bool CreateProject(Project project)
        {
            string sql = @"INSERT into projects (org_id, proj_name, proj_desc, proj_image, proj_zipcode, proj_city, proj_state, proj_working_hours, proj_contact_email)
                           VALUES (@orgId, @projName, @projDesc, @projImage, @projZipcode, @projCity, @projState, @projWorkingHours, @projContactEmail);
                           Select @@IDENTITY";
            string projectsSql = @"INSERT into projects (proj_name, proj_desc, proj_image, proj_zipcode, proj_city, proj_state, proj_working_hours, proj_contact_email)
                                     VALUES (@projName, @projDesc, @projImage, @projZipcode, @projCity, @projState, @projWorkingHours, @projContactEmail);
                                     Select @@IDENTITY";
            string profProjSql = @"INSERT into profiles_projects(project_id, profile_id)
                                     VALUES(@@IDENTITY, @profileId)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
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

                        int projId = Convert.ToInt32(cmd.ExecuteScalar());

                        return causeDAO.AddCausesToRelationalTable(project.ProjCauses, projId, "projects", "proj");
                    }

                    if (project.OrgId == 0)
                    {
                        SqlCommand cmdProjects = new SqlCommand(projectsSql, conn);
                        cmdProjects.Parameters.AddWithValue("@projName", project.ProjName);
                        cmdProjects.Parameters.AddWithValue("@projDesc", project.ProjDesc);
                        cmdProjects.Parameters.AddWithValue("@projImage", project.ProjImage);
                        cmdProjects.Parameters.AddWithValue("@projZipcode", project.ProjZip);
                        cmdProjects.Parameters.AddWithValue("@projCity", project.ProjCity);
                        cmdProjects.Parameters.AddWithValue("@projState", project.ProjState);
                        cmdProjects.Parameters.AddWithValue("@projWorkingHours", project.ProjWorkingHours);
                        cmdProjects.Parameters.AddWithValue("@projContactEmail", project.ProjContactEmail);

                        int projId = Convert.ToInt32(cmdProjects.ExecuteScalar());

                        SqlCommand cmdProfProj = new SqlCommand(profProjSql, conn);
                        cmdProfProj.Parameters.AddWithValue("@profileId", project.ProfId);

                        cmdProfProj.ExecuteNonQuery();

                        return causeDAO.AddCausesToRelationalTable(project.ProjCauses, projId, "projects", "proj");
                    }
                    return false;
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
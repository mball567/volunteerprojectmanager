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
            string sql = @"INSERT into projects (user_id, proj_name, proj_desc, proj_image, proj_zipcode, proj_city, proj_state, proj_working_hours, proj_contact_email)
                           VALUES (@userId, @projName, @projDesc, @projImage, @projZipcode, @projCity, @projState, @projWorkingHours, @projContactEmail);
                           Select @@IDENTITY";

            string profProjSql = @"INSERT into profiles_projects(project_id, profile_id)
                                     VALUES(@@IDENTITY, @profileId)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (project.ProfId == 0)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@userId", project.UserId);
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

                    if (project.ProfId > 0)
                    {
                        SqlCommand cmdProjects = new SqlCommand(sql, conn);
                        cmdProjects.Parameters.AddWithValue("@userId", project.UserId);
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

        public List<string> getAllCauseNames(int projId)
        {
            string sql = @"select causes.cause_name from causes
                            join projects_causes ON projects_causes.cause_id = causes.cause_id
                            where projects_causes.proj_id = @projId";

            List<string> causes = new List<string>();

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
                        proj.UserId = Convert.ToInt32(rdr["user_id"]);
                        proj.ProjName = Convert.ToString(rdr["proj_name"]);
                        proj.ProjDesc = Convert.ToString(rdr["proj_desc"]);
                        proj.ProjImage = Convert.ToString(rdr["proj_image"]);
                        proj.ProjZip = Convert.ToInt32(rdr["proj_zipcode"]);
                        proj.ProjCity = Convert.ToString(rdr["proj_city"]);
                        proj.ProjState = Convert.ToString(rdr["proj_state"]);
                        proj.ProjWorkingHours = Convert.ToInt32(rdr["proj_working_hours"]);
                        proj.ProjContactEmail = Convert.ToString(rdr["proj_contact_email"]);
                    }
                    proj.ProjCauseNames = getAllCauseNames(proj.ProjId).ToArray();
                    proj.ProjEvents = getProjectEvents(proj.ProjId);
                    return proj;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        private Project RowToObjectCreatedProject(SqlDataReader rdr)
        {
            Project proj = new Project();

            proj.ProjId = Convert.ToInt32(rdr["proj_id"]);
            proj.UserId = Convert.ToInt32(rdr["user_id"]);
            proj.ProjName = Convert.ToString(rdr["proj_name"]);
            proj.ProjDesc = Convert.ToString(rdr["proj_desc"]);
            proj.ProjImage = Convert.ToString(rdr["proj_image"]);
            proj.ProjZip = Convert.ToInt32(rdr["proj_zipcode"]);
            proj.ProjCity = Convert.ToString(rdr["proj_city"]);
            proj.ProjState = Convert.ToString(rdr["proj_state"]);
            proj.ProjWorkingHours = Convert.ToInt32(rdr["proj_working_hours"]);
            proj.ProjContactEmail = Convert.ToString(rdr["proj_contact_email"]);

            return proj;
        }

        public List<Project> SearchByName(string name)
        {
            string sql = @"Select * from projects where proj_name like @name";

            try
            {
                List<Project> projects = new List<Project>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", $"%{name}%");

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        projects.Add(RowToObjectCreatedProject(rdr));
                    }
                    return projects;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Project> SearchByCause(int[] causeIds)
        {
            string sql = @"Select * from projects
                           Join projects_causes ON projects_causes.proj_id = projects.proj_id
                           Join causes ON causes.cause_id = projects_causes.cause_id
                           Where causes.cause_id = @causeId";

            try
            {
                List<Project> projects = new List<Project>();
                List<int> projectIds = new List<int>();

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
                            Project project = RowToObjectCreatedProject(rdr);

                            if (!projectIds.Contains(project.ProjId))
                            {
                                projectIds.Add(project.ProjId);
                                projects.Add(project);
                            }
                        }
                    }
                }

                return projects;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool CreateEvent(Event myEvent)
        {
            string sql = @"Insert into events (user_id, proj_id, event_name, event_desc, event_zipcode, event_city, event_state, event_contact_email, event_starttime, event_endtime, event_working_hours, event_date)
                           VALUES (@userId, @projId, @eventName, @eventDesc, @eventZipcode, @eventCity, @eventState, @eventContactEmail, @eventStartTime, @eventEndTime, @eventWorkingHours, @eventDate);
                           Select @@IDENTITY";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", myEvent.UserId);
                    cmd.Parameters.AddWithValue("@projId", myEvent.ProjId);
                    cmd.Parameters.AddWithValue("@eventName", myEvent.EventName);
                    cmd.Parameters.AddWithValue("@eventDesc", myEvent.EventDesc);
                    cmd.Parameters.AddWithValue("@eventZipcode", myEvent.EventZip);
                    cmd.Parameters.AddWithValue("@eventCity", myEvent.EventCity);
                    cmd.Parameters.AddWithValue("@eventState", myEvent.EventState);
                    cmd.Parameters.AddWithValue("@eventContactEmail", myEvent.EventContactEmail);
                    cmd.Parameters.AddWithValue("@eventStartTime", myEvent.EventStartTime);
                    cmd.Parameters.AddWithValue("@eventEndTime", myEvent.EventEndTime);
                    cmd.Parameters.AddWithValue("@eventWorkingHours", myEvent.EventWorkingHours);
                    cmd.Parameters.AddWithValue("@eventDate", myEvent.EventDate);

                    int eventId = Convert.ToInt32(cmd.ExecuteScalar());

                    EventSignUp(eventId, myEvent.UserId);

                    if (eventId > 0)
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

        public bool EventSignUp(int eventId, int userId)
        {
            string sql = @"Insert into events_users (user_id, event_id)
                           VALUES (@userId, @eventId)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    int rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

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

        public Event getEvent(int eventId)
        {
            string sql = @"Select * from events where event_id = @eventId";
            Event myEvent = new Event();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Project proj = new Project();

                        myEvent.EventId = Convert.ToInt32(rdr["event_id"]);
                        myEvent.ProjId = Convert.ToInt32(rdr["proj_id"]);
                        myEvent.EventName = Convert.ToString(rdr["event_name"]);
                        myEvent.EventDesc = Convert.ToString(rdr["event_desc"]);
                        myEvent.EventZip = Convert.ToInt32(rdr["event_zipcode"]);
                        myEvent.EventCity = Convert.ToString(rdr["event_city"]);
                        myEvent.EventState = Convert.ToString(rdr["event_state"]);
                        myEvent.EventWorkingHours = Convert.ToInt32(rdr["event_working_hours"]);
                        myEvent.EventContactEmail = Convert.ToString(rdr["event_contact_email"]);
                        myEvent.EventStartTime = Convert.ToString(rdr["event_starttime"]);
                        myEvent.EventEndTime = Convert.ToString(rdr["event_endtime"]);
                        myEvent.EventDate = Convert.ToString(rdr["event_date"]);
                    }
                    myEvent.EventProfiles = getEventProfiles(myEvent.EventId);

                    return myEvent;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Event> getProjectEvents(int projectId)
        {
            string sql = @"Select * from events where proj_id = @projId";
            List<Event> events = new List<Event>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@projId", projectId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Event myEvent = new Event();

                        myEvent.EventId = Convert.ToInt32(rdr["event_id"]);
                        myEvent.EventName = Convert.ToString(rdr["event_name"]);
                        myEvent.EventDesc = Convert.ToString(rdr["event_desc"]);
                        myEvent.EventZip = Convert.ToInt32(rdr["event_zipcode"]);
                        myEvent.EventCity = Convert.ToString(rdr["event_city"]);
                        myEvent.EventState = Convert.ToString(rdr["event_state"]);
                        myEvent.EventWorkingHours = Convert.ToInt32(rdr["event_working_hours"]);
                        myEvent.EventContactEmail = Convert.ToString(rdr["event_contact_email"]);
                        myEvent.EventStartTime = Convert.ToString(rdr["event_starttime"]);
                        myEvent.EventEndTime = Convert.ToString(rdr["event_endtime"]);
                        myEvent.EventDate = Convert.ToString(rdr["event_date"]);

                        events.Add(myEvent);
                    }

                    return events;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Profile> getEventProfiles(int eventId)
        {
            string sql = @"Select * from profiles
                           Join users on users.user_id = profiles.user_id
                           Join events_users on events_users.user_id = users.user_id
                           Where events_users.event_id = @eventId";

            List<Profile> profiles = new List<Profile>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Profile profile = new Profile();

                        profile.UserId = Convert.ToInt32(rdr["user_id"]);
                        profile.FirstName = Convert.ToString(rdr["first_name"]);
                        profile.LastName = Convert.ToString(rdr["last_name"]);

                        profiles.Add(profile);
                    }

                    return profiles;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Project> getProjectByUserId(int userId)
        {
            string sql = @"Select * from projects where user_id = @userId";
            List<Project> projects = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Project proj = new Project();

                        proj.ProjId = Convert.ToInt32(rdr["proj_id"]);
                        proj.UserId = Convert.ToInt32(rdr["user_id"]);
                        proj.ProjName = Convert.ToString(rdr["proj_name"]);
                        proj.ProjDesc = Convert.ToString(rdr["proj_desc"]);
                        proj.ProjImage = Convert.ToString(rdr["proj_image"]);
                        proj.ProjZip = Convert.ToInt32(rdr["proj_zipcode"]);
                        proj.ProjCity = Convert.ToString(rdr["proj_city"]);
                        proj.ProjState = Convert.ToString(rdr["proj_state"]);
                        proj.ProjWorkingHours = Convert.ToInt32(rdr["proj_working_hours"]);
                        proj.ProjContactEmail = Convert.ToString(rdr["proj_contact_email"]);

                        projects.Add(proj);
                    }

                    return projects;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
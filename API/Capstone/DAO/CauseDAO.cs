using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CauseDAO : ICauseDAO
    {
        private readonly string connectionString;

        public CauseDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Cause[] GetCauses()
        {
            string sql = @"Select * from causes";

            try
            {
                List<Cause> causes = new List<Cause>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Cause cause = new Cause();

                        cause.CauseId = Convert.ToInt32(rdr["cause_id"]);
                        cause.CauseName = Convert.ToString(rdr["cause_name"]);

                        causes.Add(cause);
                    }
                    Cause[] causesArray = causes.ToArray();
                    return causesArray;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool AddCausestoOrganization(int[] causeIds, int orgId)
        {
            string sql = @"Insert into organizations_causes (cause_id, org_id)
                                     Values ";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@orgId", orgId);

            for (int i = 0; i < causeIds.Length; i++)
            {
                if (i > 0)
                {
                    sql += ", ";
                }

                sql += $"(@cause{i}, @orgId)";

                cmd.Parameters.AddWithValue($"@cause{i}", causeIds[i]);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private bool PopulateRelationalCausesTable(string sqlStatement, int[] causeIds, SqlConnection connection)
        //{
        //    string sql = sqlStatement;

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            foreach (int causeId in causeIds)
        //            {
        //                SqlCommand cmd = new SqlCommand(sql, conn);
        //                cmd.Parameters.AddWithValue("@causeId", causeId);

        //                int rowsAffected = cmd.ExecuteNonQuery();

        //                if (rowsAffected == 1)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            return false;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
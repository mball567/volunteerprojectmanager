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

        public Cause[] getCauses()
        {
            string sql = @"Select * from causes";
            ;

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
    }
}

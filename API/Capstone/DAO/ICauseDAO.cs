using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface ICauseDAO
    {
        bool AddCausestoOrganization(int[] causeIds, int orgId);

        Cause[] GetCauses();

        //bool PopulateRelationalCausesTable(string sqlStatement, int[] causeIds, SqlConnection connection);
    }
}
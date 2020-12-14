using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface ICauseDAO
    {
        bool AddCausesToRelationalTable(int[] causeIds, int orgId, string tableName, string idName);

        Cause[] GetCauses();
    }
}
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IOrganizationSqlDAO
    {
        bool CreateOrganization(Organization org);
        Organization getOrganizationOnLogin(int userID);
    }
}
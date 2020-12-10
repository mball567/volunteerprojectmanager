using Capstone.Models;

namespace Capstone.DAO
{
    public interface IOrganizationDAO
    {
        bool CreateOrganization(Organization org);
        Organization getOrganizationOnLogin(int userID);
    }
}
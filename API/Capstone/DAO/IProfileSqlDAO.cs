using Capstone.Models;

namespace Capstone.DAO
{
    public interface IProfileSqlDAO
    {
        bool CreateProfile(Profile profile);
        Profile getProfileOnLogin(int userID);
    }
}
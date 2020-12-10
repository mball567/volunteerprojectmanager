using Capstone.Models;

namespace Capstone.DAO
{
    public interface IProfileDAO
    {
        bool CreateProfile(Profile profile);
        Profile getProfileOnLogin(int userID);
    }
}
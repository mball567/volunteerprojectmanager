using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IProfileDAO
    {
        bool CreateProfile(Profile profile);
        Profile getProfileOnLogin(int userID);
        List<Profile> SearchByCause(int[] causeIds);
        List<Profile> SearchByName(string name);
    }
}
﻿using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IOrganizationDAO
    {
        bool CreateOrganization(Organization org);

        List<string> getAllCauseNames(int orgID);

        Organization getOrganizationOnLogin(int userID);
        List<Organization> SearchByCause(int[] causeIds);
        List<Organization> SearchByName(string orgName);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using TheBankMVC.Models;

namespace SquadMobile.Services.DAO
{
    class UserAccountsDAO : DataService<UserAccount>
    {
        public UserAccountsDAO() : base("UserAccounts")
        {
        }
    }
}

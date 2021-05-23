using System;
using System.Collections.Generic;
using System.Text;
using TheBankMVC.Models;

namespace SquadMobile.Services.DAO
{
    class TransactionsDAO : DataService<Transaction>
    {
        public TransactionsDAO() : base("Transactions")
        {
        }
    }
}

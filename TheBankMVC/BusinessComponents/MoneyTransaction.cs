using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Data;
using TheBankMVC.Models;

namespace TheBankMVC.BusinessComponents
{
    public class MoneyTransaction
    {
        private readonly ApplicationDbContext _context;

        public MoneyTransaction(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMoneyTransaction(Transaction transaction)
        {
            if (transaction.TransactionTypeId == (int)Enumeration.TransactionType.Credit)
            {
                if(transaction.ReferenceTypeId == (int)Enumeration.TransactionRefType.Loan)
                {
                    //_context
                }

            }
            else if (transaction.TransactionTypeId == (int)Enumeration.TransactionType.Credit)
            {

            }
            transaction.ReferenceType = ((Enumeration.TransactionRefType)transaction.ReferenceTypeId).ToString();
            _context.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}

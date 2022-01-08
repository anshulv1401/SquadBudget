using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Data;
using TheBankMVC.Models;

namespace TheBankMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(string query = null)
        {
            if (!string.IsNullOrEmpty(query) && int.TryParse(query, out int BankId))
            {
                return await _context.Transactions.Where(x => x.BankId == BankId).ToListAsync();
            }
            else
            {
                return await _context.Transactions.ToListAsync();
            }
        }
    }
}
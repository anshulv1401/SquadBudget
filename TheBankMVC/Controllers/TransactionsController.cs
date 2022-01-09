using BudgetManager.BusinessComponents;
using BudgetManager.Data;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MoneyTransactionComponent moneyTransaction;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
            moneyTransaction = new MoneyTransactionComponent(_context);
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.OrderByDescending(x => x.TransactionDate).ToListAsync();
            var transactionsViewModels = new List<TransactionsViewModel>();
            foreach (var transaction in transactions)
            {
                transactionsViewModels.Add(new TransactionsViewModel()
                {
                    TransactionId = transaction.TransactionId,
                    GroupId = transaction.GroupId,
                    UserAccountId = transaction.UserAccountId,
                    TransactionTypeId = transaction.TransactionTypeId,
                    TransactionAmount = transaction.TransactionAmount,
                    TransactionDate = transaction.TransactionDate,
                    ReferenceType = transaction.ReferenceType,
                    ReferenceTypeId = transaction.ReferenceTypeId,
                    TransactionRemark = transaction.TransactionRemark,
                    Groups = _context.Groups.Where(m => m.GroupId == transaction.GroupId).ToList(),
                    UserAccounts = _context.UserAccounts.Where(m => m.UserAccountId == transaction.UserAccountId).ToList()
                });
            }

            return View(transactionsViewModels);
        }


        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionsViewModel = new TransactionsViewModel()
            {
                TransactionId = transaction.TransactionId,
                GroupId = transaction.GroupId,
                UserAccountId = transaction.UserAccountId,
                TransactionTypeId = transaction.TransactionTypeId,
                TransactionAmount = transaction.TransactionAmount,
                TransactionDate = transaction.TransactionDate,
                ReferenceType = transaction.ReferenceType,
                ReferenceTypeId = transaction.ReferenceTypeId,
                TransactionRemark = transaction.TransactionRemark,
                Groups = _context.Groups.Where(m => m.GroupId == transaction.GroupId).ToList(),
                UserAccounts = _context.UserAccounts.Where(m => m.UserAccountId == transaction.UserAccountId).ToList()
            };

            return View(transactionsViewModel);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var transactionsViewModel = new TransactionsViewModel();
            transactionsViewModel.Groups = _context.Groups.ToList();
            transactionsViewModel.UserAccounts = new List<UserAccount>();
            return View(transactionsViewModel);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,GroupId,UserAccountId,TransactionTypeId,TransactionAmount,TransactionDate,ReferenceType,ReferenceTypeId,TransactionRemark")] TransactionsViewModel transactionsViewModel)
        {
            if (ModelState.IsValid && transactionsViewModel.ReferenceTypeId != 0)
            {
                var transaction = new Transaction()
                {
                    TransactionId = transactionsViewModel.TransactionId,
                    GroupId = transactionsViewModel.GroupId,
                    UserAccountId = transactionsViewModel.UserAccountId,
                    TransactionTypeId = transactionsViewModel.TransactionTypeId,
                    TransactionAmount = transactionsViewModel.TransactionAmount,
                    TransactionDate = transactionsViewModel.TransactionDate,
                    ReferenceType = transactionsViewModel.ReferenceType,
                    ReferenceTypeId = transactionsViewModel.ReferenceTypeId,
                    TransactionRemark = transactionsViewModel.TransactionRemark
                };

                await moneyTransaction.CreateMoneyTransaction(transaction);
                return RedirectToAction(nameof(Index));
            }

            return View(transactionsViewModel);
        }
        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}

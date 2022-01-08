using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.BusinessComponents;
using TheBankMVC.Data;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;

namespace TheBankMVC.Controllers
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
                    BankId = transaction.BankId,
                    UserAccountId = transaction.UserAccountId,
                    TransactionTypeId = transaction.TransactionTypeId,
                    TransactionAmount = transaction.TransactionAmount,
                    TransactionDate = transaction.TransactionDate,
                    ReferenceType = transaction.ReferenceType,
                    ReferenceTypeId = transaction.ReferenceTypeId,
                    TransactionRemark = transaction.TransactionRemark,
                    Banks = _context.Bank.Where(m => m.BankId == transaction.BankId).ToList(),
                    UserAccounts = _context.UserAccount.Where(m => m.UserAccountId == transaction.UserAccountId).ToList()
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
                BankId = transaction.BankId,
                UserAccountId = transaction.UserAccountId,
                TransactionTypeId = transaction.TransactionTypeId,
                TransactionAmount = transaction.TransactionAmount,
                TransactionDate = transaction.TransactionDate,
                ReferenceType = transaction.ReferenceType,
                ReferenceTypeId = transaction.ReferenceTypeId,
                TransactionRemark = transaction.TransactionRemark,
                Banks = _context.Bank.Where(m => m.BankId == transaction.BankId).ToList(),
                UserAccounts = _context.UserAccount.Where(m => m.UserAccountId == transaction.UserAccountId).ToList()
            };

            return View(transactionsViewModel);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var transactionsViewModel = new TransactionsViewModel();
            transactionsViewModel.Banks = _context.Bank.ToList();
            transactionsViewModel.UserAccounts = new List<UserAccount>();
            return View(transactionsViewModel);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,BankId,UserAccountId,TransactionTypeId,TransactionAmount,TransactionDate,ReferenceType,ReferenceTypeId,TransactionRemark")] TransactionsViewModel transactionsViewModel)
        {
            if (ModelState.IsValid && transactionsViewModel.ReferenceTypeId != 0)
            {
                var transaction = new Transaction()
                {
                    TransactionId = transactionsViewModel.TransactionId,
                    BankId = transactionsViewModel.BankId,
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

        // GET: Transactions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transaction = await _context.Transaction.FindAsync(id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(transaction);
        //}

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TransactionId,BankId,AccountId,TransactionTypeId,TransactionAmount,TransactionDate,ReferenceType,ReferenceTypeId,TransactionRemark")] Transaction transaction)
        //{
        //    if (id != transaction.TransactionId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(transaction);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TransactionExists(transaction.TransactionId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(transaction);
        //}

        //// GET: Transactions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transaction = await _context.Transaction
        //        .FirstOrDefaultAsync(m => m.TransactionId == id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transaction);
        //}

        //// POST: Transactions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var transaction = await _context.Transaction.FindAsync(id);
        //    _context.Transaction.Remove(transaction);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}

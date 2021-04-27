using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBankMVC.Data;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;

namespace TheBankMVC.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.ToListAsync();
            var transactionsViewModels = new List<TransactionsViewModel>();
            foreach (var transaction in transactions)
            {
                transactionsViewModels.Add(new TransactionsViewModel()
                {
                    TransactionId = transaction.TransactionId,
                    BankId = transaction.BankId,
                    AccountId = transaction.AccountId,
                    TransactionTypeId = transaction.TransactionTypeId,
                    TransactionAmount = transaction.TransactionAmount,
                    TransactionDate = transaction.TransactionDate,
                    ReferenceType = transaction.ReferenceType,
                    ReferenceTypeId = transaction.ReferenceTypeId,
                    TransactionRemark = transaction.TransactionRemark,
                    Banks = _context.Bank.Where(m => m.BankId == transaction.BankId).ToList(),
                    UserAccounts = _context.UserAccount.Where(m => m.UserAccountId == transaction.AccountId).ToList()
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

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionsViewModel = new TransactionsViewModel()
            {
                TransactionId = transaction.TransactionId,
                BankId = transaction.BankId,
                AccountId = transaction.AccountId,
                TransactionTypeId = transaction.TransactionTypeId,
                TransactionAmount = transaction.TransactionAmount,
                TransactionDate = transaction.TransactionDate,
                ReferenceType = transaction.ReferenceType,
                ReferenceTypeId = transaction.ReferenceTypeId,
                TransactionRemark = transaction.TransactionRemark,
                Banks = _context.Bank.Where(m => m.BankId == transaction.BankId).ToList(),
                UserAccounts = _context.UserAccount.Where(m => m.UserAccountId == transaction.AccountId).ToList()
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
        public async Task<IActionResult> Create([Bind("TransactionId,BankId,AccountId,TransactionTypeId,TransactionAmount,TransactionDate,ReferenceType,ReferenceTypeId,TransactionRemark")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.ReferenceType = ((Enumeration.TransactionRefType)transaction.ReferenceTypeId).ToString();
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
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
            return _context.Transaction.Any(e => e.TransactionId == id);
        }
    }
}

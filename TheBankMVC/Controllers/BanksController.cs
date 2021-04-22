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
    public class BanksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BanksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Banks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bank.ToListAsync());
        }

        // GET: Banks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Bank
                .FirstOrDefaultAsync(m => m.BankId == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Banks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankId,BankName,BankInstallmentAmount,DefaultLoanInterest,DefaultNoOfInstallment,BankInstallmentDelayFine,BankInstallmentDelayFineType,BankInstallmentDelayFineTerm,LoanDelayFine,LoanDelayFineType,LoanDelayFineTerm,InterestTermID,DateFormat")] Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return View(bank);
            }

            if (bank.BankId == 0)
            {
                _context.Add(bank);
            }
            else
            {
                var bankInDb = _context.Bank.Single(c => c.BankId == bank.BankId);
                bankInDb.BankName = bank.BankName;
                bankInDb.BankInstallmentAmount = bank.BankInstallmentAmount;
                bankInDb.DefaultLoanInterest = bank.DefaultLoanInterest;
                bankInDb.DefaultNoOfInstallment = bank.DefaultNoOfInstallment;
                bankInDb.BankInstallmentDelayFine = bank.BankInstallmentDelayFine;
                bankInDb.BankInstallmentDelayFineTerm = bank.BankInstallmentDelayFineTerm;
                bankInDb.BankInstallmentDelayFineType = bank.BankInstallmentDelayFineType;
                bankInDb.LoanDelayFine = bank.LoanDelayFine;
                bankInDb.LoanDelayFineType = bank.LoanDelayFineType;
                bankInDb.LoanDelayFineTerm = bank.LoanDelayFineTerm;
                bankInDb.InterestTermID = bank.InterestTermID;
                bankInDb.DateFormat = bank.DateFormat;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Banks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Bank.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            return View("Create", bank);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankId,BankName,BankInstallmentAmount,DefaultLoanInterest,DefaultNoOfInstallment,BankInstallmentDelayFine,BankInstallmentDelayFineType,BankInstallmentDelayFinePeriod,LoanDelayFine,LoanDelayFineType,LoanDelayFinePeriod,InterestTermID,DateFormat")] Bank bank)
        {
            if (id != bank.BankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.BankId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }

        // GET: Banks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Bank
                .FirstOrDefaultAsync(m => m.BankId == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bank = await _context.Bank.FindAsync(id);
            _context.Bank.Remove(bank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.BankId == id);
        }
    }
}

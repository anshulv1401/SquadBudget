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
    public class UserAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            var userAccounts = await _context.UserAccount.ToListAsync();
            var userAccountViewModels = new List<UserAccountViewModel>();
            foreach (var userAccount in userAccounts)
            {
                userAccountViewModels.Add(new UserAccountViewModel()
                {
                    GroupId = userAccount.GroupId,
                    PhoneNo = userAccount.PhoneNo,
                    Groups = _context.Group.Where(m => m.GroupId == userAccount.GroupId).ToList(),
                    UserAccountId = userAccount.UserAccountId,
                    UserAccountName = userAccount.UserAccountName,
                    Email = userAccount.Email,
                    ShareSubmitted = userAccount.ShareSubmitted,
                    FineSubmitted = userAccount.FineSubmitted,
                    InterestSubmitted = userAccount.InterestSubmitted,
                    IsActive = userAccount.IsActive,
                    AmountOnLoan = userAccount.AmountOnLoan,
                });
            }

            return View(userAccountViewModels);
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .FirstOrDefaultAsync(m => m.UserAccountId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            var groups = _context.Group.ToList();
            var userAccountViewModel = new UserAccountViewModel()
            {
                Groups = groups
            };
            return View(userAccountViewModel);
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,UserAccountId,UserAccountName,Email,PhoneNo,ShareSubmitted,FineSubmitted,InterestSubmitted,IsActive,AmountOnLoan")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                userAccount.IsActive = true;
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            var userAccountViewModel = new UserAccountViewModel()
            {
                GroupId = userAccount.GroupId,
                PhoneNo = userAccount.PhoneNo,
                Groups = _context.Group.Where(m => m.GroupId == userAccount.GroupId).ToList(),
                UserAccountId = userAccount.UserAccountId,
                UserAccountName = userAccount.UserAccountName,
                Email = userAccount.Email,
                ShareSubmitted = userAccount.ShareSubmitted,
                FineSubmitted = userAccount.FineSubmitted,
                InterestSubmitted = userAccount.InterestSubmitted,
                IsActive = userAccount.IsActive,
                AmountOnLoan = userAccount.AmountOnLoan,
            };

            return View(userAccountViewModel);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,UserAccountId,UserAccountName,Email,PhoneNo,IsActive")] UserAccountViewModel userAccountViewModel)
        {
            if (id != userAccountViewModel.UserAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userAccount = _context.UserAccount.Where(x => x.UserAccountId == userAccountViewModel.UserAccountId).FirstOrDefault();

                    userAccount.PhoneNo = userAccountViewModel.PhoneNo;
                    userAccount.Email = userAccountViewModel.Email;
                    userAccount.IsActive = userAccountViewModel.IsActive;

                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccountViewModel.UserAccountId))
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
            return View(userAccountViewModel);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .FirstOrDefaultAsync(m => m.UserAccountId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);
            _context.UserAccount.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.UserAccountId == id);
        }
    }
}

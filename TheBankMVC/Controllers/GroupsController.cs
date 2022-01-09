using BudgetManager.Data;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var groupDetails = await _context.Groups.ToListAsync();

            var groupViewModels = new List<GroupViewModel>();

            foreach (var group in groupDetails)
            {
                var users = _context.UserAccounts.Where(x => x.GroupId == group.GroupId).ToList();

                var totalShare = users.Sum(x => x.ShareSubmitted);
                var totalFine = users.Sum(x => x.FineSubmitted);
                var totalInterest = users.Sum(x => x.InterestSubmitted);
                var totalAmtOnLoan = users.Sum(x => x.AmountOnLoan);
                var TotalAmtInGroup = totalShare + totalFine + totalInterest - totalAmtOnLoan;

                var groupViewModel = new GroupViewModel()
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    GroupInstAmt = group.GroupInstallmentAmount,
                    TotalShare = totalShare,
                    TotalFine = totalFine,
                    TotalInterest = totalInterest,
                    TotalAmtOnLoan = totalAmtOnLoan,
                    TotalAmtInGroup = TotalAmtInGroup
                };
                groupViewModels.Add(groupViewModel);
            }

            return View(groupViewModels);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupName,GroupInstallmentAmount,InstallmentDayOfMonth,DefaultLoanInterest,DefaultNoOfInstallment,GroupInstallmentDelayFine,GroupInstallmentDelayFineType,GroupInstallmentDelayFineTerm,LoanDelayFine,LoanDelayFineType,LoanDelayFineTerm,InterestTermID,DateFormat")] Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (group.GroupId == 0)
                    {
                        _context.Add(group);
                        var groupUserMapping = new GroupUserMapping()
                        {
                            GroupId = group.GroupId,
                            UserId = _context.GetCurrentUserId()
                        };
                        _context.Add(groupUserMapping);
                    }
                    else
                    {
                        var groupInDb = _context.Groups.Single(c => c.GroupId == group.GroupId);
                        groupInDb.GroupName = group.GroupName;
                        groupInDb.GroupInstallmentAmount = group.GroupInstallmentAmount;
                        groupInDb.InstallmentDayOfMonth = group.InstallmentDayOfMonth;
                        groupInDb.DefaultLoanInterest = group.DefaultLoanInterest;
                        groupInDb.DefaultNoOfInstallment = group.DefaultNoOfInstallment;
                        groupInDb.GroupInstallmentDelayFine = group.GroupInstallmentDelayFine;
                        groupInDb.GroupInstallmentDelayFineTerm = group.GroupInstallmentDelayFineTerm;
                        groupInDb.GroupInstallmentDelayFineType = group.GroupInstallmentDelayFineType;
                        groupInDb.LoanDelayFine = group.LoanDelayFine;
                        groupInDb.LoanDelayFineType = group.LoanDelayFineType;
                        groupInDb.LoanDelayFineTerm = group.LoanDelayFineTerm;
                        groupInDb.InterestTermID = group.InterestTermID;
                        groupInDb.DateFormat = group.DateFormat;
                    }
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //TODO log the error
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            return View("Create", group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GroupName,GroupInstallmentAmount,InstallmentDayOfMonth,DefaultLoanInterest,DefaultNoOfInstallment,GroupInstallmentDelayFine,GroupInstallmentDelayFineType,GroupInstallmentDelayFinePeriod,LoanDelayFine,LoanDelayFineType,LoanDelayFinePeriod,InterestTermID,DateFormat")] Group group)
        {
            if (id != group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.GroupId))
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
            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }
    }
}

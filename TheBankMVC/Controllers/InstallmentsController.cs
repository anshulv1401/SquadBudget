using BudgetManager.BusinessComponents;
using BudgetManager.Data;
using BudgetManager.Enumerations;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManager.Controllers
{
    public class InstallmentsController : Controller
    {
        private ApplicationDbContext _context;
        public InstallmentComponent InstallmentComponent { get; }

        public InstallmentsController(ApplicationDbContext context)
        {
            _context = context;
            InstallmentComponent = new InstallmentComponent(_context);
        }

        // GET: Installments
        public async Task<IActionResult> Index()
        {
            var installmentChanged = InstallmentComponent.RefreshInstallments();

            var groups = _context.Groups.ToList();
            var installmentViewModelList = new List<InstallmentViewModel>();
            foreach (var group in groups)
            {
                var dueDate = InstallmentComponent.GetDueDate(group.InstallmentDayOfMonth);

                var installments = await _context.Installments.
                    Where(x =>
                        x.DueDate.Date <= dueDate.Date &&
                        x.InstallmentStatus != (int)Enumeration.InstallmentStatus.Paid &&
                        x.GroupId == group.GroupId).OrderBy(x => x.DueDate).ToListAsync();

                foreach (var installment in installments)
                {
                    var installmentViewModel = new InstallmentViewModel
                    {
                        GroupName = group.GroupName,
                        UserAccountName = _context.UserAccounts.Where(x => x.UserAccountId == installment.UserAccountId).First().UserAccountName,
                        Id = installment.Id,
                        EMIHeaderId = installment.EMIHeaderId,
                        EMIType = installment.EMIType,
                        InstallmentNo = installment.InstallmentNo,
                        DueDate = installment.DueDate.Date,
                        PrincipalAmount = installment.PrincipalAmount,
                        InterestAmount = installment.InterestAmount,
                        EMIAmount = installment.EMIAmount,
                        Difference = installment.Difference,
                        InstallmentStatus = installment.InstallmentStatus,
                        Fine = installment.Fine,
                        Opening = installment.Opening,
                        Closing = installment.Closing,
                        PaymentDate = installment.PaymentDate,
                    };

                    installmentViewModelList.Add(installmentViewModel);
                }
            }
            return View(installmentViewModelList);
        }

        // GET: Installments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installment = await _context.Installments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (installment == null)
            {
                return NotFound();
            }

            var installmentViewModel = new InstallmentViewModel
            {
                GroupName = _context.Groups.Where(x => x.GroupId == installment.GroupId).First().GroupName,
                UserAccountName = _context.UserAccounts.Where(x => x.UserAccountId == installment.UserAccountId).First().UserAccountName,
                Id = installment.Id,
                EMIHeaderId = installment.EMIHeaderId,
                EMIType = _context.EMIHeaders.Where(x => x.EMIHeaderId == installment.EMIHeaderId).First().EMIType,
                InstallmentNo = installment.InstallmentNo,
                DueDate = installment.DueDate.Date,
                PrincipalAmount = installment.PrincipalAmount,
                InterestAmount = installment.InterestAmount,
                EMIAmount = installment.EMIAmount,
                Difference = installment.Difference,
                InstallmentStatus = installment.InstallmentStatus,
                Fine = installment.Fine,
                Opening = installment.Opening,
                Closing = installment.Closing,
                PaymentDate = installment.PaymentDate
            };

            return View(installmentViewModel);
        }


        // GET: Installments/Paid/5
        public async Task<IActionResult> Paid(int id)
        {
            var installmentChanged = InstallmentComponent.RefreshInstallments();

            if (installmentChanged)
            {
                ViewBag.Message = "Installment got refresh, please try again";
                ViewBag.Type = "Installment got refresh, please try again";
                return RedirectToAction(nameof(Index));
            }

            if (id == 0)
            {
                return NotFound();
            }

            var installment = _context.Installments.Where(x => x.Id == id).First();
            await InstallmentComponent.PayInstallmentTransaction(installment);
            return RedirectToAction(nameof(Index));
        }

        // GET: Installments/RefreshInstallments
        public IActionResult RefreshInstallments()
        {
            InstallmentComponent.RefreshInstallments();
            return RedirectToAction(nameof(Index));
        }
    }
}

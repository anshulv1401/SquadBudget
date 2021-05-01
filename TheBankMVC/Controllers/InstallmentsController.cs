using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBankMVC.BusinessComponents;
using TheBankMVC.Data;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;

namespace TheBankMVC.Controllers
{
    public class InstallmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly InstallmentComponent installmentComponent;

        public InstallmentsController(ApplicationDbContext context)
        {
            _context = context;
            installmentComponent = new InstallmentComponent(_context);
        }

        // GET: Installments
        public async Task<IActionResult> Index()
        {
            installmentComponent.RefreshInstallmentStatus();

            var banks = _context.Bank.ToList();
            var installmentViewModelList = new List<InstallmentViewModel>();
            foreach (var bank in banks)
            {
                var dueDate = installmentComponent.GetDueDate(bank.InstallmentDayOfMonth);

                var installments = await _context.Installments.
                    Where(x => 
                        x.DueDate.Date <= dueDate.Date 
                        && x.InstallmentStatus != (int)Enumeration.InstallmentStatus.Paid
                        && x.BankId == bank.BankId).ToListAsync();

                foreach(var installment in installments)
                {
                    var installmentViewModel = new InstallmentViewModel();
                    installmentViewModel.BankName = bank.BankName;
                    installmentViewModel.UserAccountName = _context.UserAccount.Where(x => x.UserAccountId == installment.UserAccountId).First().UserAccountName;
                    installmentViewModel.Id = installment.Id;
                    installmentViewModel.EMIHeaderId = installment.EMIHeaderId;
                    installmentViewModel.EMIType = _context.EMIHeaders.Where(x => x.EMIHeaderId == installment.EMIHeaderId).First().EMIType;
                    installmentViewModel.InstallmentNo = installment.InstallmentNo;
                    installmentViewModel.DueDate = installment.DueDate.Date;
                    installmentViewModel.PrincipalAmount = installment.PrincipalAmount;
                    installmentViewModel.InterestAmount = installment.InterestAmount;
                    installmentViewModel.EMIAmount = installment.EMIAmount;
                    installmentViewModel.Difference = installment.Difference;
                    installmentViewModel.InstallmentStatus = installment.InstallmentStatus;
                    installmentViewModel.Fine = installment.Fine;

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

            return View(installment);
        }

        //// GET: Installments/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Installments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,EMIHeaderId,InstallmentNo,DueDate,PaymentDate,Opening,PrincipalAmount,InterestAmount,EMIAmount,Closing,Difference,InstallmentStatus")] Installment installment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(installment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(installment);
        //}

        //// GET: Installments/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var installment = await _context.Installments.FindAsync(id);
        //    if (installment == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(installment);
        //}

        //// POST: Installments/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,EMIHeaderId,InstallmentNo,DueDate,PaymentDate,Opening,PrincipalAmount,InterestAmount,EMIAmount,Closing,Difference,InstallmentStatus")] Installment installment)
        //{
        //    if (id != installment.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(installment);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InstallmentExists(installment.Id))
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
        //    return View(installment);
        //}

        // GET: Installments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var installment = await _context.Installments
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (installment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(installment);
        //}

        //// POST: Installments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var installment = await _context.Installments.FindAsync(id);
        //    _context.Installments.Remove(installment);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InstallmentExists(int id)
        //{
        //    return _context.Installments.Any(e => e.Id == id);
        //}
    }
}

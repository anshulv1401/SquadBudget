using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SquadMobile.Models;
using SquadMobile.Views;
using SquadMobile.Services.DAO;
using System.Collections.Generic;
using TheBankMVC.Models;

namespace SquadMobile.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public Command LoadTransactionsCommand { get; set; }

        public TransactionsViewModel()
        {
            Title = "Transactions";
            Transactions = new ObservableCollection<Transaction>();
            LoadTransactionsCommand = new Command(async () => await ExecuteLoadTransactionsCommand());
        }

        async Task ExecuteLoadTransactionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Transactions.Clear();
                var transactionsDAO = new TransactionsDAO();
                var transactions = await transactionsDAO.GetAsync(true);

                foreach(var transaction in transactions)
                {
                    Transactions.Add(transaction);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
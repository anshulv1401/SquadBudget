using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SquadMobile.Models;
using SquadMobile.Views;
using SquadMobile.ViewModels;

namespace SquadMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TransactionsPage : ContentPage
    {
        TransactionsViewModel viewModel;

        public TransactionsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TransactionsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as Item;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Transactions.Count == 0)
                viewModel.LoadTransactionsCommand.Execute(null);
        }
    }
}
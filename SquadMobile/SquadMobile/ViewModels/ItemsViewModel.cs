using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SquadMobile.Models;
using SquadMobile.Views;
using SquadMobile.Services.DAO;
using System.Collections.Generic;

namespace SquadMobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var userDAO = new UserAccountsDAO();
                var userAccounts = await userDAO.GetAsync(true);
                var items = new List<Item>();

                foreach(var account in userAccounts)
                {
                    Items.Add(new Item()
                    {
                        Id = account.UserAccountId.ToString(),
                        Text = account.UserAccountName,
                        Description = account.ShareSubmitted.ToString()
                    });
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
﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LifestyleEffectChecker.Helpers;
using LifestyleEffectChecker.Models;
using Xamarin.Forms;
using NewItemPage = LifestyleEffectChecker.Views.CreateEditViews.NewItemPage;

namespace LifestyleEffectChecker.ViewModels.Index
{
    public class JournalsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public ObservableRangeCollection<Journal> Journals { get; set; }

        public Command LoadItemsCommand { get; set; }
        public Command LoadJournalsCommand { get; set; }

        public JournalsViewModel()
        {
            Title = "Browse Journals";
            Items = new ObservableRangeCollection<Item>();
            Journals = new ObservableRangeCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });

        }

        async Task ExecuteLoadJournalsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var journals = await journalRepository.ReadAll();//await DataStore.GetItemsAsync(true);
                
                Journals = new ObservableRangeCollection<Journal>(journals);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load journals.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
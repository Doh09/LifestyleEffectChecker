﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Helpers;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository;
using LifestyleEffectChecker.Views.CreateEditViews;
using LifestyleEffectChecker.Views.DetailViews;
using Xamarin.Forms;
using JournalDeletePage = LifestyleEffectChecker.Views.DeleteViews.JournalDeletePage;

namespace LifestyleEffectChecker.ViewModels.Index
{
    public class JournalsViewModel : BaseViewModel
    {
        
        public ObservableRangeCollection<Item> Items { get; set; }
        public ObservableRangeCollection<Journal> Journals { get; set; }

        public Command LoadItemsCommand { get; set; }
        public Command LoadJournalsCommand { get; set; }

        IRepository<Journal> journalRepository = RepositoryFacade.GetJournalRepository();

        private static JournalsViewModel instance;

        public static JournalsViewModel GetInstance()
        {
            if (instance == null)
                instance = new JournalsViewModel();
            return instance;
        }

        private JournalsViewModel()
        {
            
            Title = "Browse Journals";
            Items = new ObservableRangeCollection<Item>();
            Journals = new ObservableRangeCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            MessagingCenter.Subscribe<NewJournalPage, Journal>(this, "AddJournal", async (obj, journal) =>
                {
                    await journalRepository.Create(journal);
                    await ExecuteLoadJournalsCommand();
                });

            MessagingCenter.Subscribe<NewJournalPage, Journal>(this, "EditJournal", async (obj, journal) =>
            {
                //It gets here when you click save in the item page.
                await journalRepository.Update(journal);
                await ExecuteLoadJournalsCommand();
            });

            MessagingCenter.Subscribe<JournalDeletePage, Journal>(this, "DeleteJournal", async (obj, journal) =>
            {
                //It gets here when you click save in the item page.
                await journalRepository.Delete(journal.ID);

                await ExecuteLoadJournalsCommand();
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
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LifestyleEffectChecker.Helpers;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.ViewModels.Index
{
    public class JournalsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public ObservableRangeCollection<Journal> Journals { get; set; }

        public Command LoadItemsCommand { get; set; }
        public Command LoadJournalsCommand { get; set; }

        IRepository<Journal> journalRepository = RepositoryFacade.GetJournalRepository();

        public JournalsViewModel()
        {
            Title = "Browse Journals";
            Items = new ObservableRangeCollection<Item>();
            Journals = new ObservableRangeCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalsCommand());
            
        MessagingCenter.Subscribe<NewJournalPage, Journal>(this, "AddJournal", async (obj, journal) =>
            {
                var _journal = journal as Journal;
                await journalRepository.Create(_journal);
                Journals.Add(_journal);
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
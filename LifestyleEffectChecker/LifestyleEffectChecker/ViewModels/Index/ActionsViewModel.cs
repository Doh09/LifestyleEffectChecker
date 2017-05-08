using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using LifestyleEffectChecker.Helpers;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository;
using LifestyleEffectChecker.Repository.Action;
using LifestyleEffectChecker.Views.CreateEditViews;
using LifestyleEffectChecker.Views.CreateEditViews.Action;
using Action = LifestyleEffectChecker.Models.Action.Action;
using Xamarin.Forms;

namespace LifestyleEffectChecker.ViewModels.Index
{
    public class ActionsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<JournalChild> JournalChildren { get; set; }
        public IRepository<Action> actionRepository = RepositoryFacade.GetActionRepository();
        public IRepository<Journal> journalRepository = RepositoryFacade.GetJournalRepository();
        public Journal ParentJournal { get; set; } = new Journal();

        public Command LoadActionsCommand { get; set; }

        public ActionsViewModel(Journal journalWithActions)
        {
            ParentJournal = journalWithActions;
            Title = "Browse Actions";
            JournalChildren = new ObservableRangeCollection<JournalChild>();
            LoadActionsCommand = new Command(async () => await ExecuteLoadActionsCommand());

            MessagingCenter.Subscribe<NewActionPage, JournalChild>(this, "AddAction", async (obj, journalChild) =>
            {
                await actionRepository.Create(journalChild as Action);
                JournalChildren.Add(journalChild);
                await ExecuteLoadActionsCommand();
            });

        }

        async Task ExecuteLoadActionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ParentJournal = await journalRepository.Read(ParentJournal.ID);
                var journalChildren = ParentJournal.JournalChildren;//await DataStore.GetItemsAsync(true);

                JournalChildren = new ObservableRangeCollection<JournalChild>(journalChildren);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load actions.",
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
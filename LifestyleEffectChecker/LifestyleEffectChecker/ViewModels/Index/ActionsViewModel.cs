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
        public ObservableRangeCollection<Action> Actions { get; set; }
        public IRepository<Action> actionRepository = RepositoryFacade.GetActionRepository();
        public IRepository<Journal> journalRepository = RepositoryFacade.GetJournalRepository();
        public Journal ParentJournal { get; set; } = new Journal();

        public Command LoadActionsCommand { get; set; }

        public ActionsViewModel(Journal journalWithActions)
        {
            ParentJournal = journalWithActions;
            Title = "Browse Actions";
            Actions = new ObservableRangeCollection<Action>();
            LoadActionsCommand = new Command(async () => await ExecuteLoadActionsCommand());

            MessagingCenter.Subscribe<NewActionPage, Item>(this, "AddAction", async (obj, action) =>
            {
                //var _action = action as Action;
                //Actions.Add(_action);
                //await DataStore.AddItemAsync(_action);
                //var _action = action as Item;
                //Items.Add(_item);
                //await DataStore.AddItemAsync(_action);

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
                var actions = ParentJournal.Actions;//await DataStore.GetItemsAsync(true);

                Actions = new ObservableRangeCollection<Action>(actions);

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
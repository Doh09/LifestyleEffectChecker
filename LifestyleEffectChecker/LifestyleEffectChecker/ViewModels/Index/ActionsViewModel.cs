using System;
using System.Threading.Tasks;
using System.Diagnostics;
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

        public Command LoadJournalsCommand { get; set; }

        public ActionsViewModel(Journal journalWithActions)
        {
            ParentJournal = journalWithActions;
            Title = "Browse Actions";
            Actions = new ObservableRangeCollection<Action>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            MessagingCenter.Subscribe<NewActionPage, Item>(this, "AddItem", async (obj, item) =>
            {
                //var _action = action as Action;
                //Actions.Add(_action);
                //await DataStore.AddItemAsync(_action);
                var _item = item as Item;
                //Items.Add(_item);
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
                Actions.Clear();
                var actions = await actionRepository.ReadAll();
                Actions.ReplaceRange(actions);
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
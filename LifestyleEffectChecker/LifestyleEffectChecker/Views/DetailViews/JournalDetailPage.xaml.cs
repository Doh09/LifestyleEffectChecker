using System;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Detail.Action;
using LifestyleEffectChecker.ViewModels.Detail.Effect;
using LifestyleEffectChecker.Views.CreateEditViews;
using LifestyleEffectChecker.Views.DetailViews.Action;
using LifestyleEffectChecker.Views.DetailViews.Effect;
using Xamarin.Forms;
using Action = LifestyleEffectChecker.Models.Action.Action;
using System.Threading.Tasks;
using LifestyleEffectChecker.Helpers;

namespace LifestyleEffectChecker.Views.DetailViews
{
    public partial class JournalDetailPage : ContentPage
    {
        JournalDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public JournalDetailPage()
        {
            InitializeComponent();
        }

        public JournalDetailPage(JournalDetailViewModel viewModel)
        {
            InitializeComponent();

            MessagingCenter.Subscribe<CreateEditViews.Action.NewActionPage, Models.Action.Action>(this, "AddAction", async (obj, action) =>
            {
                //It gets here when you click save in the item page.
                var actionRepo = Repository.RepositoryFacade.GetActionRepository();
                await actionRepo.Create(action);
                viewModel.Journal.JournalChildren.Add(action);
                await ExecuteLoadActionsCommand();
            });



            JournalChild mockAction = new Models.Action.Action();
            mockAction.Name = viewModel.Journal.Name + "_mockAction";
            viewModel.Journal.JournalChildren.Add(mockAction);
            JournalChild mockEffect = new Models.Effect.Effect();
            mockEffect.Name = viewModel.Journal.Name + "_mockEffect";
            viewModel.Journal.JournalChildren.Add(mockEffect);
           
            BindingContext = this.viewModel = viewModel;

        }

        async Task ExecuteLoadActionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var actionRepo = Repository.RepositoryFacade.GetActionRepository();
                var actions = await actionRepo.ReadAll();//await DataStore.GetItemsAsync(true);

                actions = new ObservableRangeCollection<Models.Action.Action>(actions);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load action.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void EditJournal_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewJournalPage(true, viewModel.Journal));
        }

        private async void DeleteJournal_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteViews.JournalDeletePage(new JournalDetailViewModel(viewModel.Journal)));
        }

        private async void JournalsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Models.Action.Action)
            {
                await Navigation.PushAsync(new ActionDetailPage(new ActionDetailViewModel(e.SelectedItem as Models.Action.Action)));
            }
            else if (e.SelectedItem is Models.Effect.Effect)
            {
                await Navigation.PushAsync(new EffectDetailPage(new EffectDetailViewModel(e.SelectedItem as Models.Effect.Effect)));
            }
            //
        }
        private async void Add_Button_Action(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateEditViews.Action.NewActionPage());
            int i = 5;
        }
        private async void Add_Button_Effekt(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailViews.ListReturn(viewModel));
        }
    }
}

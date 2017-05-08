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
            var mockAction = new Models.Action.Action();
            mockAction.Name = viewModel.Journal.Name + "_mockAction";
            viewModel.Journal.JournalChildren.Add(mockAction);
            var mockEffect = new Models.Effect.Effect();
            mockEffect.Name = viewModel.Journal.Name + "_mockEffect";
            viewModel.Journal.JournalChildren.Add(mockEffect);
           
            BindingContext = this.viewModel = viewModel;

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
    }
}

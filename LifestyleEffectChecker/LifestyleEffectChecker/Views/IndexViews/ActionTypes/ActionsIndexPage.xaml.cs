using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Index;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.IndexViews.ActionTypes
{
    public partial class ActionsIndexPage : ContentPage
    {
        ActionsViewModel viewModel;

        public ActionsIndexPage(Journal parentJournal)
        {
            InitializeComponent();
            viewModel = new ActionsViewModel(parentJournal);
            
            BindingContext = viewModel;
            viewModel.Actions.CollectionChanged += ListenToJournalChanges;
            viewModel.Actions.Add(new Models.Action.Action() { Name = "No actions", ID = -1, ActionParts = new List<Models.Action.ActionPart>() }); //Display this "Journal" if initial loading of journals failed
        }

        void ListenToJournalChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            ActionsListView.RefreshCommand.Execute(null);
            ActionsListView.ItemsSource = null;
            ActionsListView.ItemsSource = viewModel.Actions;
        }

        async void OnActionSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ActionsListView.ItemsSource = viewModel.Actions;
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new DetailViews.JournalDetailPage(new JournalDetailViewModel(journal)));

            // Manually deselect item
            ActionsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewJournalPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Actions.Count == 0) {
                  viewModel.LoadActionsCommand.Execute(null);
            }
        }
    }
}

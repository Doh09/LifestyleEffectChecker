using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;
using Action = LifestyleEffectChecker.Models.Action.Action;

namespace LifestyleEffectChecker.Views.IndexViews
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            viewModel = new ItemsViewModel();
            
            BindingContext = viewModel;
            viewModel.Journals.CollectionChanged += ListenToJournalChanges;
            viewModel.Journals.Add(new Journal() { Name = "No journals", ID = -1, ActionParts = new List<Action>() }); //Display this "Journal" if initial loading of journals failed
        }

        void ListenToJournalChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            ItemsListView.RefreshCommand.Execute(null);
            ItemsListView.ItemsSource = null;
            ItemsListView.ItemsSource = viewModel.Journals;
        }

        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemsListView.ItemsSource = viewModel.Journals;
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new DetailViews.ItemDetailPage(new ItemDetailViewModel(journal)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0) {
                  viewModel.LoadJournalsCommand.Execute(null);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Index;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.IndexViews
{
    public partial class JournalsIndexPage : ContentPage
    {
        JournalsViewModel viewModel;

        public JournalsIndexPage()
        {
            InitializeComponent();
            viewModel = new JournalsViewModel();
            
            BindingContext = viewModel;
            //viewModel.Journals.CollectionChanged += ListenToJournalChanges;
            viewModel.Journals.Add(new Journal() { Name = "No journals", ID = -1, JournalChildren = new List<PartInformation>() }); //Display this "Journal" if initial loading of journals failed
        }

        void ListenToJournalChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            //viewModel.LoadJournalsCommand.Execute(null);
            JournalsListView.RefreshCommand.Execute(null);
            JournalsListView.ItemsSource = null;
            JournalsListView.ItemsSource = viewModel.Journals;
        }

        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            JournalsListView.ItemsSource = viewModel.Journals;
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new DetailViews.JournalDetailPage(new JournalDetailViewModel(journal)));

            // Manually deselect item
            JournalsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewJournalPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0) {
                {
                    viewModel.LoadJournalsCommand.Execute(null);
                    ListenToJournalChanges(null, null);
                }
            }
        }
    }
}

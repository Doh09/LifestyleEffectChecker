﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Index;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.IndexViews.Action
{
    public partial class ActionsIndexPage : ContentPage
    {
        JournalsViewModel viewModel;

        public ActionsIndexPage()
        {
            InitializeComponent();
            viewModel = new JournalsViewModel();
            
            BindingContext = viewModel;
            viewModel.Journals.CollectionChanged += ListenToJournalChanges;
            viewModel.Journals.Add(new Journal() { Name = "No journals", ID = -1, ActionParts = new List<Models.Action.Action>() }); //Display this "Journal" if initial loading of journals failed
        }

        void ListenToJournalChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            ActionsListView.RefreshCommand.Execute(null);
            ActionsListView.ItemsSource = null;
            ActionsListView.ItemsSource = viewModel.Journals;
        }

        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ActionsListView.ItemsSource = viewModel.Journals;
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new DetailViews.JournalDetailPage(new JournalDetailViewModel(journal)));

            // Manually deselect item
            ActionsListView.SelectedItem = null;
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
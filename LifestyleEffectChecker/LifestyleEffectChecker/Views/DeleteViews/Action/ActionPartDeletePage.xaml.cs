﻿using System;
using System.ComponentModel;
using LifestyleEffectChecker.ViewModels.Detail;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DeleteViews.Action
{
    public partial class ActionPartDeletePage : ContentPage
    {
        private JournalDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ActionPartDeletePage()
        {//TODO this class is not implemented yet.
            InitializeComponent();
        }

        public ActionPartDeletePage(JournalDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }


        private async void ConfirmDeletion_OnClicked(object sender, EventArgs e)
        {
            if (DeletionConfirmationEntry.Text.Equals(viewModel.Journal.Name))
            {
                //Delete journal
                var answeredYes = await DisplayAlert("Confirmation", "Are you sure you want to delete the journal?",
                    "Yes", "No");
                if (answeredYes)
                {
                    MessagingCenter.Send(this, "DeleteJournal", viewModel.Journal);
                    await Navigation.PopToRootAsync();
                }
                //Debug.WriteLine("Answer: " + answer);
            }
            else
            {
                MatchingNames.Text = "ERROR - names don't match";
                MatchingNames.TextColor = Color.Red;
                //Write error message? DisplayAlert seems to cause a bug if used, maybe use Toast? DisplayAlert seems to interfere with answeredYes, but test, am not sure.
            }
        }

        private void DeletionConfirmationEntry_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (viewModel == null)
            {
                //no viewmodel.
                MatchingNames.Text = "ERROR - no journal found to delete - CODE 1";
                MatchingNames.TextColor = Color.Red;
                return;
            }
            if (viewModel.Journal == null)
            {
                //No journal.
                MatchingNames.Text = "ERROR - no journal found to delete - CODE 2";
                MatchingNames.TextColor = Color.Red;
                return;
            }
            bool isMatching = DeletionConfirmationEntry.Text.Equals(viewModel.Journal.Name);
            if (isMatching)
            {
                MatchingNames.Text = "Matching";
                MatchingNames.TextColor = Color.Lime;
            }
            else
            {
                MatchingNames.Text = "Not matching";
                MatchingNames.TextColor = Color.Red;
            }
        }

    }
}

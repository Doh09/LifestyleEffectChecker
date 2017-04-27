using System;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews
{
    public partial class JournalDeletePage : ContentPage
    {
        private JournalDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public JournalDeletePage()
        {
            InitializeComponent();
        }

        public JournalDeletePage(JournalDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        private async void EditJournal_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewJournalPage(true, viewModel.Journal));
        }

        private async void ConfirmDeletion_OnClicked(object sender, EventArgs e)
        {
            if (DeletionConfirmationEntry.Text.Equals(viewModel.Journal.Name))
            {
                //Delete journal
                var answeredYes = await DisplayAlert("Confirmation", "Are you sure you want to delete the journal?", "Yes", "No");
                if (answeredYes)
                {
                    MessagingCenter.Send(this, "DeleteJournal", viewModel.Journal);
                    await Navigation.PopToRootAsync();
                }
                //Debug.WriteLine("Answer: " + answer);
            }
            else
            {
                DisplayAlert("Error", "Name written does not match journal name", "OK");
            }
        }
    }
}

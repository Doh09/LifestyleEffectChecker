using System;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

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

            BindingContext = this.viewModel = viewModel;
        }

        private async void EditJournal_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewJournalPage(true, viewModel.Journal));
        }

        private async void DeleteJournal_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JournalDeletePage(new JournalDetailViewModel(viewModel.Journal)));
        }
    }
}

using System;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Detail.Action;
using LifestyleEffectChecker.Views.CreateEditViews;
using LifestyleEffectChecker.Views.DetailViews.Action;
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
            var mockPartInformation1 = new PartInformation();
            mockPartInformation1.Name = viewModel.Journal.Name + "_mockAction";
            viewModel.Journal.JournalChildren.Add(mockPartInformation1);
            var mockPartInformation2 = new PartInformation();
            mockPartInformation2.Name = viewModel.Journal.Name + "_mockEffect";
            viewModel.Journal.JournalChildren.Add(mockPartInformation2);

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
                await Navigation.PushAsync(new PartInformationDetailPage(new PartInformationDetailViewModel(e.SelectedItem as PartInformation)));
        }
    }
}

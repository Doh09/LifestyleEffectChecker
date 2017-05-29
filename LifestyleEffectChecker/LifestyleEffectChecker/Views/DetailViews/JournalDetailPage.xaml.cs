using System;
using System.Collections.Specialized;
using System.Diagnostics;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Index;
using LifestyleEffectChecker.Views.CreateEditViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews
{
    public partial class JournalDetailPage : ContentPage
    {
        JournalDetailViewModel viewModel;
        PartInformationsViewModel childrenViewModel = new PartInformationsViewModel(null);

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
            childrenViewModel.ParentJournal = viewModel.Journal;
            childrenViewModel.LoadPartInformationsCommand.Execute(null);

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

        private async void Btn_AddPartInformation_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPartInformationPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (JournalsPartInformationsListView.RefreshCommand == null)
            {
                JournalsPartInformationsListView.RefreshCommand = childrenViewModel.LoadPartInformationsCommand;
            }
            ListenToPartInformationsChanges(null, null);
        }

        void ListenToPartInformationsChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            //viewModel.LoadJournalsCommand.Execute(null);
            if (JournalsPartInformationsListView != null)
            {

                    JournalsPartInformationsListView.RefreshCommand.Execute(null);
                    JournalsPartInformationsListView.ItemsSource = null;
                    JournalsPartInformationsListView.ItemsSource = viewModel.Journal.JournalChildren;
                    AmountOfJournalEntries.Text = "Journal entries made: " + viewModel.Journal.JournalChildren.Count;

            }
        }
    }
}

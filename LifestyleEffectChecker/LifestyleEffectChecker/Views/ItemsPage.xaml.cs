using System;

using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels;

using Xamarin.Forms;

namespace LifestyleEffectChecker.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            viewModel = new ItemsViewModel();
            BindingContext = viewModel;
            ItemsListView.ItemsSource = viewModel.Journals;
            ItemsListView.ItemsSource = viewModel.Journals;
        }

        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(journal)));

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

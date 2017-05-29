using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Index;
using LifestyleEffectChecker.Views.CreateEditViews;
using System.Numerics;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.IndexViews
{
    public partial class JournalsIndexPage : ContentPage
    {
        JournalsViewModel viewModel;
        private MotionSensorType _sensorType;
        Random random = new Random();
        public JournalsIndexPage()
        {
            InitializeComponent();
            viewModel = JournalsViewModel.GetInstance();
            
            BindingContext = viewModel;
            //viewModel.Journals.CollectionChanged += ListenToJournalChanges;
            viewModel.Journals.Add(new Journal() { Name = "No journals", ID = -1, JournalChildren = new List<PartInformation>() }); //Display this "Journal" if initial loading of journals failed

            SetUpColorMotion();
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

        private void SetUpColorMotion()
        {
            //CrossDeviceMotion.Current.Start(_sensorType, MotionSensorDelay.Default);
            _sensorType = MotionSensorType.Accelerometer;

            CrossDeviceMotion.Current.SensorValueChanged += (s, a) =>
            {

                if (a.SensorType == _sensorType)
                {
                    string hexColor = String.Format("#{0:X6}", random.Next(0x1000000));
                    App.Current.Resources["Primary"] = hexColor;
                    //;

                }
            };
        }

        private void FunkyColours_OnClicked(object sender, EventArgs e)
        {
            if (CrossDeviceMotion.Current.IsActive(_sensorType))
            {
                CrossDeviceMotion.Current.Stop(_sensorType);
            }
            else
            {
                CrossDeviceMotion.Current.Start(_sensorType, MotionSensorDelay.Default);
            }
        }
    }
}

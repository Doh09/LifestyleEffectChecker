using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LifestyleEffectChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1Tab1 : ContentPage
    {
        ObservableCollection<string> _coordinates; //Bør nok være "Position" og ikke "string"
        bool _isListening = false;
        IGeolocator locator = CrossGeolocator.Current;

        public Page1Tab1()
        {
            InitializeComponent();

            _coordinates = new ObservableCollection<string>();

            locator.PositionChanged += (object sender, PositionEventArgs e) => OnPositionChanged(e.Position);
            //Refresh();
        }

        private void Refresh()
        {
            lstCoordinates.ItemsSource = null;
            lstCoordinates.ItemsSource = _coordinates;
        }

        public void OnPositionChanged(Position p)
        {
            _coordinates.Add(PositionAsString(p));
            //Refresh();
        }

        private string PositionAsString(Position p)
        {
            return string.Format("Lat: {0} Long: {1} Alt: {2}",
              p.Latitude, p.Longitude, p.Altitude);
        }

        private async void BtnGPSListening_OnClicked(object sender, EventArgs e)
        {
            _isListening = !_isListening;
            btnGetGPS.IsEnabled = !_isListening;
            btnGPSListening.Text = _isListening ? "Stop listening" : "Start listening";
            if (_isListening)
                await locator.StartListeningAsync(3000, 5);
            else
                await locator.StopListeningAsync();

        }

        private async void BtnGetGPS_OnClicked(object sender, EventArgs e)
        {
            locator.DesiredAccuracy = 10;

            var position = await locator.GetPositionAsync(5000);

            if (position == null)
            {
                await DisplayAlert("Info", "NO GPS signal received!", "ok");
                return;
            }

            _coordinates.Clear();
            object o = position;
            _coordinates.Add(PositionAsString(position));
            
            Refresh();
        }
    }
}

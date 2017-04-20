using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Google.Apis.Services;
using System.Collections.ObjectModel;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace LifestyleEffectChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleMapsPage : TabbedPage
    {
        ObservableCollection<string> _coordinates; //Bør nok være "Position" og ikke "string"
        bool _isListening = false;
        IGeolocator locator = CrossGeolocator.Current;
        public GoogleMapsPage()
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
            locator.DesiredAccuracy = 2;

            var position = await locator.GetPositionAsync(5000);

            if (position == null)
            {
                await DisplayAlert("Info", "NO GPS signal received!", "ok");
                return;
            }

            Position fstPosition = new Position();
            Position scndPosition = new Position();
            if (_coordinates.Count == 0)
            {
                fstPosition = position;
                _coordinates.Add(PositionAsString(fstPosition));
            }
            else
            {
                scndPosition = position;
                _coordinates.Add(PositionAsString(scndPosition));
                FindSurfaceDistanceBetweenPositions(fstPosition, scndPosition);
            }

            Refresh();
        }
        public void FindSurfaceDistanceBetweenPositions(Position fstPosition, Position scndPosition)
        {
            var R = 6371000;
            var φ1 = (Math.PI / 180) * fstPosition.Latitude;
            var φ2 = (Math.PI / 180) * scndPosition.Latitude;
            var Δφ = (scndPosition.Latitude - fstPosition.Latitude) * (Math.PI / 180);
            var Δλ = (scndPosition.Longitude - fstPosition.Longitude) * (Math.PI / 180);

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
        Math.Cos(φ1) * Math.Cos(φ2) *
        Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = R * c;

            //var correctDistance = R-d;
            _coordinates.Clear();
            _coordinates.Add(d + "");
        }
    }
}

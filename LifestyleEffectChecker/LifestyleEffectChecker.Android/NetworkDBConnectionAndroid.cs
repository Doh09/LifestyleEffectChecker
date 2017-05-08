using Android.App;
using Android.Net;
using Android.OS;
using Android.Widget;
using Android.Util;

namespace LifestyleEffectChecker.Droid
{
    public class NetworkDBConnectionAndroid : Activity
    {
        public bool IsOnline()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo.IsConnected;
        }
    }
}
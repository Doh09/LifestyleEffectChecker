using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Droid;
using SQLite.Net;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(DBConnectionAndroid))]
namespace LifestyleEffectChecker.Droid
{
    class DBConnectionAndroid : IDBConnection
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "PersonsDB.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            //var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}
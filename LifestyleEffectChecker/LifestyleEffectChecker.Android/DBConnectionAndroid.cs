using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LifestyleEffectChecker.Connection;
using SQLite.Net;
using Environment = System.Environment;

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
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}
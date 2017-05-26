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
using Xamarin.Forms;
using LifestyleEffectChecker.Droid.Camera;
using LifestyleEffectChecker._Camera;

[assembly: Dependency(typeof(CameraHandlerAndroid))]

namespace LifestyleEffectChecker.Droid.Camera
{
    public class CameraHandlerAndroid : ICameraHandler
    {
        private static event PhotoTaken PhotoTakenEvent;

        public void AddPhotoTakenEventhandler(PhotoTaken pt)
        {
            PhotoTakenEvent += pt;
        }
        public void TakePhoto()
        {
            Activity activity = (Activity)Forms.Context;
            Intent intent = new Intent();
            intent.SetClass(activity, typeof(CameraMediator));
            activity.StartActivity(intent);
        }
        public static void HereIsThePic(String filename)
        {
            PhotoTakenEvent.Invoke(filename);
        }
    }
}

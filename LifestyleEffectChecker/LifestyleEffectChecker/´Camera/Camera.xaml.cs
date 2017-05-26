using LifestyleEffectChecker._Camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LifestyleEffectChecker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Camera : ContentPage
    {
        ICameraHandler _cameraHandler;
        List<string> fileNames = new List<string>();

        public Camera()
        {
            InitializeComponent();
         //   TakePicture.Clicked += (sender, args) => { TakePicture(); };
         //   NextPicture.Clicked += (sender, args) => { NextPicture(); };
            _cameraHandler = DependencyService.Get<ICameraHandler>();

        }

        private void takePicture()
        {
            _cameraHandler.AddPhotoTakenEventhandler(PhotoReceived);
            _cameraHandler.TakePhoto();
        }
        int i;
        private void nextPicture()
        {
            if (fileNames.Count != 0)
            {
            string fileName = fileNames[i];
            image.Source = fileName;
            lblFileName.Text = fileName;
            i++;
            if (i > fileNames.Count - 1)
            {
                i = 0;
            }
            }
        }


        public void PhotoReceived(string fileName)
        {
            fileNames.Add(fileName);
        }
    }
}

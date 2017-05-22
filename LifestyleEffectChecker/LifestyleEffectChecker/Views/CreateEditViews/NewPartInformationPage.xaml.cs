using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Discovery;
using LifestyleEffectChecker.Models;
using Xamarin.Forms;
using PartInformation = LifestyleEffectChecker.Models.Action.PartInformation;

namespace LifestyleEffectChecker.Views.CreateEditViews.Action
{
    public partial class NewPartInformationPage : ContentPage
    {
        public PartInformation PartInformation { get; set; }

        public NewPartInformationPage()
        {
            InitializeComponent();

            Dictionary<string, MeasuringMethod> measuringMethods = new Dictionary<string, MeasuringMethod>
        {
            { MeasuringMethod.Text.ToString(), MeasuringMethod.Text }, //0
            { MeasuringMethod.Decimal.ToString(), MeasuringMethod.Decimal }, //1
            { MeasuringMethod.GPSLocation.ToString(), MeasuringMethod.GPSLocation }, //2
            { MeasuringMethod.Number.ToString(), MeasuringMethod.Number }, //3
            { MeasuringMethod.Picture.ToString(), MeasuringMethod.Picture }, //4
            { MeasuringMethod.Slider.ToString(), MeasuringMethod.Slider }, //5
        };

            foreach (string colorName in measuringMethods.Keys)
            {
                measureMethodPicker.Items.Add(colorName);
            }

            measureMethodPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (measureMethodPicker.SelectedIndex != -1)
                {
                    MeasuringMethod mm = measuringMethods.Values.ToList()[measureMethodPicker.SelectedIndex];
                    //string methodName = measureMethodPicker.Items[measureMethodPicker.SelectedIndex];
                    PartInformation.MeasuringMethod = mm;
                    pickedMeasure.Text = PartInformation.MeasuringMethod.ToString();
                    pickedMeasureSL.Children.Clear();
                    pickedMeasureSL.Children.Add(getMeasuringUI(mm));
                }
            };

            PartInformation = new PartInformation()
            {
                ID = -1,
                Name = "Name of part information",
                parentID = -1
            };

            BindingContext = this;
        }

        StackLayout getMeasuringUI(MeasuringMethod mm)
        {
            var toReturn = new StackLayout();
            switch (mm)
            {
                case MeasuringMethod.Text:
                    var textEntry = new Entry();
                    textEntry.Placeholder = "Write text here";
                    toReturn.Children.Add(textEntry);
                    break;
                case MeasuringMethod.Decimal:
                    var decimalEntry = new Entry();
                    decimalEntry.Keyboard = Keyboard.Numeric;
                    decimalEntry.Placeholder = "Enter decimal number";
                    toReturn.Children.Add(decimalEntry);
                    break;
                case MeasuringMethod.Slider:
                    var slider = new Slider(0,10,5);
                    toReturn.Children.Add(slider);
                    break;
                case MeasuringMethod.Picture:
                    var picture = new Slider(0, 10, 5);
                    toReturn.Children.Add(picture);
                    break;
                case MeasuringMethod.GPSLocation:
                    var gPSLocation = new Slider(0, 10, 5);
                    toReturn.Children.Add(gPSLocation);
                    break;
                case MeasuringMethod.Number:
                    var NumberEntry = new Entry();
                    NumberEntry.Keyboard = Keyboard.Numeric;
                    NumberEntry.Placeholder = "Enter number without decimals";
                    toReturn.Children.Add(NumberEntry);
                    break;

            }
            return toReturn;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
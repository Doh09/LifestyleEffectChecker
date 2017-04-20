using System;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Effect;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.CreateEditViews.Effect
{
    public partial class NewEffectParameterPage : ContentPage
    {
        public EffectParameter EffectParameter { get; set; }

        public NewEffectParameterPage()
        {
            InitializeComponent();

            EffectParameter = new EffectParameter()
            {
                ID = -1,
                Name = "Name of the effect parameter",
                MeasuringMethod = MeasuringMethod.Text
            };
            
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
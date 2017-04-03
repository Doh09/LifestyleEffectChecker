using System;
using System.Collections.Generic;
using LifestyleEffectChecker.Models.Effect;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.CreateEditViews.Effect
{
    public partial class NewEffectPage : ContentPage
    {
        public Models.Effect.Effect Effect { get; set; }

        public NewEffectPage()
        {
            InitializeComponent();

            Effect = new Models.Effect.Effect()
            {
                ID = -1,
                Name = "Name of effect",
                EffectParameters = new List<EffectParameter>()
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
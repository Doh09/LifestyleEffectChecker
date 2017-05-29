using System;
using System.Collections.Generic;
using LifestyleEffectChecker.Models.Action;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.CreateEditViews.Action
{
    public partial class NewActionPartPage : ContentPage
    {
        public ActionPart ActionPart { get; set; }

        public NewActionPartPage()
        {
            InitializeComponent();

            ActionPart = new ActionPart()
            {
                ID = -1,
                Name = "Name of action part",
                parentID = -1,
                PartInformations = new List<PartInformation>()
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
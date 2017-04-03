using System;
using System.Collections.Generic;
using LifestyleEffectChecker.Models.Action;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.CreateEditViews.Action
{
    public partial class NewActionPage : ContentPage
    {
        public Models.Action.Action Action { get; set; }

        public NewActionPage()
        {
            InitializeComponent();

            Action = new Models.Action.Action()
            {
                ID = -1,
                Name = "Action name",
                parentID = -1,
                ActionParts = new List<ActionPart>()
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
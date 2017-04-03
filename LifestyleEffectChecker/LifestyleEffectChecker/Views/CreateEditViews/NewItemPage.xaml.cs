using System;
using System.Collections.Generic;
using LifestyleEffectChecker.Models;
using Xamarin.Forms;
using Action = LifestyleEffectChecker.Models.Action.Action;

namespace LifestyleEffectChecker.Views.CRUDViews
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        public Journal Journal { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is a nice description"
            };
            Journal = new Journal()
            {
                ID = -1,
                Name = "Journal name",
                ActionParts = new List<Action>()
            };

            

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
using System;
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

            PartInformation = new PartInformation()
            {
                ID = -1,
                Name = "Name of part information",
                parentID = -1
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
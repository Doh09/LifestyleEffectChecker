using System;
using System.Collections.Generic;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.CreateEditViews
{
    public partial class NewJournalPage : ContentPage
    {
        public bool Edit { get; set; } = false;
        public Item Item { get; set; }
        public Journal Journal { get; set; }

        public NewJournalPage(bool edit = false, Journal journal = null)
        {
            Journal = journal;
            Edit = edit;
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is a nice description"
            };
            if (!edit)
                Journal = new Journal()
                {
                    ID = -1,
                    Name = "Journal name",
                    JournalChildren = new List<PartInformation>()
                };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (!Edit)
            {
                MessagingCenter.Send(this, "AddJournal", Journal);
            }
            else if (Edit)
            {
                MessagingCenter.Send(this, "EditJournal", Journal);
            }
            await Navigation.PopToRootAsync();
        }
    }
}
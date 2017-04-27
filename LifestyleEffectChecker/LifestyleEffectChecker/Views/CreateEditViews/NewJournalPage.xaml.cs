using System;
using System.Collections.Generic;
using LifestyleEffectChecker.Models;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.CreateEditViews
{
    public partial class NewJournalPage : ContentPage
    {
        public bool Edit { get; set; }
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
                    Actions = new List<Models.Action.Action>()
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
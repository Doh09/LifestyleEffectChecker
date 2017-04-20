﻿using System;
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

        public NewJournalPage()
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
                Actions = new List<Models.Action.Action>()
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            MessagingCenter.Send(this, "AddJournal", Journal);
            await Navigation.PopToRootAsync();
        }
    }
}
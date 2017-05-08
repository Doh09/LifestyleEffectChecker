using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels.Index;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LifestyleEffectChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        JournalsViewModel jvm;
        List<Journal> List;
        List<DataHolder> DHs = new List<DataHolder>();
        public SearchPage()
        {
            InitializeComponent();
            #region

            jvm = new JournalsViewModel();
            jvm.LoadJournalsCommand.Execute(null);
            List = jvm.Journals.ToList();
            foreach (var item in List)
            {
                DHs.Add(new DataHolder {Name = item.Name, Objert = item,Type = CheckType(item) });
            }
            //viewModel.Journals.CollectionChanged += ListenToJournalChanges;
            List.Add(new Journal() { Name = "No journals", ID = -1, Actions = new List<Models.Action.Action>() }); //Display this "Journal" if initial loading of journals failed
            #endregion
            Refresh();
        }
        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ListView.ItemsSource = List;
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new DetailViews.JournalDetailPage(new ViewModels.Detail.JournalDetailViewModel(journal)));

            // Manually deselect item
            ListView.SelectedItem = null;
        }

        private void Refresh()
        {
            ListView.ItemsSource = null;
            ListView.ItemsSource = DHs;
        }

        // Buttons
        #region
        public enum searchTags
        {
            All, Journal, Action, ActionPart, PartInformation, Error
        }
        private void Search_Button_OpenListOfTypes(object sender, EventArgs e)
        {
            if (ListView_ListOfTypes.HeightRequest == 300) {
                ListView_ListOfTypes.HeightRequest = 0;
            } else {
                ListView_ListOfTypes.HeightRequest = 300;
            }
            if (ListView_ListOfTypes.ItemsSource == null)
            {
                List<string> L = new List<string>() { };
                L = Enum.GetNames(typeof(searchTags)).ToList();
                ListView_ListOfTypes.ItemsSource = null;
                ListView_ListOfTypes.ItemsSource = L;
                Refresh();
            }
            else
            {
                ListView_ListOfTypes.ItemsSource = null;
                Refresh();
            }
        }
        private void Search_Button_Clicked(object sender, EventArgs e)
        {
           // jvm.LoadJournalsCommand.Execute(null);
            List = jvm.Journals.ToList();
            List.Add(new Journal() { Name = "No journals", ID = -1, Actions = new List<Models.Action.Action>() }); //Display this "Journal" if initial loading of journals failed
            DHs.Clear();
            foreach (var journal in List)
            {
                Search(journal);
            }
            
            BindingContext = List;
            Refresh();
        }
        #endregion
        private void Search(Journal journal)
        {
            var KeyWord = EntryKeyWord.Text;
            List<Object> LO = Find(KeyWord, journal);
            
            if (LO != null)
            {
                foreach (Object O in LO)
                {
                    if (O != null)
                    {
                        DataHolder DH = new DataHolder { Name = "blah", Objert = O };
                        DH.Type = CheckType(O);
                        if (DH.Type == searchTags.Journal)
                        {
                            Journal J = (Journal)O;
                            DH.Name = J.Name;
                        }
                        if (DH.Type == searchTags.Action)
                        {
                            Action J = (Action)O;
                            DH.Name = "Why Do Actions Not have Names";//J.Name;
                        }
                        if (DH.Type == searchTags.ActionPart)
                        {
                            Models.Action.ActionPart J = (Models.Action.ActionPart)O;
                            DH.Name = J.Name;
                        }
                        if (DH.Type == searchTags.PartInformation)
                        {
                            Models.Action.PartInformation J = (Models.Action.PartInformation)O;
                            DH.Name = J.Name;
                        }
                        DHs.Add(DH);
                    }
                }
            }
        }
        private static searchTags CheckType(Object O)
        {
            if (O.GetType() == typeof(Journal))
            {
               return searchTags.Journal;
            }
            else
            {
               // Console.WriteLine("not a jounal");
            }
            // same with the other values
            #region
            if (O.GetType() == typeof(Models.Action.Action))
            {
                return searchTags.Action;
            }
            else
            {
                // Console.WriteLine("not a Action");
            }
            if (O.GetType() == typeof(Models.Action.ActionPart))
            {
                return searchTags.ActionPart;
            }
            else
            {
                // Console.WriteLine("not a ActionPart");
            }
            if (O.GetType() == typeof(Models.Action.PartInformation))
            {
                return searchTags.PartInformation;
            }
            else
            {
                //  Console.WriteLine("not a PartInformation");
            }
            #endregion
            return searchTags.Error;
        }
        public static List<Object> Find(string Key, Journal journal)
        {
            List<Object> LO = new List<object>();
            if (journal.Name == Key) { LO.Add(journal); }
            if (Key == "All") { LO.Add(journal); }
            foreach (var action in journal.Actions)
            {
                if (action.Name == Key) { LO.Add(action); }
                if (Key == "All") { LO.Add(action); }
                foreach (var actionParts in action.ActionParts)
                {
                    if (actionParts.Name == Key) { LO.Add(actionParts); }
                    if (Key == "All") { LO.Add(actionParts); }
                    foreach (var partInformations in actionParts.PartInformations)
                    {
                        if (partInformations.Name == Key) { LO.Add(partInformations); }
                        if (Key == "All") { LO.Add(partInformations); }
                    }
                }
            }
            return LO;
        }
    }
    internal class DataHolder
    {
        public SearchPage.searchTags Type;
        public Object Objert;
        public string Name;

        public override string ToString()
        {
            return $"{Name}: \n{Type}";
        }
    }
}

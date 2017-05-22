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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        JournalsViewModel jvm;
        List<Journal> List;
        List<DataHolder> DHs = new List<DataHolder>();
        public enum searchTags
        {
            All, Journal, Action, ActionPart, PartInformation, Effect, EffectParameter, Error

        }
        public searchTags CurrentSeatchTag { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            CurrentSeatchTag = searchTags.All;
            ListView_ListOfTypes.ItemTapped += (sender, e) =>
            {
                string s = (string)e.Item;
                CurrentSeatchTag = (searchTags)Enum.Parse(typeof(searchTags), s, true);
            };

            #region

            jvm = new JournalsViewModel();
            jvm.LoadJournalsCommand.Execute(null);
            List = jvm.Journals.ToList();
            foreach (var item in List)
            {
                DHs.Add(new DataHolder { Name = item.Name, Objert = item, Type = CheckType(item) });
            }

           // List[3].JournalChildren.Add(new Models.Action.Action() { ID = 1, Name = "MutionAction" });
           // List[3].JournalChildren.Add(new Models.Effect.Effect() { ID = 1, Name = "MutionEffect" });

            #endregion
            Refresh();
        }
        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ListView_List_of_results.ItemsSource = List;

            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new DetailViews.JournalDetailPage(new ViewModels.Detail.JournalDetailViewModel(journal)));

            // Manually deselect item
            ListView_List_of_results.SelectedItem = null;
        }

        private void Refresh()
        {
            ListView_List_of_results.ItemsSource = null;
            ListView_List_of_results.ItemsSource = DHs;
            SearchResultsListView.ItemsSource = null;
            SearchResultsListView.ItemsSource = DHs;
        }

        // Buttons
        #region

        private void Search_Button_OpenListOfTypes(object sender, EventArgs e)
        {
            if (ListView_ListOfTypes.HeightRequest == 300)
            {
                ListView_ListOfTypes.HeightRequest = 0;
            }
            else
            {
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
                            Models.Action.Action J = (Models.Action.Action)O;
                            DH.Name = J.Name;
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
                        if (DH.Type == searchTags.Effect)
                        {
                            Models.Effect.Effect J = (Models.Effect.Effect)O;
                            DH.Name = J.Name;
                        }
                        if (DH.Type == searchTags.EffectParameter)
                        {
                            Models.Effect.EffectParameter J = (Models.Effect.EffectParameter)O;
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
            #region
            if (O.GetType() == typeof(Models.Action.Action))
            {
                return searchTags.Action;
            }
            if (O.GetType() == typeof(Models.Action.ActionPart))
            {
                return searchTags.ActionPart;
            }
            if (O.GetType() == typeof(Models.Action.PartInformation))
            {
                return searchTags.PartInformation;
            }
            if (O.GetType() == typeof(Models.Effect.Effect))
            {
                return searchTags.Effect;
            }
            if (O.GetType() == typeof(Models.Effect.EffectParameter))
            {
                return searchTags.EffectParameter;
            }
            #endregion
            return searchTags.Error;
        }
        public List<Object> Find(string Key, Journal journal)
        {
            List<Object> LO = new List<object>();
            #region
            if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.Journal)
            {
                if (journal.Name == Key || Key == "" + journal.TimeStamp || "" + journal.ID == Key || Key == "")
                { LO.Add(journal); }
            }
            #endregion
            foreach (var journalChild in journal.JournalChildren)
            {
                if (journalChild.GetType() == typeof(Models.Action.Action))
                {
                    var action = journalChild as Models.Action.Action;
                    #region

                    if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.Action)
                    {
                        if (action.Name == Key || Key == "" + action.TimeStamp || "" + action.ID == Key || Key == "")
                        { LO.Add(action); }
                    }


                    foreach (var actionParts in action.ActionParts)
                    {
                        #region
                        if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.ActionPart)
                        {
                            if (actionParts.Name == Key || Key == "" + actionParts.TimeStamp || "" + actionParts.ID == Key || Key == "")
                            { LO.Add(actionParts); }
                        }
                        #endregion
                        foreach (var partInformations in actionParts.PartInformations)
                        {
                            #region
                            if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.PartInformation)
                            {
                                if (partInformations.Name == Key || Key == "" + partInformations.TimeStamp || "" + partInformations.ID == Key || Key == "")
                                { LO.Add(partInformations); }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    var Effect = journalChild as Models.Effect.Effect;
                    #region
                    if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.Effect)
                    {
                        if (Effect.Name == Key || Key == "" + Effect.TimeStamp || "" + Effect.ID == Key || Key == "")
                        { LO.Add(Effect); }
                    }
                    foreach (var effectParameter in Effect.EffectParameters)
                    {

                        if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.EffectParameter)
                        {
                            if (effectParameter.Name == Key || Key == "" + effectParameter.TimeStamp || "" + effectParameter.ID == Key || Key == "")
                            { LO.Add(effectParameter); }
                        }

                    }
                    #endregion
                }
            }
            return LO;
        }

        async void SearchResult(object sender, SelectedItemChangedEventArgs args)
        {
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

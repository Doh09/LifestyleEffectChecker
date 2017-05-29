using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.ViewModels.Index;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LifestyleEffectChecker.ViewModels.Detail;

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
            All, Journal, PartInformation, Error

        }
        public searchTags CurrentSeatchTag { get; set; }
        public async void OnListClicked_GoTo_JournalDetailPage(Object Obj)
        {


            if (Obj.GetType() == typeof(DataHolder))
            {
                DataHolder DH = Obj as DataHolder;

                searchTags ST = CheckType(DH.Objert);
                if (ST == searchTags.Journal)
                {
                    Journal J = (Journal)DH.Objert;
                    await Navigation.PushAsync(new DetailViews.JournalDetailPage(new JournalDetailViewModel(J)));
                }
                if (ST == searchTags.PartInformation)
                {
                    PartInformation PartJ = (PartInformation)DH.Objert;
                    await Navigation.PushAsync(new DetailViews.PartInformationDetailPage(new ViewModels.Detail.PartInformationDetailViewModel(PartJ)));
                }
            }
        }
        public SearchPage()
        {
            InitializeComponent();

            ListView_List_of_results.ItemTapped += (sender, e) => {

                OnListClicked_GoTo_JournalDetailPage((Object)e.Item);

            };

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
            //viewModel.Journals.CollectionChanged += ListenToJournalChanges;

            //List[3].JournalChildren.Add(new Models.Action.PartInformation() { ID = 1, Name = "Mution" });


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
        }

        #region

        private void Search_Button_OpenListOfTypes(object sender, EventArgs e)
        {
            if (ListView_ListOfTypes.HeightRequest == 200)
            {
                ListView_ListOfTypes.HeightRequest = 0;
            }
            else
            {
                ListView_ListOfTypes.HeightRequest = 200;
            }
            if (ListView_ListOfTypes.ItemsSource == null)
            {
                List<string> L = new List<string>() { };
                L = Enum.GetNames(typeof(searchTags)).ToList();
                if (L.Contains("Error"))
                {
                    L.Remove("Error");
                }

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
                        DataHolder DH = new DataHolder { Name = "Error", Objert = O };
                        DH.Type = CheckType(O);
                        if (DH.Type == searchTags.Journal)
                        {
                            Journal J = (Journal)O;
                            DH.Name = J.Name;
                        }
                        if (DH.Type == searchTags.PartInformation)
                        {
                            Models.PartInformation J = (Models.PartInformation)O;
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
            #region
            if (O.GetType() == typeof(Models.PartInformation))
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
        public List<Object> Find(string Key, Journal journal)
        {
            List<Object> LO = new List<object>();
            #region
            if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.Journal)
            {
                if (StringTjekkerReturn(journal.Name, Key) || Key == "" + journal.TimeStamp || "" + journal.ID == Key || Key == "")
                { LO.Add(journal); }
            }
            #endregion
            foreach (var partInformations in journal.JournalChildren)
            {
                #region
                if (CurrentSeatchTag == searchTags.All || CurrentSeatchTag == searchTags.PartInformation)
                {
                    if (StringTjekkerReturn(partInformations.Name, Key) || StringTjekkerReturn(partInformations.ToolTip, Key) || Key == "" + partInformations.TimeStamp || "" + partInformations.ID == Key || Key == "")
                    { LO.Add(partInformations); }
                }
                #endregion
            }
            return LO;
        }
        // says if sting is incluede in string
        public bool StringTjekkerReturn(string _Text, string _Word)
        {
            if (_Text != null && _Word != null)
            {
                bool i = false;
                if (_Text.ToLower().Contains(_Word.ToLower()))
                { i = true; }
                return i;
            }
            return false;
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

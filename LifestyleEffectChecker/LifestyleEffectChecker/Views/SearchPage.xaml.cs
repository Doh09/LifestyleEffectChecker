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


            jvm = new JournalsViewModel();
            jvm.LoadJournalsCommand.Execute(null);
            List = jvm.Journals.ToList();
            foreach (var item in List)
            {
                DHs.Add(new DataHolder { Name = "tim", Objert = item, Type = CheckType(item) });
            }
            //viewModel.Journals.CollectionChanged += ListenToJournalChanges;
            List.Add(new Journal() { Name = "No journals", ID = -1, Actions = new List<Models.Action.Action>() }); //Display this "Journal" if initial loading of journals failed
            List.Add(new Journal() { Name = "No journals", ID = -1, Actions = new List<Models.Action.Action>() }); //Display this "Journal" if initial loading of journals failed



            //     BuildList(_LS);
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
            ListView.ItemsSource = List;
        }

        private void Search_Button_Clicked(object sender, EventArgs e)
        {
            // jvm.LoadJournalsCommand.Execute(null);
            List = jvm.Journals.ToList();
            List.Add(new Journal() { Name = "No journals", ID = -1, Actions = new List<Models.Action.Action>() }); //Display this "Journal" if initial loading of journals failed

            foreach (var journal in List)
            {
                List<Object> ffv = Search(journal);
            }
            BindingContext = List;
            Refresh();
        }

        private List<Object> Search(Journal journal)
        {
            //Console.WriteLine("what type do you want");
            //String SearchType = Console.ReadLine();

            // Console.WriteLine("Write a Text to search for");
            // string s = Console.ReadLine();
            var KeyWord = EntryKeyWord.Text;
            List<Object> LO = Find(KeyWord, journal);

            if (LO != null)
            {
                foreach (var O in LO)
                {
                    if (O != null)
                    {
                        DHs.Add(new DataHolder { Name = "Bob", Objert = O, Type = CheckType(O) });
                    }
                }
            }
            return null;
        }
        private static string CheckType(Object O)
        {
            if (O.GetType() == typeof(Journal))
            {
                return ("jounal");
            }
            else
            {
                // Console.WriteLine("not a jounal");
            }
            if (O.GetType() == typeof(Models.Action.Action))
            {
                return ("Action");
            }
            else
            {
                // Console.WriteLine("not a Action");
            }
            if (O.GetType() == typeof(Models.Action.ActionPart))
            {
                return ("ActionPart");
            }
            else
            {
                // Console.WriteLine("not a ActionPart");
            }
            if (O.GetType() == typeof(Models.Action.PartInformation))
            {
                return ("PartInformation");
            }
            else
            {
                //  Console.WriteLine("not a PartInformation");
            }
            return "Error";
        }

        public static List<Object> Find(string Key, Journal journal)
        {
            List<Object> LO = new List<object>();
            if (journal.Name == Key) { LO.Add(journal); }
            foreach (var action in journal.Actions)
            {
                if (action.Name == Key) { LO.Add(action); }
                foreach (var actionParts in action.ActionParts)
                {
                    if (actionParts.Name == Key) { LO.Add(actionParts); }
                    foreach (var partInformations in actionParts.PartInformations)
                    {
                        if (partInformations.Name == Key) { LO.Add(partInformations); }
                    }
                }
            }
            return LO;
        }
    }

    internal class DataHolder
    {
        public string Type;
        public Object Objert;
        public string Name;
    }
}

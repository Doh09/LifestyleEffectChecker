using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LifestyleEffectChecker.ViewModels.Detail;
using LifestyleEffectChecker.ViewModels.Detail.Action;
using LifestyleEffectChecker.ViewModels.Detail.Effect;
using static Java.Util.Jar.Attributes;

namespace LifestyleEffectChecker.Views.DetailViews
{
    // not includete
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListReturn : ContentPage
    {
        JournalDetailViewModel viewModel;

        public ListReturn(JournalDetailViewModel _viewModel)
        {
            viewModel = _viewModel;
            foreach (var item in viewModel.Journal.JournalChildren)
            {
                #region
                //  if (item is Models.Action.Action)
                //  {
                //      JournalChild mockAction = new Models.Action.Action();
                //      mockAction.Name = "_1Action";
                //      viewModel.Journal.JournalChildren.Add(mockAction);
                //  }
                //  else if (item is Models.Effect.Effect)
                //  {
                //      JournalChild mockEffect = new Models.Effect.Effect();
                //      mockEffect.Name = "_1Effect";
                //      viewModel.Journal.JournalChildren.Add(mockEffect);
                //  }
                #endregion
            }
            InitializeComponent();
            UpdateView();

            ChildListView.ItemTapped += (sender, e) => {
                JournalChildDataHolder s = (JournalChildDataHolder)e.Item;
                OnButtonClicked_button_Back(s.JournalChild);

            };
        }

        private void OnButtonClicked_button_Back(JournalChild JournalChild)
        {
            viewModel.Journal.JournalChildren.Add(JournalChild);
            Navigation.RemovePage(this);
            
        }

        public void UpdateView()
        {
            List<JournalChild> JCList = viewModel.Journal.JournalChildren; // GetJournallChildren();
            List<JournalChildDataHolder> JCDHList = new List<JournalChildDataHolder>();
            foreach (var item in JCList)
            {
                JCDHList.Add(new JournalChildDataHolder() { Name = item.Name, JournalChild = item });
            }
            ChildListView.ItemsSource = null;
            ChildListView.ItemsSource = JCDHList;
        }

    //  private async List<JournalChild> GetJournallChildren()
    //  {
    //      try
    //      {
    //          var journals = await journalRepository.ReadAll();//await DataStore.GetItemsAsync(true);
    //
    //          return Journals = new ObservableRangeCollection<Journal>(journals);
    //      }
    //      catch (Exception ex)
    //      {
    //          MessagingCenter.Send(new MessagingCenterAlert
    //          {
    //              Title = "Error",
    //              Message = "Unable to load journals.",
    //              Cancel = "OK"
    //          }, "message");
    //      }
    //      return new List<JournalChild>();
    //  }
    }

    internal class JournalChildDataHolder
    {
        public string Name;
        public JournalChild JournalChild;

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}

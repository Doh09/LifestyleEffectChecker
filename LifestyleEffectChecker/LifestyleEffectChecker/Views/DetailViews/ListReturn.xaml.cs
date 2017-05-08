using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LifestyleEffectChecker.Views.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListReturn : ContentPage
    {
        public ListReturn()
        {
            InitializeComponent();
        }
        private async void JournalsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Models.Action.Action)
            {

            }
            else if (e.SelectedItem is Models.Effect.Effect)
            {

            }
            //
        }
    }
}

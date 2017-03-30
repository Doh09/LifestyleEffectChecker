using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace LifestyleEffectChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleMapsPage : TabbedPage
    {
        public GoogleMapsPage()
        {
            InitializeComponent();
        }
    }
}

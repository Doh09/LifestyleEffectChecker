using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Action;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews.Action
{
    public partial class ActionDetailPage : ContentPage
    {
        ActionDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ActionDetailPage()
        {
            InitializeComponent();
        }

        public ActionDetailPage(ActionDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}

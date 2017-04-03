using LifestyleEffectChecker.ViewModels;
using LifestyleEffectChecker.ViewModels.Action;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews.Action
{
    public partial class ActionPartDetailPage : ContentPage
    {
        ActionPartDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ActionPartDetailPage()
        {
            InitializeComponent();
        }

        public ActionPartDetailPage(ActionPartDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}

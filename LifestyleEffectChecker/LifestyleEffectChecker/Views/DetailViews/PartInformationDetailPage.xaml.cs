using LifestyleEffectChecker.ViewModels.Detail;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews
{
    public partial class PartInformationDetailPage : ContentPage
    {
        PartInformationDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public PartInformationDetailPage()
        {
            InitializeComponent();
        }

        public PartInformationDetailPage(PartInformationDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}

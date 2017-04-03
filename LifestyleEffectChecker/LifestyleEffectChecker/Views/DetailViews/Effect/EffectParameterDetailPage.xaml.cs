using LifestyleEffectChecker.ViewModels.Effect;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews.Effect
{
    public partial class EffectParameterDetailPage : ContentPage
    {
        EffectParameterViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EffectParameterDetailPage()
        {
            InitializeComponent();
        }

        public EffectParameterDetailPage(EffectParameterViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}

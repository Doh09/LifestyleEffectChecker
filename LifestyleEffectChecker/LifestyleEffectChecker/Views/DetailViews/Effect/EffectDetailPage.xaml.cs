using LifestyleEffectChecker.ViewModels.Detail.Effect;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews.Effect
{
    public partial class EffectDetailPage : ContentPage
    {
        EffectDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EffectDetailPage()
        {
            InitializeComponent();
        }

        public EffectDetailPage(EffectDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}

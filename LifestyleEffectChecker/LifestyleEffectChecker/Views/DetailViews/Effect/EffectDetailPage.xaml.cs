using LifestyleEffectChecker.ViewModels.Detail.Effect;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Views.DetailViews.Effect
{
    public partial class EffectDetailPage : ContentPage
    {
        EffectViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EffectDetailPage()
        {
            InitializeComponent();
        }

        public EffectDetailPage(EffectViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}

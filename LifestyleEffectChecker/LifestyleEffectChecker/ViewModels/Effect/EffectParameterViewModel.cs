using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.Models.Effect;

namespace LifestyleEffectChecker.ViewModels
{
    public class EffectParameterViewModel : BaseViewModel
    {
        
        public EffectParameter EffectParameter { get; set; }
        public EffectParameterViewModel(EffectParameter effectParameter = null)
        {
            if (effectParameter != null)
            Title = effectParameter.Name;
            else
            {
                Title = "No EffectParameter in database";
            }
            EffectParameter = effectParameter;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
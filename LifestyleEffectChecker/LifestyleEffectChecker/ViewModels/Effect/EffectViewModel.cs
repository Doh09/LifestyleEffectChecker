using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.Models.Effect;

namespace LifestyleEffectChecker.ViewModels
{
    public class EffectViewModel : BaseViewModel
    {
        
        public Effect Effect { get; set; }
        public EffectViewModel(Effect effect = null)
        {
            if (effect != null)
            Title = effect.Name;
            else
            {
                Title = "No Effect in database";
            }
            Effect = effect;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
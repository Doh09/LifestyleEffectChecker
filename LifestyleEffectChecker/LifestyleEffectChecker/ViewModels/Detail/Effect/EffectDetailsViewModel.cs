namespace LifestyleEffectChecker.ViewModels.Detail.Effect
{
    public class EffectViewModel : BaseViewModel
    {
        
        public Models.Effect.Effect Effect { get; set; }
        public EffectViewModel(Models.Effect.Effect effect = null)
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
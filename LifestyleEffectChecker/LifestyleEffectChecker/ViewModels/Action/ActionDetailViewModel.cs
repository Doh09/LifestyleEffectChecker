using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;

namespace LifestyleEffectChecker.ViewModels
{
    public class ActionDetailViewModel : BaseViewModel
    {
        
        public Action Action { get; set; }
        public ActionDetailViewModel(Action action = null)
        {
            if (action != null)
                Title = action.Name;
            else
            {
                Title = "No action";
            }
            Action = action;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
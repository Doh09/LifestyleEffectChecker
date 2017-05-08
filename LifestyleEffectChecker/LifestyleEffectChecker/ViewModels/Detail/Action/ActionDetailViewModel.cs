using LifestyleEffectChecker.Models;

namespace LifestyleEffectChecker.ViewModels.Detail.Action
{
    public class ActionDetailViewModel : BaseViewModel
    {
        
        public Models.Action.Action Action { get; set; }
        public ActionDetailViewModel(Models.Action.Action action = null)
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
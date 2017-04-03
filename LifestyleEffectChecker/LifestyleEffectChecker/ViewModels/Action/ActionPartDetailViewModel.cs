using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;

namespace LifestyleEffectChecker.ViewModels
{
    public class ActionPartDetailViewModel : BaseViewModel
    {
        
        public ActionPart ActionPart { get; set; }
        public ActionPartDetailViewModel(ActionPart actionPart = null)
        {
            if (actionPart != null)
            Title = actionPart.Name;
            else
            {
                Title = "No actionPart";
            }
            ActionPart = actionPart;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
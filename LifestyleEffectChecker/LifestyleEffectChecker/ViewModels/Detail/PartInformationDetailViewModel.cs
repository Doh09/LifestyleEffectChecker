using LifestyleEffectChecker.Models;

namespace LifestyleEffectChecker.ViewModels.Detail
{
    public class PartInformationDetailViewModel : BaseViewModel
    {
        
        public PartInformation PartInformation { get; set; }
        public PartInformationDetailViewModel(PartInformation partInformation = null)
        {
            if (partInformation != null)
            Title = partInformation.Name;
            else
            {
                Title = "No PartInformation";
            }
            PartInformation = partInformation;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
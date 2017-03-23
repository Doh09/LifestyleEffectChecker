using LifestyleEffectChecker.Models;

namespace LifestyleEffectChecker.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        
        public Item Item { get; set; }
        public Journal Journal { get; set; }
        public ItemDetailViewModel(Journal journal = null)
        {
            Title = journal.Name;
            Journal = journal;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
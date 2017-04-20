using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Action
{
    public class PartInformation : Parameter
    {
        [ForeignKey(typeof(ActionPart))]
        public int parentID { get; set; }

    }
}
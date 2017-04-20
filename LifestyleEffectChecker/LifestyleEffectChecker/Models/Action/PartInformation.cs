using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Action
{
    public class PartInformation : AbstractBaseObject
    {
        [ForeignKey(typeof(ActionPart))]
        public int parentID { get; set; } = -1;
        public string Name { get; set; }
    }
}
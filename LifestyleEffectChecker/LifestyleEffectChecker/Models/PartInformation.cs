using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models
{
    public class PartInformation : AbstractBaseObject
    {
        [ForeignKey(typeof(ActionPart))]
        public int parentID { get; set; }
        public string Name { get; set; }
    }
}
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Action
{
    public class ActionPart : AbstractBaseObject
    {
        [ForeignKey(typeof(Action))]
        public int parentID { get; set; }

        public string Name { get; set; }

        [OneToMany]
        public List<PartInformation> PartInformations { get; set; } = new List<PartInformation>();
    }
}
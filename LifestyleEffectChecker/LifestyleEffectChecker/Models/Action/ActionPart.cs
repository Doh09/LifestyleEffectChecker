using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Action
{
    /// <summary>
    /// A subpart of the action you wish to track, e.g. "Apple" or "Running"
    /// </summary>
    public class ActionPart : AbstractBaseObject
    {
        [ForeignKey(typeof(Action))]
        public int parentID { get; set; } = -1;

        [OneToMany]
        public List<PartInformation> PartInformations { get; set; } = new List<PartInformation>();
    }
}
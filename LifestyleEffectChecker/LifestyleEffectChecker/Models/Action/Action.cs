using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Action
{
    public class Action : AbstractBaseObject
    {
        [ForeignKey(typeof(Journal))]
        public int parentID { get; set; } = -1;

        public string Name { get; set; }

        [OneToMany]
        public List<ActionPart> ActionParts { get; set; } = new List<ActionPart>();
    }


}

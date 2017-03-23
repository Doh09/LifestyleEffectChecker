using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models
{
    public class Action : AbstractBaseObject
    {
        [ForeignKey(typeof(Journal))]
        public int parentID { get; set; }

        public string Name { get; set; }

        [OneToMany]
        public List<ActionPart> ActionParts { get; set; } = new List<ActionPart>();
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models
{
    public class Journal : AbstractBaseObject
    {
        [OneToMany]
        public List<PartInformation> JournalChildren { get; set; } = new List<PartInformation>();
        //[OneToMany]
        //public List<Action.Action> Actions { get; set; } = new List<Action.Action>();
        //[OneToMany]
        //public List<Effect.Effect> Effects { get; set; } = new List<Effect.Effect>();
        //Journal should probably have a common list for actions and effects? Maybe they should both have an interface called "JournalChild"
    }
}

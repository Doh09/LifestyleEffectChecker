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
        public string Name { get; set; }

        [OneToMany]
        public List<Action.Action> ActionParts { get; set; } = new List<Action.Action>();
    }
}

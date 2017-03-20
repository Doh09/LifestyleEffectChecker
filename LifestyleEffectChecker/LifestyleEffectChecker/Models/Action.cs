using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Models
{
    public class Action : AbstractBaseObject
    {
        public string Name { get; set; }

        public List<ActionPart> ActionParts { get; set; } = new List<ActionPart>();
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Models
{
    class Journal : AbstractBaseObject
    {
        public string Name { get; set; }

        public List<Action> ActionParts { get; set; } = new List<Action>();
    }
}

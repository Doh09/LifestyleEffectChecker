using System.Collections.Generic;

namespace LifestyleEffectChecker.Models
{
    public class ActionPart : AbstractBaseObject
    {
        
        public string Name { get; set; }

        public List<PartInformation> PartInformations { get; set; } = new List<PartInformation>();
    }
}
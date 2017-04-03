using System.Collections.Generic;

namespace LifestyleEffectChecker.Models.Effect
{
    public class Effect : AbstractBaseObject
    {
        public string Name { get; set; }
        public List<EffectParameter> EffectParameters { get; set; } = new List<EffectParameter>();


    }
}

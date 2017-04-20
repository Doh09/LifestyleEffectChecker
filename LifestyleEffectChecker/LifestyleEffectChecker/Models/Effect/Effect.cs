using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Effect
{
    public class Effect : AbstractBaseObject
    {
        public string Name { get; set; }
        [OneToMany]
        public List<EffectParameter> EffectParameters { get; set; } = new List<EffectParameter>();


    }
}

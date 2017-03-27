using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Models
{
    public class Effect : AbstractBaseObject
    {
        public string Name { get; set; }
        public List<EffectParameter> MeasuringMethod { get; set; } = new List<EffectParameter>();


    }
}

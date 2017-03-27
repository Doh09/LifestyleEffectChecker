using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Models
{
    public class EffectParameter : AbstractBaseObject
    {
        
        public string Name { get; set; }

        public MeasuringMethod Method { get; set; } = MeasuringMethod.Slider;

        public object Value { get; set; }
    }

    public enum MeasuringMethod
    {
        Slider, Decimal, Number, Text, GPSLocation, Picture
    }

}

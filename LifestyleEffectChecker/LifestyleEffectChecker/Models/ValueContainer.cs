using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace LifestyleEffectChecker.Models
{
    public class ValueContainer
    {
        public string Name { get; set; }
        private string Text { get; set; }
        private double DecimalValue { get; set; }
        private int NumberValue { get; set; }
        private Position GeoPosition { get; set; }
        public MeasuringMethod MeasuringMethod { get; set; }

    }
}

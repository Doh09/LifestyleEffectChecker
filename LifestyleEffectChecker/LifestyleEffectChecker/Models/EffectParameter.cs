using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace LifestyleEffectChecker.Models
{
    public class EffectParameter : AbstractBaseObject
    {

        public string Name { get; set; }
        public MeasuringMethod MeasuringMethod { get; set; } = MeasuringMethod.Slider;

        private string Text { get; set; }
        private string ImgPath { get; set; }
        private double DecimalValue { get; set; }
        private double SliderValue { get; set; }
        private int NumberValue { get; set; }
        private Position GeoPosition { get; set; }

        public string GetValue()
        {
            switch (MeasuringMethod)
            {
                case MeasuringMethod.Text:
                    return Text;
                case MeasuringMethod.Decimal:
                    return DecimalValue+"";
                case MeasuringMethod.Number:
                    return NumberValue + "";
                case MeasuringMethod.Picture:
                    return ImgPath;
                case MeasuringMethod.Slider:
                    return SliderValue+"";
                    case MeasuringMethod.GPSLocation:
                    return "GeoLocation - Latitude:" + GeoPosition.Latitude + " " + "Longitude:" +
                           GeoPosition.Longitude;
                default:
                    return "No measuring method defined";
            }
        }
    }

    public enum MeasuringMethod
    {
        Slider, Decimal, Number, Text, GPSLocation, Picture
    }

}

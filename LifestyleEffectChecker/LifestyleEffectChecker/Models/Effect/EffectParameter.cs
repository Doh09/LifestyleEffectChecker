using Plugin.Geolocator.Abstractions;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Effect
{
    public class EffectParameter : Parameter
    {
        [ForeignKey(typeof(Effect))]
        public int parentID { get; set; }

    }
}

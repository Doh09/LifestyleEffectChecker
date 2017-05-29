using Plugin.Geolocator.Abstractions;
using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models.Effect
{
    /// <summary>
    /// A parameter by which you track the parent Effect.
    /// </summary>
    public class EffectParameter : Parameter
    {
        [ForeignKey(typeof(Effect))]
        public int parentID { get; set; }

    }
}

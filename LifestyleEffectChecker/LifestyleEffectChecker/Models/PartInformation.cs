using SQLiteNetExtensions.Attributes;

namespace LifestyleEffectChecker.Models
{
    /// <summary>
    /// The parameter with which you track, e.g. if you were running,  you could track for how many minutes.
    /// E.g. I ran for 10 minutes.
    /// How i felt before running.
    /// How i felt after running.
    /// The ActionPart parent should then have a PartInformation for each of the 3 examples which are to be tracked.
    /// The PartInformations Name property can be used to describe the tracked value, the tracked value and how it is measured, will come from Parameter.
    /// </summary>
    public class PartInformation : Parameter
    {
        [ForeignKey(typeof(Journal))]
        public int parentID { get; set; }
    }
}
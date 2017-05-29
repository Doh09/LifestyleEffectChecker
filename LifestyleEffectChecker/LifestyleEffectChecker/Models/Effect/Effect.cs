using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Models.Effect
{
    /// <summary>
    /// An effect which you wish to track.
    /// E.g. "Stress level" or "energy level"
    /// </summary>
    public class Effect : JournalChild
    {
        [OneToMany]
        public List<EffectParameter> EffectParameters { get; set; } = new List<EffectParameter>();

        /// <summary>
        /// Returns a stacklayout that is a representation of this Effect, used where it appears as a JournalChild.
        /// </summary>
        /// <returns></returns>
        public StackLayout GetStacklayoutRepresentation()
        {
            StackLayout sl = new StackLayout();
            Label id = new Label();
            id.Text = "id: " + ID;
            sl.Children.Add(id);
            Label name = new Label();
            name.Text = "name: " + Name;
            sl.Children.Add(name);
            return sl;
        }
    }
}

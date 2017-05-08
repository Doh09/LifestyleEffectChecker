using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Models.Action
{
    /// <summary>
    /// An action you wish to track, e.g. "Eat" or "Excercise"
    /// </summary>
    public class Action : AbstractBaseObject, JournalChild
    {
        [ForeignKey(typeof(Journal))]
        public int parentID { get; set; } = -1;

        [OneToMany]
        public List<ActionPart> ActionParts { get; set; } = new List<ActionPart>();

        /// <summary>
        /// Returns a stacklayout that is a representation of this Action, used where it appears as a JournalChild.
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

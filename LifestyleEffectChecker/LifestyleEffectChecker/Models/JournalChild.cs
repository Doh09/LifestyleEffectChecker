using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Models
{
    /// <summary>
    /// This interface needs to be put on objects that will be presented in a Journal's overview.
    /// The interface will give access to any possible common usage in this regard.
    /// </summary>
    public abstract class JournalChild : AbstractBaseObject
    {
        /// <summary>
        /// Returns a stacklayout that is a brief data summary of this Journalchild.
        /// </summary>
        /// <returns></returns>
        ///StackLayout GetStacklayoutRepresentation();
    }
}

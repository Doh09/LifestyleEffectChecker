using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker._Camera
{
    public delegate void PhotoTaken(String s);

    public interface ICameraHandler
    {
        void TakePhoto();
        void AddPhotoTakenEventhandler(PhotoTaken pt);
    }
}

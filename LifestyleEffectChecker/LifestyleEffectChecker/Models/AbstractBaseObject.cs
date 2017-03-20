using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net.Attributes;

namespace LifestyleEffectChecker.Models
{
    public class AbstractBaseObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}

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
        public int ID { get; set; } = -1;

        public string Name { get; set; } = "noNameSet";

        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}

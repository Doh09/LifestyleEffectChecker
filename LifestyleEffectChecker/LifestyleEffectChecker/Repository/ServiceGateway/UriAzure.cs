using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Repository.ServiceGateway
{
    public class UriAzure
    {
        public string DataBaseUri { get; set; } = "http://lifestyleeffectchecker.azurewebsites.net/";
        public string MessageUri { get; set; } = "";
    }
}

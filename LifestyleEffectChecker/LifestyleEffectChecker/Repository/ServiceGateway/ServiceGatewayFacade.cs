using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models;

namespace LifestyleEffectChecker.Repository.ServiceGateway
{
    class ServiceGatewayFacade
    {

        public static IRepository<Journal> GetJournalServiceGateway()
        {
            return new JournalServiceGateway();
        }


        public static IRepository<PartInformation> GetPartInformationServiceGateway()
        {
            return new PartInformationServiceGateway();
        }

    }
}

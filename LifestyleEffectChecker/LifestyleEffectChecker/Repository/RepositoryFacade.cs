using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository.ServiceGateway;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository
{
    public class RepositoryFacade
    {
        public static IRepository<Journal> GetJournalRepository()
        {
            return JournalRepository.GetInstance();
        }

        public static IRepository<PartInformation> GetPartInformationRepository()
        {
            return PartInformationRepository.GetInstance();
        }

    }
}

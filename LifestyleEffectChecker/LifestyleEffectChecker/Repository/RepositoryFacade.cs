using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.Repository.Action;
using LifestyleEffectChecker.Repository.Effect;
using LifestyleEffectChecker.Repository.ServiceGateway;

namespace LifestyleEffectChecker.Repository
{
    public class RepositoryFacade
    {
        #region Shared between Action and Effect
        public static IRepository<Journal> GetJournalRepository()
        {
            return JournalRepository.GetInstance();
        }
        #endregion

        #region Action
        public static IRepository<Models.Action.Action> GetActionRepository()
        {
            return ActionRepository.GetInstance();
        }

        public static IRepository<ActionPart> GetActionPartRepository()
        {
            return ActionPartRepository.GetInstance();
        }

        public static IRepository<PartInformation> GetPartInformationRepository()
        {
            return PartInformationRepository.GetInstance();
        }
        #endregion

        #region Effect
        public static IRepository<Models.Effect.Effect> GetEffectRepository()
        {
            return EffectRepository.GetInstance();
        }

        public static IRepository<Models.Effect.EffectParameter> GetEffectParameterRepository()
        {
            return EffectParameterRepository.GetInstance();
        }
        #endregion
    }
}

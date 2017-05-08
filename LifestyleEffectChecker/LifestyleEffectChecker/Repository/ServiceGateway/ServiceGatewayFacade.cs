using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.Repository.Action;
using LifestyleEffectChecker.Repository.Effect;
using LifestyleEffectChecker.Repository.ServiceGateway.Action;
using LifestyleEffectChecker.Repository.ServiceGateway.Effect;

namespace LifestyleEffectChecker.Repository.ServiceGateway
{
    class ServiceGatewayFacade
    {
        #region Shared between Action and Effect
        public static IRepository<Journal> GetJournalServiceGateway()
        {
            return new JournalServiceGateway();
        }
        #endregion

        #region Action
        public static IRepository<Models.Action.Action> GetActionServiceGateway()
        {
            return new ActionServiceGateway();
        }

        public static IRepository<ActionPart> GetActionPartServiceGateway()
        {
            return new ActionPartServiceGateway();
        }

        public static IRepository<PartInformation> GetPartInformationServiceGateway()
        {
            return new PartInformationServiceGateway();
        }
        #endregion

        #region Effect
        public static IRepository<Models.Effect.Effect> GetEffectServiceGateway()
        {
            return new EffectServiceGateway();
        }

        public static IRepository<Models.Effect.EffectParameter> GetEffectParameterServiceGateway()
        {
            return new EffectParemeterServiceGateway();
        }
        #endregion
    }
}

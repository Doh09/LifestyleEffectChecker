using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Helpers;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository;
using LifestyleEffectChecker.Views.CreateEditViews;
using LifestyleEffectChecker.Views.DeleteViews;
using LifestyleEffectChecker.Views.DetailViews;
using Xamarin.Forms;

namespace LifestyleEffectChecker.ViewModels.Index
{
    public class PartInformationsViewModel : BaseViewModel
    {
        public Journal ParentJournal { get; set; }

        public ObservableRangeCollection<PartInformation> PartInformations { get; set; }

        public Command LoadPartInformationsCommand { get; set; }

        IRepository<PartInformation> partInformationRepository = RepositoryFacade.GetPartInformationRepository();

        public PartInformationsViewModel(Journal parent = null)
        {
            this.ParentJournal = parent;
            if (parent != null)
                Title = "Browse " + ParentJournal.Name;
            else
            {
                Title = "Browse Information (no journal)";
            }
            PartInformations = new ObservableRangeCollection<PartInformation>();
            LoadPartInformationsCommand = new Command(async () => await ExecuteLoadPartInformationsCommand());

            MessagingCenter.Subscribe<NewPartInformationPage, PartInformation>(this, "AddPartInformation",
                async (obj, partInformation) =>
                {
                    await partInformationRepository.Create(partInformation);
                    await ExecuteLoadPartInformationsCommand();
                });

            MessagingCenter.Subscribe<NewPartInformationPage, PartInformation>(this, "EditPartInformation",
                async (obj, partInformation) =>
                {
                    //It gets here when you click save in the item page.
                    await partInformationRepository.Update(partInformation);
                    await ExecuteLoadPartInformationsCommand();
                });

            MessagingCenter.Subscribe<PartInformationDeletePage, PartInformation>(this, "DeletePartInformation",
                async (obj, partInformation) =>
                {
                    //It gets here when you click save in the item page.
                    await partInformationRepository.Delete(partInformation.ID);

                    await ExecuteLoadPartInformationsCommand();
                });

        }

        async Task ExecuteLoadPartInformationsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var pInformations = await partInformationRepository.ReadAll(); //await DataStore.GetItemsAsync(true);
                if (ParentJournal != null)
                {
                    //If there is a parent journal, then take only the partInformations that belong to it.
                    pInformations = pInformations.Where(x => x.parentID == ParentJournal.ID);
                    if (pInformations == null) //Avoid nullpointer error.
                        pInformations = new List<PartInformation>();
                    ParentJournal.JournalChildren = pInformations.ToList();
                }
                PartInformations = new ObservableRangeCollection<PartInformation>(pInformations);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load partinformations.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
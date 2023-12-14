using Prototypes.Model;
using Prototypes.Business_Logic.Interface;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    public partial class BusinessLogic : IBusinessLogic
    {
        /// <summary>
        /// Gets the ObservableCollection performers
        /// </summary>
        public ObservableCollection<Performer> GetCheckedInPerformers(Rehearsal rehearsal)
        {
             return Database.GetCheckedInPerformers(rehearsal);
        }

        /// <summary>
        /// Gets the ObservableCollection of all not checked in performers
        /// </summary>
        public ObservableCollection<Performer> GetNotCheckedInPerformers(Rehearsal rehearsal)
        {
            return Database.GetNotCheckedInPerformers(rehearsal);
        }

        /// <summary>
        /// Adds a performer into the checked in list
        /// </summary>
        /// <param name="performer"></param>
        /// <param name="status"></param>
        /// <returns>if the add was successful, success or error message</returns>
        public (bool success, string message) CheckInPerformer(Performer performer, String status)
        {
            return Database.CheckInPerformer(performer, status);
        }

        /// <summary>
        /// This method updates a performer's status- whether they are checked in, not checked in, or excused
        /// </summary>
        /// <param name="performer">The name of the performer</param>
        /// <param name="status">The status- checked in, not checked in, or excused</param>
        /// <returns>a tuple of a boolean based on the success of updating and a message</returns>
        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status)
        {
            return Database.UpdatePerformerStatus(performer, status);
        }

        /// <summary>
        /// This method updates whether or not a performer is checked in for a specific rehearsal or not
        /// </summary>
        /// <param name="performer">The performer</param>
        /// <param name="rehearsal">The rehearsal</param>
        /// <param name="isCheckedIn">true or false if they are checked in or not for that rehearsal</param>
        /// <returns>a tuple of boolean true or false if the status was updated and a message</returns>
        public (bool success, string message) UpdatePerformerRehearsalStatus(Performer performer, Rehearsal rehearsal, bool isCheckedIn)
        {
            return Database.UpdatePerformerRehearsalStatus(performer, rehearsal, isCheckedIn);
        }
    }
}

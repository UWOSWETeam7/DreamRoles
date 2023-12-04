using Prototypes.Model;
using Prototypes.Business_Logic.Interface;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    public partial class BusinessLogic : IBusinessLogic
    {
        /// <summary>
        /// This method gets all rehearsals
        /// </summary>
        /// <returns>an observable collection of all rehearsals</returns>
        public ObservableCollection<Rehearsal> GetAllRehearsals()
        {
            return Database.GetAllRehearsals();
        }

        /// <summary>
        /// This method gets all rehearsals that the particular performer is involved in
        /// </summary>
        /// <param name="performer">The performer whose rehearsals you want to get</param>
        /// <returns>The observable collection of all of that performer's rehearsals</returns>
        public ObservableCollection<Rehearsal> GetPerformerRehearsals(Performer performer)
        {
            return Database.GetPerformerRehearsals(performer);
        }
        public (bool success, String message) AddPerformerToRehearsal(Performer performer, Rehearsal rehearsal)
        {
            return Database.InsertIntoRehersalMembers(performer.Id, rehearsal.Time, "checked in", rehearsal.Song.Title);
        }
        /// <summary>
        /// Removes a performer from the list of performers of a specific rehearsal
        /// </summary>
        /// <param name="performer"></param>
        /// <param name="rehearsal"></param>
        /// <returns></returns>
        public (bool success, String message) RemovePerformerFromRehearsal(Performer performer, Rehearsal rehearsal)
        {
            return Database.DeleteRehearsalMember(performer.Id, rehearsal.Time, rehearsal.Song.Title);
        }

        /// <summary>
        /// Gets the ObservableCollection of all rehearsals
        /// </summary>
        public ObservableCollection<Rehearsal> Rehearsals
        {
            get { return Database.GetAllRehearsals(); }
        }


        /// <summary>
        /// This method adds an entry into the rehearsal table, namely the time of the rehearsal and the song 
        /// they are rehearsing
        /// </summary>
        /// <param name="rehearsalTime">The time at which the rehearsal is taking place</param>
        /// <param name="songTitle">The title of the song to be rehearsed</param>
        /// <returns>String- null if the addition was successful or an error message if not</returns>
        public (bool success, string message) AddRehersal(DateTime rehearsalTime, String songTitle)
        {
            return Database.InsertIntoRehearsals(rehearsalTime, songTitle);
        }

        public (bool success, string message) DeleteRehersal(Rehearsal rehearsal)
        {
            return Database.DeleteRehearsal(rehearsal.Time, rehearsal.Song.Title);
        }
    }
}

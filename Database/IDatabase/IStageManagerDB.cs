using Prototype.Model;
using System.Collections.ObjectModel;

namespace Prototypes.Database
{
    public interface IStageManagerDB
    {
        /// <summary>
        /// Creates a file if needed and reads it and puts performer objects into a ObservableCollection
        /// </summary>
        /// <returns>a ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> SelectAllPerformers();

        /// <summary>
        /// Uses the given id to find a Aiport object with that id in a ObservableCollection
        /// </summary>
        /// <param name="id">the id of the performer that the user is trying to find</param>
        /// <returns>the performer if it is in the ObservableCollection. Returns null if the performer is not found</returns>
        public Performer SelectPerformer(String id);

        /// <summary>
        /// Adds a new performer object into the ObservableCollection and the file
        /// </summary>
        /// <param name="performer">the performer that the user wants to put into the file</param>
        /// <returns>True if it was inserted into the ObservableCollection and the file, false otherwise</returns>
        public Boolean InsertPerformer(Performer performer);

        /// <summary>
        /// Deletes the performer in the file with the given id
        /// </summary>
        /// <param name="id">the id of the performer that wll be deleted</param>
        /// <returns>true if the aiport is found in the file and delted. False if the performer is not in the file</returns>
        public Boolean DeletePerformer(String id);

        /// <summary>
        /// Will update a performer after checking that the performer is already in the file
        /// </summary>
        /// <param name="performer">the updated versoin of the performer</param>
        /// <returns>true if it found and updated the performer. False if it could not find the performer in the file</returns>
        public Boolean UpdatePerformer(Performer performer);
    }
}

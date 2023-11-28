using Prototypes.Model;
using System.Collections.ObjectModel;

namespace Prototypes.Databases.Interface
{
    public interface IDatabase
    {
        public String GetManagerAccessCode();
        public String GetChoreoAccessCode();
        public String GetPerformerAccessCode();
        public ObservableCollection<Performer> GetPerformersOfASong(Song song);
        public Boolean UpdatePerformerName(int userId, String firstName, String lastName);
        public Boolean UpdateSong(int setlistId, String oldSongName, String oldArtist, String songName, String artist, int duration);
        public Boolean InsertSong(int setlistId, String title, String artist, int duration);
        public ObservableCollection<Song> SelectAllSongs();
        public Boolean DeleteSong(String songTitle, String artistName);
        public Boolean InsertSongForPerformer(int userId, String songName, String artistName, int duration);
        public Boolean UpdatePerformerContact(int userId, String phoneNumber, String email);
        public ObservableCollection<Performer> GetCheckedInPerformers();
        public ObservableCollection<Performer> GetNotCheckedInPerformers();
        /// <summary>
        /// Creates a file if needed and reads it and puts performer objects into a ObservableCollection
        /// </summary>
        /// <returns>a ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> SelectAllPerformers(int year);

        /// <summary>
        /// Uses the given id to find a Aiport object with that id in a ObservableCollection
        /// </summary>
        /// <param name="id">the id of the performer that the user is trying to find</param>
        /// <returns>the performer if it is in the ObservableCollection. Returns null if the performer is not found</returns>
        public Performer SelectPerformer(int userId);

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
        public Boolean DeletePerformer(int userId);

        /// <summary>
        /// Will update a performer after checking that the performer is already in the file
        /// </summary>
        /// <param name="performer">the updated versoin of the performer</param>
        /// <returns>true if it found and updated the performer. False if it could not find the performer in the file</returns>
        public Boolean UpdatePerformer(Performer performer);
        /// <summary>
        /// Updates the checked in status of a performer
        /// </summary>
        /// <param name="performer"></param>
        /// <param name="status"></param>
        /// <returns>if the update was successful and a success/error message</returns>
        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status);
        /// <summary>
        /// Adds a peformer to the checked_in_performer database
        /// </summary>
        /// <param name="performer"></param>
        /// <param name="status"></param>
        /// <returns>if the add was successful, a success or error message</returns>
        public (bool success, string message) CheckInPerformer(Performer performer, String status);

        public Boolean UpdatePerformerAccessCode(int newAccessCode);


    }
}

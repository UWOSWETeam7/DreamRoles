using Prototypes.Model;
using System.Collections.ObjectModel;
using Prototypes.Model.Interfaces;

namespace Prototypes.Business_Logic.IBusinessLogic
{
    public interface IStageManagerBL
    {
        public Boolean EditSong(int setlistId, String oldSongName, String oldArtist,  String songName, String artist, int duration);
        public Boolean AddSong(int setlistId, String title, String artist, int duration);
        /// <summary>
        /// A ObservableCollection of performer objects that contians all the performer the user has inputed
        /// </summary>
        public ObservableCollection<Performer> Performers { get; }
        public ObservableCollection<Song> Songs { get; }
        public ObservableCollection<(Performer, DateTime?)> GetCheckedInPerformers { get; }
        public Boolean DeleteSong(String songTitle, String artistName);

        public Boolean AddSongForPerformer(int userId, String songName, String artistName, String duration);
        public Boolean EditPerformerContact(int userId, String phoneNumber, String email);

        /// <summary>
        /// Creates a performer object and send it to the Database
        /// </summary>
        /// <param name="id">the id of the performer</param>
        /// <param name="city">the city the performer is in</param>
        /// <param name="dateVisted">the date the user visited the performer</param>
        /// <param name="rating">the rating the gave to the performer</param>
        /// <returns>true if all the params are valid, false if not</returns>
        public Boolean AddPerformer(int userId, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber);

        /// <summary>
        /// Checks if the id is valid then asks the Database to delete the performer with that id
        /// </summary>
        /// <param name="id">the id of the performer that is supposed to be deleted</param>
        /// <returns>true if the Database can delete the performer. False if the param is invalid or the performer could not be found in the Database</returns>
        public Boolean DeletePerformer(int userId);

        /// <summary>
        /// Creates a performer object to replace another performer object and send it to the Database
        /// </summary>
        /// <param name="id">the id of the performer</param>
        /// <param name="city">the city the performer is in</param>
        /// <param name="dateVisted">the date the user visited the performer</param>
        /// <param name="rating">the rating the gave to the performer</param>
        /// <returns>true if all the params are valid, false if the Database could not find the performer to edit or the params are invalid</returns>
        public Boolean EditPerformer(int userId, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber);

        /// <summary>
        /// Find the performer object with the given id
        /// </summary>
        /// <param name="id">the id of the performer that is suppose to be found</param>
        /// <returns>the performer if it is found null if it is not found or invalid param</returns>
        public Performer FindPerformer(int userId);


        /// <summary>
        /// Gets all the performers from the Database
        /// </summary>
        /// <returns>a bservableCollection of performers objects the user has visted</returns>
        public ObservableCollection<Performer> GetPerformers();

        
    }
}

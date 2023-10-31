using Prototypes.Model;
using Prototypes.Business_Logic.IBusinessLogic;
using Prototypes.Database;
using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    class StageManagerBL : IStageManagerBL
    {

        //A interface of StageManagerDB
        private IStageManagerDB StageManagerDB = new StageManagerDB();

        public Boolean AddSong(int setlistId, String title, String artist, int duration)
        {
            return StageManagerDB.InsertSong(setlistId, title, artist, duration);
        }
        /// <summary>
        /// Gets the ObservableCollection performers
        /// </summary>
        public ObservableCollection<Performer> Performers
        {
            get { return StageManagerDB.SelectAllPerformers(); }
        }
        public ObservableCollection<Song> Songs
        {
            get { return StageManagerDB.SelectAllSongs(); }
        }

        public Boolean DeleteSong(String songTitle, String artistName)
        {
            return StageManagerDB.DeleteSong(songTitle, artistName);
        }

        public Boolean AddSongForPerformer(int userId, String songName, String artistName, String duration)
        {
            return StageManagerDB.InsertSongForPerformer(userId, songName, artistName, duration);
        }

        public Boolean EditPerformerContact(int userId, String phoneNumber, String email)
        {
            //Sends it to the StageManagerDB
            return StageManagerDB.UpdatePerformerContact(userId, phoneNumber, email);
        }

        /// <summary>
        /// Initializes a StageManagerDB when the program starts 
        /// </summary>
        public StageManagerBL()
        {
            StageManagerDB = new StageManagerDB();
        }

        /// <summary>
        /// Creates a Performer object and send it to the StageManagerDB
        /// </summary>
        /// <param name="id">the id of the performer</param>
        /// <param name="city">the city the performer is in</param>
        /// <param name="dateVisted">the date the user visited the performer</param>
        /// <param name="rating">the rating the gave to the performer</param>
        /// <returns>true if all the params are valid, false if not</returns>
        public Boolean AddPerformer(int userId, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber)
        {
            //Creates a new performer object
            Performer performer = new Performer(userId, firstName, lastName, songs, email, phoneNumber, 0);
            //Sends it to the StageManagerDB and gets a true or false depending if it can add it to the StageManagerDB
            return StageManagerDB.InsertPerformer(performer);
        }

        /// <summary>
        /// Checks if the id is valid then asks the StageManagerDB to delete the performer with that id
        /// </summary>
        /// <param name="id">the id of the performer that is supposed to be deleted</param>
        /// <returns>true if the StageManagerDB can delete the performer. False if the param is invalid or the performer could not be found in the StageManagerDB</returns>
        public Boolean DeletePerformer(int userId)
        {
           //Sends it to the StageManagerDB
           return StageManagerDB.DeletePerformer(userId);
        }

        /// <summary>
        /// Creates a performer object to replace another performer object and send it to the StageManagerDB
        /// </summary>
        /// <param name="id">the id of the performer</param>
        /// <param name="city">the city the performer is in</param>
        /// <param name="dateVisted">the date the user visited the performer</param>
        /// <param name="rating">the rating the gave to the performer</param>
        /// <returns>true if all the params are valid, false if the StageManagerDB could not find the performer to edit or the params are invalid</returns>
        public Boolean EditPerformer(int userId, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber)
        {
            //Creates a new performer object
            Performer performer = new Performer(userId, firstName, lastName, songs, email, phoneNumber, 0);
            //Sends it to the StageManagerDB
            return StageManagerDB.UpdatePerformer(performer);
        }

        /// <summary>
        /// Find the performer object with the given id
        /// </summary>
        /// <param name="id">the id of the performer that is suppose to be found</param>
        /// <returns>the performer if it is found null if it is not found or invalid param</returns>
        public Performer FindPerformer(int userId)
        {
            //Sends it to StageManagerDB
            return StageManagerDB.SelectPerformer(userId);
        }

        /// <summary>
        /// Gets all the performers from the StageManagerDB
        /// </summary>
        /// <returns>a observableCollection of performers objects the user has visted</returns>
        public ObservableCollection<Performer> GetPerformers()
        {
            return Performers;
        }
        /// <summary>
        /// Gets all the not checked in performers from the StageManagarDB
        /// </summary>
        /// <returns> an observableCollection of performer objects that are not checked in</returns>
        public ObservableCollection<Performer> NotCheckedInPerformers
        {
            get { return StageManagerDB.NotCheckedInPerformers(); }
        }
    }

}
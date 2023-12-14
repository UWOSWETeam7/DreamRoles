using Prototypes.Model;
using System.Collections.ObjectModel;
using Prototypes.Model.Interfaces;
using Prototypes.Databases;

namespace Prototypes.Business_Logic.Interface
{
    public interface IBusinessLogic
    {
        public Boolean DeleteAllTables();

        public ObservableCollection<Song> GetPerfomrerSetlist(int performerId);

        /// <summary>
        /// Gets the ObservableCollection of all performers
        /// </summary>
        public ObservableCollection<Performer> Performers { get; }

        /// <summary>
        /// Gets the ObservableCollection of all songs
        /// </summary>
        public ObservableCollection<Song> Songs { get; }

        /// <summary>
        /// Gets the ObservableCollection performers
        /// </summary>

        public ObservableCollection<Performer> GetCheckedInPerformers(Rehearsal rehearsal);

        /// <summary>
        /// Gets the ObservableCollection of all not checked in performers
        /// </summary>
        public ObservableCollection<Performer> GetNotCheckedInPerformers(Rehearsal rehearsal);


        /// <summary>
        /// Gets the ObservableCollection of all rehearsals
        /// </summary>
        public ObservableCollection<Rehearsal> Rehearsals { get; }

        /// <summary>
        /// This method adds an entry into the rehearsal table, namely the time of the rehearsal and the song 
        /// they are rehearsing
        /// </summary>
        /// <param name="rehearsalTime">The time at which the rehearsal is taking place</param>
        /// <param name="songTitle">The title of the song to be rehearsed</param>
        /// <returns>String- null if the addition was successful or an error message if not</returns>
        public (bool success, String message) AddRehersal(DateTime rehearsalTime, String songTitle);

        /// <summary>
        /// Deletes a rehearsal
        /// </summary>
        /// <param name="rehearsalTime">The time at which the rehearsal was taking place</param>
        /// <param name="songTitle">The title of the song rehearsed</param>
        /// <returns></returns>
        public (bool success, String message) DeleteRehersal(Rehearsal rehearsal);

        /// <summary>
        /// This method gets the stage manager's access code by calling the DB method by the same name
        /// </summary>
        /// <returns>String- the 7 digit access code</returns>
        public String GetManagerAccessCode();

        /// <summary>
        /// This method gets the choreographers's access code by calling the DB method by the same name
        /// </summary>
        /// <returns>String- the 7 digit access code</returns>
        public String GetChoreoAccessCode();

        /// <summary>
        /// This method gets the performer's access code by calling the DB method by the same name
        /// </summary>
        /// <returns>String- the 7 digit access code</returns>
        public String GetPerformerAccessCode();

        /// <summary>
        /// This method gets the list of all the peformers of a certain song
        /// </summary>
        /// <param name="song">The song for which you want to know the performers</param>
        /// <returns>Observable Collection of the performers for that song</returns>
        public ObservableCollection<Performer> GetPerformersOfASong(Song song);

        public ObservableCollection<Performer> GetPerformersOfARehearsal(Rehearsal rehearsal);
        public (bool success, String message) AddPerformerToRehearsal(Performer performer, Rehearsal rehearsal);
        public (bool success, String message) RemovePerformerFromRehearsal(Performer performer, Rehearsal rehearsal);

        /// <summary>
        /// This method updates a performer's name by identifying them with an id, and using the given first and last name
        /// </summary>
        /// <param name="userId">The int id to identify the performer</param>
        /// <param name="firstName">The first name of the performer</param>
        /// <param name="lastName">The last name of the performer</param>
        /// <returns>String- null if update was successful or error message if not</returns>
        public String EditPerformerName(int userId, String firstName, String lastName);

        /// <summary>
        /// This method updates an exisiting song's name given its old name to identify it and the new one
        /// </summary>
        /// <param name="oldSongName">The name of the song to update (its old name)</param>
        /// <param name="newSongName">The new name of the song</param>
        /// <returns>String- null if update was successful or error message if not</returns>
        public String EditSong(String oldSongName,String newSongName);

        /// <summary>
        /// This method adds a given song
        /// </summary>
        /// <param name="title">The title of the song to add</param>
        /// <returns>String- null if addition was successful or error message if not</returns>
        public String AddSong(String title);

        /// <summary>
        /// This method deletes a song given its title
        /// </summary>
        /// <param name="songTitle">The title of the song to delete</param>
        /// <returns>boolean true or false if delete was successful or not</returns>
        public Boolean DeleteSong(String songTitle);

        /// <summary>
        /// This method adds a song for the specified performer
        /// </summary>
        /// <param name="userId">The id of the performer</param>
        /// <param name="songName">The song name</param>
        /// <returns>String- null if addition was successful or error message if not</returns>
        public String AddSongForPerformer(int userId, String songName, String notes, bool allPerformersAdded = true);

        /// <summary>
        /// This method edits a performer's contact information, namely their phone number and email
        /// </summary>
        /// <param name="userId">The id of the performer whose information you are editing</param>
        /// <param name="phoneNumber">The phone number for the performer</param>
        /// <param name="email">The email for the performer</param>
        /// <returns>String- null if edit was successful or error message if not</returns>
        public String EditPerformerContact(int userId, String phoneNumber, String email);

        /// <summary>
        /// This asks the database to insert a performer. It also checks the length of the input.
        /// </summary>
        /// <param name="firstName">The first name of the performer</param>
        /// <param name="lastName">The last name of the performer</param>
        /// <param name="songs">This is always going to by null</param>
        /// <param name="email">The email of the performer</param>
        /// <param name="phoneNumber">The phone number of the perfoormer</param>
        /// <returns>A String that is null if everything worked if not it will return a error message.</returns>
        public (string ResultMessage, int PerformerId) AddPerformer(String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber, bool allPerformersAdded = true);

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

        public ObservableCollection<Rehearsal> GetPerformerMissedRehearsals(Performer performer);

        /// <summary>
        /// Gets all the performers from the Database
        /// </summary>
        /// <returns>a observableCollection of performers</returns>
        public ObservableCollection<Performer> GetPerformers();

        /// <summary>
        /// This method updates a performer's status- whether they are checked in, not checked in, or excused
        /// </summary>
        /// <param name="performer">The name of the performer</param>
        /// <param name="status">The status- checked in, not checked in, or excused</param>
        /// <returns>a tuple of a boolean based on the success of updating and a message</returns>
        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status);

        /// <summary>
        /// This method generates a new 7 digit access code for the performers and calls the database method to change the code
        /// </summary>
        public void GenerateNewAccessCode();

        /// <summary>
        /// This method gets all rehearsals
        /// </summary>
        /// <returns>an observable collection of all rehearsals</returns>
        public ObservableCollection<Rehearsal> GetAllRehearsals();

        /// <summary>
        /// This method gets all rehearsals that the particular performer is involved in
        /// </summary>
        /// <param name="performer">The performer whose rehearsals you want to get</param>
        /// <returns>The observable collection of all of that performer's rehearsals</returns>
        public ObservableCollection<Rehearsal> GetPerformerRehearsals(Performer performer);

        /// <summary>
        /// This method updates whether or not a performer is checked in for a specific rehearsal or not
        /// </summary>
        /// <param name="performer">The performer</param>
        /// <param name="rehearsal">The rehearsal</param>
        /// <param name="isCheckedIn">true or false if they are checked in or not for that rehearsal</param>
        /// <returns>a tuple of boolean true or false if the status was updated and a message</returns>
        public (bool success, string message) UpdatePerformerRehearsalStatus(Performer performer, Rehearsal rehearsal, String status);
        /// <summary>
        /// Removes a song from a performer's setlist
        /// </summary>
        /// <param name="performer">the performer</param>
        /// <param name="song">The song to remove</param>
        /// <returns></returns>
        public Boolean RemoveSongFromSetlist(Performer performer, Song song);





}
}

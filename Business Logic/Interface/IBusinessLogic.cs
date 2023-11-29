using Prototypes.Model;
using System.Collections.ObjectModel;
using Prototypes.Model.Interfaces;

namespace Prototypes.Business_Logic.Interface
{
    public interface IBusinessLogic
    {
        public String GetManagerAccessCode();
        public String GetChoreoAccessCode();
        public String GetPerformerAccessCode();
        public ObservableCollection<Performer> GetPerformersOfASong(Song song);
        public String EditPerformerName(int userId, String firstName, String lastName);
        public String EditSong(String oldSongName,String newSongName);
        public String AddSong(String title);
        /// <summary>
        /// A ObservableCollection of performer objects that contians all the performer the user has inputed
        /// </summary>
        public ObservableCollection<Performer> Performers { get; }
        public ObservableCollection<Song> Songs { get; }
        public ObservableCollection<Performer> GetCheckedInPerformers { get; }
        public ObservableCollection<Performer> GetNotCheckedInPerformers { get; }
        public ObservableCollection<Rehearsal> Rehearsals { get; }
        public Boolean DeleteSong(String songTitle);

        public String AddSongForPerformer(int userId, String songName);
        public String EditPerformerContact(int userId, String phoneNumber, String email);

        /// <summary>
        /// This asks the database to insert a performer. It also checks the length of the input.
        /// </summary>
        public String AddPerformer(String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber);

        /// <summary>
        /// Checks if the id is valid then asks the Database to delete the performer with that id
        /// </summary>
        public Boolean DeletePerformer(int userId);

        /// <summary>
        /// Creates a performer object to replace another performer object and send it to the Database
        /// </summary>
        public Boolean EditPerformer(int userId, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber);

        /// <summary>
        /// Find the performer object with the given id
        /// </summary>
        public Performer FindPerformer(int userId);


        /// <summary>
        /// Gets all the performers from the Database
        /// </summary>
        /// <returns>a ObservableCollection of performers objects the user has visted</returns>
        public ObservableCollection<Performer> GetPerformers();

        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status);
        public ObservableCollection<Rehearsal> GetAllRehearsals();
        public ObservableCollection<Rehearsal> GetPerformerRehearsals(Performer performer);
        public (bool success, string message) UpdatePerformerRehearsalStatus(Performer performer, Rehearsal rehearsal, bool isCheckedIn);
        public void GenerateNewAccessCode();
    }
}

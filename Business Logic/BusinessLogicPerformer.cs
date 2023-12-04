using Prototypes.Model;
using Prototypes.Business_Logic.Interface;
using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    public partial class BusinessLogic : IBusinessLogic
    {
        /// <summary>
        /// Gets all the performers from the Database
        /// </summary>
        /// <returns>a observableCollection of performers objects the user has visted</returns>
        public ObservableCollection<Performer> GetPerformers()
        {
            return Performers;
        }

        /// <summary>
        /// Gets the ObservableCollection of all performers
        /// </summary>
        public ObservableCollection<Performer> Performers
        {
            get { return Database.SelectAllPerformers(2023); }
        }
        /// <summary>
        /// Gets the ObservableCollection of all performers in a specific rehearsal
        /// </summary>
        /// <param name="rehearsal"></param>
        /// <returns></returns>
        public ObservableCollection<Performer> GetPerformersOfARehearsal(Rehearsal rehearsal)
        {
            return Database.SelectAllPerfomersFromRehearsal(rehearsal.Time, rehearsal.Song.Title);
        }

        /// <summary>
        /// This method gets the list of all the peformers of a certain song
        /// </summary>
        /// <param name="song">The song for which you want to know the performers</param>
        /// <returns>Observable Collection of the performers for that song</returns>
        public ObservableCollection<Performer> GetPerformersOfASong(Song song)
        {
            return Database.GetPerformersOfASong(song);
        }

        /// <summary>
        /// This asks the database to insert a performer. It also checks the length of the input.
        /// </summary>
        /// <param name="firstName">The first name of the performer</param>
        /// <param name="lastName">The last name of the performer</param>
        /// <param name="songs">This is always going to by null</param>
        /// <param name="email">The email of the performer</param>
        /// <param name="phoneNumber">The phone number of the perfoormer</param>
        /// <returns>A String that is null if everything worked if not it will return a error message.</returns>
        public String AddPerformer(String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber)
        {

            if (firstName.Length <= 255 || lastName.Length <= 255 || email.Length <= 255 || phoneNumber.Length <= 14)
            {
                //True if it did add it to the datebase false if it didn't
                bool answer = Database.InsertPerformer(firstName, lastName, songs, email, phoneNumber, 0);
                if (answer)
                {
                    return null;
                }
                else
                {
                    return "Could not add performer to database.";
                }

            }
            return "Invalid length of input.";

        }

        /// <summary>
        /// This method updates a performer's name by identifying them with an id, and using the given first and last name
        /// </summary>
        /// <param name="userId">The int id to identify the performer</param>
        /// <param name="firstName">The first name of the performer</param>
        /// <param name="lastName">The last name of the performer</param>
        /// <returns>String- null if update was successful or error message if not</returns>
        public String EditPerformerName(int userId, String firstName, String lastName)
        {
            if (firstName.Length <= 255 || lastName.Length <= 255)
            {
                //True if it did add it to the datebase false if it didn't
                bool answer = Database.UpdatePerformerName(userId, firstName, lastName); ;
                if (answer)
                {
                    return null;
                }
                else
                {
                    return "Could not edit performer in database.";
                }

            }
            return "Invalid length of input.";
        }

        /// <summary>
        /// This method edits a performer's contact information, namely their phone number and email
        /// </summary>
        /// <param name="userId">The id of the performer whose information you are editing</param>
        /// <param name="phoneNumber">The phone number for the performer</param>
        /// <param name="email">The email for the performer</param>
        /// <returns>String- null if edit was successful or error message if not</returns>
        public String EditPerformerContact(int userId, String phoneNumber, String email)
        {
            if (email.Length <= 255 || phoneNumber.Length <= 14)
            {
                //True if it did add it to the datebase false if it didn't
                bool answer = Database.UpdatePerformerContact(userId, phoneNumber, email);
                if (answer)
                {
                    return null;
                }
                else
                {
                    return "Could not edit performers contact info in the database.";
                }

            }
            return "Invalid length of input.";
            //Sends it to the Database

        }

        /// <summary>
        /// Checks if the id is valid then asks the Database to delete the performer with that id
        /// </summary>
        /// <param name="id">the id of the performer that is supposed to be deleted</param>
        /// <returns>true if the Database can delete the performer. False if the param is invalid or the performer could not be found in the Database</returns>
        public Boolean DeletePerformer(int userId)
        {
            //Sends it to the Database
            return Database.DeletePerformer(userId);
        }


        /// <summary>
        /// Creates a performer object to replace another performer object and send it to the Database
        /// </summary>
        /// <param name="id">the id of the performer</param>
        /// <param name="city">the city the performer is in</param>
        /// <param name="dateVisted">the date the user visited the performer</param>
        /// <param name="rating">the rating the gave to the performer</param>
        /// <returns>true if all the params are valid, false if the Database could not find the performer to edit or the params are invalid</returns>
        public Boolean EditPerformer(int userId, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber)
        {
            //Creates a new performer object
            Performer performer = new Performer(userId, firstName, lastName, songs, email, phoneNumber, 0);
            //Sends it to the Database
            return Database.UpdatePerformer(performer);
        }

        /// <summary>
        /// Find the performer object with the given id
        /// </summary>
        /// <param name="id">the id of the performer that is suppose to be found</param>
        /// <returns>the performer if it is found null if it is not found or invalid param</returns>
        public Performer FindPerformer(int userId)
        {
            //Sends it to Database
            return Database.SelectPerformer(userId);
        }
    }
}

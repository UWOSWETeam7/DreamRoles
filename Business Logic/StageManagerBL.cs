using Prototypes.StageManagerDB;
using Prototype.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Business_Logic
{
    class StageManegerBL : IStageManegerBL
    {

        //A interface of StageManagerDB
        private IStageManagerDB StageManagerDB = new StageManagerDB();
        const int ID_MAX_LENGTH = 4;
        const int ID_MIN_LENGTH = 3;
        const int CITY_MAX_LENGTH = 25;
        const int CITY_MIN_LENGTH = 1;
        const int RATING_MAX_LENGTH = 5;
        const int RATING_MIN_LENGTH = 0;


        /// <summary>
        /// Gets the ObservableCollection performers
        /// </summary>
        public ObservableCollection<Performer> Performers
        {
            get { return StageManagerDB.SelectAllPerformers(); }
        }

        /// <summary>
        /// Initializes a StageManagerDB when the program starts 
        /// </summary>
        public StageManegerBL()
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
        public Boolean AddPerformer(String id, String name, ObservableCollection<String> songs, String email, String phoneNumber)
        {
            //Creates a new performer object
            Performer performer = new Performer(id, name, songs, email, phoneNumber);
            //Sends it to the StageManagerDB and gets a true or false depending if it can add it to the StageManagerDB
            return StageManagerDB.Insertperformer(performer);
        }

        /// <summary>
        /// Checks if the id is valid then asks the StageManagerDB to delete the performer with that id
        /// </summary>
        /// <param name="id">the id of the performer that is supposed to be deleted</param>
        /// <returns>true if the StageManagerDB can delete the performer. False if the param is invalid or the performer could not be found in the StageManagerDB</returns>
        public Boolean DeletePerformer(String id)
        {
           //Sends it to the StageManagerDB
           return StageManagerDB.Deleteperformer(id);
        }

        /// <summary>
        /// Creates a performer object to replace another performer object and send it to the StageManagerDB
        /// </summary>
        /// <param name="id">the id of the performer</param>
        /// <param name="city">the city the performer is in</param>
        /// <param name="dateVisted">the date the user visited the performer</param>
        /// <param name="rating">the rating the gave to the performer</param>
        /// <returns>true if all the params are valid, false if the StageManagerDB could not find the performer to edit or the params are invalid</returns>
        public Boolean EditPerformer(String id, String name, ObservableCollection<String> songs, String email, String phoneNumber)
        {
            //Creates a new performer object
            Performer performer = new Performer(id, name, songs, email, phoneNumber);
            //Sends it to the StageManagerDB
            return StageManagerDB.UpdatePerformer(performer);
        }

        /// <summary>
        /// Find the performer object with the given id
        /// </summary>
        /// <param name="id">the id of the performer that is suppose to be found</param>
        /// <returns>the performer if it is found null if it is not found or invalid param</returns>
        public performer FindPerformer(String id)
        {
            //Sends it to StageManagerDB
            return StageManagerDB.Selectperformer(id);
        }

        /// <summary>
        /// Gets all the performers from the StageManagerDB
        /// </summary>
        /// <returns>a observableCollection of performers objects the user has visted</returns>
        public ObservableCollection<Performer> GetPerformers()
        {
            return Performers;
        }
    }

}
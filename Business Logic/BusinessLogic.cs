﻿using Prototypes.Model;
using Prototypes.Business_Logic.Interface;
using Prototypes.Databases.Interface;
using Prototypes.Databases;
using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    class BusinessLogic : IBusinessLogic
    {

        //A interface of Database
        private IDatabase Database = new Database();
        public String GetManagerAccessCode()
        {
            return Database.GetManagerAccessCode();
        }
        public String GetChoreoAccessCode()
        {
            return Database.GetChoreoAccessCode();
        }

        public String GetPerformerAccessCode()
        {
            return Database.GetPerformerAccessCode();
        }
        public ObservableCollection<Performer> GetPerformersOfASong(Song song)
        {
            return Database.GetPerformersOfASong(song);
        }
        public Boolean EditPerformerName(int userId, String firstName, String lastName)
        {
            return Database.UpdatePerformerName(userId, firstName, lastName);
        }
        public Boolean EditSong(int setlistId, String oldSongName, String oldArtist, String songName, String artist, int duration)
        {
            return Database.UpdateSong(setlistId, oldSongName, oldArtist, songName, artist, duration);
        }

        public Boolean AddSong(int setlistId, String title, String artist, int duration)
        {
            return Database.InsertSong(setlistId, title, artist, duration);
        }
        /// <summary>
        /// Gets the ObservableCollection performers
        /// </summary>
        public ObservableCollection<Performer> Performers
        {
            get { return Database.SelectAllPerformers(2023); }
        }
        public ObservableCollection<Performer> CheckedInPerformers
        {
            get { return Database.GetCheckedInPerformers(); }
        }
        public ObservableCollection<Song> Songs
        {
            get { return Database.SelectAllSongs(); }
        }
        public ObservableCollection<Performer> GetCheckedInPerformers
        {
            get { return Database.GetCheckedInPerformers(); }
        }

        public ObservableCollection<Performer> GetNotCheckedInPerformers
        {
            get { return Database.GetNotCheckedInPerformers(); }
        }
        public Boolean DeleteSong(String songTitle, String artistName)
        {
            return Database.DeleteSong(songTitle, artistName);
        }

        public Boolean AddSongForPerformer(int userId, String songName, String artistName, int duration)
        {
            return Database.InsertSongForPerformer(userId, songName, artistName, duration);
        }

        public Boolean EditPerformerContact(int userId, String phoneNumber, String email)
        {
            //Sends it to the Database
            return Database.UpdatePerformerContact(userId, phoneNumber, email);
        }

        /// <summary>
        /// Initializes a Database when the program starts 
        /// </summary>
        public BusinessLogic()
        {
            Database = new Database();
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
            //This checks the lengths of the input
            if(firstName.Length <= 255 || lastName.Length <= 255 || email.Length <= 255 || phoneNumber.Length <= 9)
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

        /// <summary>
        /// Gets all the performers from the Database
        /// </summary>
        /// <returns>a observableCollection of performers objects the user has visted</returns>
        public ObservableCollection<Performer> GetPerformers()
        {
            return Performers;
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

        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status)
        {
            return Database.UpdatePerformerStatus(performer, status);
        }

        public void GenerateNewAccessCode()
        {
            Random rand = new Random();
            int newCode = rand.Next(10000000);
            Database.UpdatePerformerAccessCode(newCode);
        }
       
    }

}
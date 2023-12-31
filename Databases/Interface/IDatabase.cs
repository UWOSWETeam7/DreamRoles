﻿using Prototypes.Model;
using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Prototypes.Databases.Interface
{
    public interface IDatabase
    {
        public Boolean DeleteAllTables();
        public ObservableCollection<Song> GetPerformerSetList(int performerId);
        public (bool success, String message) InsertIntoRehearsals(DateTime rehearsalTime, String songTitle);
        public (bool success, String message) DeleteRehearsal(DateTime rehearsalTime, String songTitle);
        public String GetManagerAccessCode();
        public String GetChoreoAccessCode();
        public String GetPerformerAccessCode();
        public ObservableCollection<Performer> GetPerformersOfASong(Song song);
        public Boolean UpdatePerformerName(int userId, String firstName, String lastName);
        public Boolean UpdateSong(String oldSongName, String newSongName);
        public Boolean InsertSong(String title);
        public ObservableCollection<Song> SelectAllSongs();
        public Boolean DeleteSong(String songTitle);
        public Boolean InsertSongForPerformer(int userId, String songName, String notes, bool allPerformersAdded = true);
        public Boolean DeleteSongFromSetlist(int userId, String songTitle);
        public Boolean UpdatePerformerContact(int userId, String phoneNumber, String email);
        public (bool success, string message) UpdatePerformerRehearsalStatus(Performer performer, Rehearsal rehearsal, String status);
        public ObservableCollection<Performer> GetCheckedInPerformers(Rehearsal rehearsal);
        public ObservableCollection<Performer> GetNotCheckedInPerformers(Rehearsal rehearsal);
        /// <summary>
        /// Creates a file if needed and reads it and puts performer objects into a ObservableCollection
        /// </summary>
        /// <returns>a ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> SelectAllPerformers(int year);

        public ObservableCollection<Performer> SelectAllPerfomersFromRehearsal(DateTime rehearsalTime, String songTitle);

        /// <summary>
        /// Uses the given id to find a Aiport object with that id in a ObservableCollection
        /// </summary>
        /// <param name="id">the id of the performer that the user is trying to find</param>
        /// <returns>the performer if it is in the ObservableCollection. Returns null if the performer is not found</returns>
        public Performer SelectPerformer(int userId);

        public ObservableCollection<Rehearsal> SelectPerformerAbsences(int userId);

        /// <summary>
        /// Adds a new performer object into the ObservableCollection and the file
        /// </summary>
        /// <param name="performer">the performer that the user wants to put into the file</param>
        /// <returns>True if it was inserted into the ObservableCollection and the file, false otherwise</returns>
        public (bool Success, int UserId) InsertPerformer(String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber, int absences, bool allPerformersAdded = true);

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
        public ObservableCollection<Rehearsal> GetAllRehearsals();
        public ObservableCollection<Rehearsal> GetPerformerRehearsals(Performer performer);
        public (bool success, String message) InsertIntoRehersalMembers(int userId, DateTime rehearsalTime, String checkedIn, string songName);
        public (bool success, String message) DeleteRehearsalMember(int userId, DateTime rehearsalTime, String songName);

    }
}

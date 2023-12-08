using Prototypes.Model;
using Prototypes.Business_Logic.Interface;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    public partial class BusinessLogic : IBusinessLogic
    {
        /// <summary>
        /// Gets the ObservableCollection of all songs
        /// </summary>
        public ObservableCollection<Song> Songs
        {
            get { return Database.SelectAllSongs(); }
        }

        /// <summary>
        /// This method adds a given song
        /// </summary>
        /// <param name="title">The title of the song to add</param>
        /// <returns>String- null if addition was successful or error message if not</returns>
        public String AddSong(String title)
        {
            if (title.Length <= 255)
            {
                bool answer = Database.InsertSong(title);
                if (answer)
                {
                    return null;
                }
                else
                {
                    return "Song could not be added to database";
                }
            }
            return "Invalid length of input";
        }

        /// <summary>
        /// This method updates an exisiting song's name given its old name to identify it and the new one
        /// </summary>
        /// <param name="oldSongName">The name of the song to update (its old name)</param>
        /// <param name="newSongName">The new name of the song</param>
        /// <returns>String- null if update was successful or error message if not</returns>
        public String EditSong(String oldSongName, String newSongName)
        {
            if (newSongName.Length <= 255)
            {
                bool answer = Database.UpdateSong(oldSongName, newSongName);
                if (answer)
                {
                    return null;
                }
                else
                {
                    return "Song could not be edited in database";
                }
            }
            return "Invalid length of input";
        }

        /// <summary>
        /// This method deletes a song given its title
        /// </summary>
        /// <param name="songTitle">The title of the song to delete</param>
        /// <returns>boolean true or false if delete was successful or not</returns>
        public Boolean DeleteSong(String songTitle)
        {
            return Database.DeleteSong(songTitle);
        }

        /// <summary>
        /// This method adds a song for the specified performer
        /// </summary>
        /// <param name="userId">The id of the performer</param>
        /// <param name="songName">The song name</param>
        /// <returns>String- null if addition was successful or error message if not</returns>
        public String AddSongForPerformer(int userId, String songName, String notes)
        {
            bool answer = Database.InsertSongForPerformer(userId, songName, notes);
            if (answer)
            {
                return null;
            }
            else
            {
                return "Could not add song to performer in the database.";
            }

        }
    }
}

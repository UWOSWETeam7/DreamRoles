using Prototypes.Business_Logic.Interface;
using Prototypes.Databases.Interface;
using Prototypes.Databases;

namespace Prototypes.Business_Logic
{
    public partial class BusinessLogic : IBusinessLogic
    {

        //A interface of Database
        private IDatabase Database = new Database();

        /// <summary>
        /// Initializes a Database when the program starts 
        /// </summary>
        public BusinessLogic()
        {
            Database = new Database();
        }

        /// <summary>
        /// This method gets the stage manager's access code by calling the DB method by the same name
        /// </summary>
        /// <returns>String- the 7 digit access code</returns>
        public String GetManagerAccessCode()
        {
            return Database.GetManagerAccessCode();
        }

        /// <summary>
        /// This method gets the choreographers's access code by calling the DB method by the same name
        /// </summary>
        /// <returns>String- the 7 digit access code</returns>
        public String GetChoreoAccessCode()
        {
            return Database.GetChoreoAccessCode();
        }

        /// <summary>
        /// This method gets the performer's access code by calling the DB method by the same name
        /// </summary>
        /// <returns>String- the 7 digit access code</returns>
        public String GetPerformerAccessCode()
        {
            return Database.GetPerformerAccessCode();
        }

        /// <summary>
        /// This method generates a new 7 digit access code for the performers and calls the database method to change the code
        /// </summary>
        public void GenerateNewAccessCode()
        {
            Random rand = new Random();
            int newCode = rand.Next(10000000);
            Database.UpdatePerformerAccessCode(newCode);
        }  
    }
}
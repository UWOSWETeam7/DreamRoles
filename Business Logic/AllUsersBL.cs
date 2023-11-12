using Prototypes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Business_Logic.IBusinessLogic
{
    class AllUsersBL
    {
        int performerAccessCode = 1122333;
        int stageManagerAccessCode = 4455666;
        int choreographerAccessCode = 7788999;
        PerformerDB PerformerDB = new PerformerDB();
        StageManagerDB StageManagerDB = new StageManagerDB();
        ChoreographerDB ChoreographerDB = new ChoreographerDB();

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            int enteredText = Int32.Parse(e.NewTextValue);
            CheckRole(enteredText);
        }
        public void CheckRole(int accessCode)
        {
            if (accessCode == performerAccessCode)
            {
                //display performers
                ShowPerformerNames(true);

            } else if (accessCode ==  stageManagerAccessCode)
            {
                //display stage managers

            } else if (accessCode ==  choreographerAccessCode)
            {
                //display choreographers

            } else
            {
                throw new Exception();
            }
            
        }

        public void ShowPerformerNames(Boolean show)
        {
            StageManagerDB.SelectAllPerformers();
        }

        public void ShowStageManagerNames(Boolean show)
        {
            StageManagerDB.SelectAllStageManagers();
        }

        public void ShowChoreographerNames(Boolean show)
        {

        }
    }
}

using Prototypes.Model;
using Prototypes.Business_Logic.IBusinessLogic;
using Prototypes.Database;
using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Prototypes.Business_Logic
{
    class PerformerBL : IPerformerBL
    {
        private IPerformerDB PerformerDB = new PerformerDB();

        int performerAccessCode = 001122;
        public Boolean IsPerformer(int accessCode)
        {
            Boolean isPerformer = false;
            if (accessCode == performerAccessCode)
            {
                isPerformer = true;
            }
            return isPerformer;
        }

        public PerformerBL()
        {
            PerformerDB = new PerformerDB();
        }

        public void UpdateStatus(Performer performer)
        {

        }
    }
}

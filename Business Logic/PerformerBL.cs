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

        public PerformerBL()
        {
            PerformerDB = new PerformerDB();
        }

        public void UpdateStatus(Performer performer)
        {

        }
    }
}

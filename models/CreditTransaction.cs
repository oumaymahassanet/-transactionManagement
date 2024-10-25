using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace testApp.models
{
    public class CreditTransaction : AbstractTransaction
    {
        public override string Type => "Credit";
    }

}

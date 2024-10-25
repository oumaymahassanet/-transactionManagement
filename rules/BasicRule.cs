using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using testApp.models;

namespace testApp.rules
{
    public interface BasicRule
    {
        bool IsValid(AbstractTransaction transaction, decimal currentBalance);
        string ErrorMessage { get; }
    }
}

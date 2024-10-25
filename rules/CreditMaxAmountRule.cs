using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using testApp.models;

namespace testApp.rules
{
    public class CreditMaxAmountRule : BasicRule
    {
        private const decimal MaxCreditAmount = 10000;

        public string ErrorMessage { get; private set; }

        public bool IsValid(AbstractTransaction transaction, decimal currentBalance)
        {
            if (transaction is CreditTransaction && transaction.Amount > MaxCreditAmount)
            {
                ErrorMessage = $"Le montant du crédit ne peut pas dépasser {MaxCreditAmount}.";
                return false;
            }
            return true;
        }
    }
}

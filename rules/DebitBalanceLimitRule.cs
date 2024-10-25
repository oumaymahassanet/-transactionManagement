using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using testApp.models;

namespace testApp.rules
{
    public class DebitBalanceLimitRule : BasicRule
    {
        private const decimal MinBalance = -5000;

        public string ErrorMessage { get; private set; }

        public bool IsValid(AbstractTransaction transaction, decimal currentBalance)
        {
            if (transaction is DebitTransaction && currentBalance - transaction.Amount < MinBalance)
            {
                ErrorMessage = $"Le solde ne peut pas être inférieur à {MinBalance} après une transaction de débit.";
                return false;
            }
            return true;
        }
    }
}

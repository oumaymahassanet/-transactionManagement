using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using testApp.models;
using testApp.rules;

namespace testApp.services
{
    public class TransactionService
    {
        private decimal _balance;
        private readonly List<BasicRule> _validationRules;

        public TransactionService(decimal initialBalance)
        {
            _balance = initialBalance;
            _validationRules = new List<BasicRule>();
        }

        public void AddValidationRule(BasicRule rule)
        {
            _validationRules.Add(rule);
        }

        public bool AddTransaction(AbstractTransaction transaction, out string validationMessage)
        {
            foreach (var rule in _validationRules)
            {
                if (!rule.IsValid(transaction, _balance))
                {
                    validationMessage = rule.ErrorMessage;
                    return false;
                }
            }

            // Appliquer la transaction si elle est valide
            if (transaction is DebitTransaction)
            {
                _balance -= transaction.Amount;
            }
            else if (transaction is CreditTransaction)
            {
                _balance += transaction.Amount;
            }

            validationMessage = "Transaction valide et ajoutée avec succès.";
            return true;
        }

        public decimal GetBalance()
        {
            return _balance;
        }
    }
}

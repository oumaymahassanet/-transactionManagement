using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApp.models;
using testApp.rules;
using testApp.services;
using Xunit;

namespace testApp.tests
{
    public class TransactionServiceTests
    {
        [Fact]
        public void AddTransaction_ShouldFail_WhenCreditExceedsLimit()
        {
            // Arrange
            var service = new TransactionService(5000);
            service.AddValidationRule(new CreditMaxAmountRule());

            var creditTransaction = new CreditTransaction
            {
                Date = DateTime.Now,
                Amount = 12000, // Dépasse la limite de 10 000
                Description = "Crédit invalide"
            };

            // Act
            var result = service.AddTransaction(creditTransaction, out string validationMessage);

            // Assert
            Assert.False(result);
            Assert.Equal("Le montant du crédit ne peut pas dépasser 10000.", validationMessage);
        }

        [Fact]
        public void AddTransaction_ShouldFail_WhenDebitCausesNegativeBalance()
        {
            // Arrange
            var service = new TransactionService(1000); // Solde initial de 1000
            service.AddValidationRule(new DebitBalanceLimitRule());

            var debitTransaction = new DebitTransaction
            {
                Date = DateTime.Now,
                Amount = 7000, // Provoque un solde inférieur à -5000
                Description = "Débit invalide"
            };

            // Act
            var result = service.AddTransaction(debitTransaction, out string validationMessage);

            // Assert
            Assert.False(result);
            Assert.Equal("Le solde ne peut pas être inférieur à -5000 après une transaction de débit.", validationMessage);
        }

        [Fact]
        public void GetBalance_ShouldReturn_CorrectBalanceAfterTransactions()
        {
            // Arrange
            var service = new TransactionService(5000);
            service.AddValidationRule(new CreditMaxAmountRule());
            service.AddValidationRule(new DebitBalanceLimitRule());

            var creditTransaction = new CreditTransaction
            {
                Date = DateTime.Now,
                Amount = 3000,
                Description = "Crédit"
            };

            var debitTransaction = new DebitTransaction
            {
                Date = DateTime.Now,
                Amount = 2000,
                Description = "Débit"
            };

            // Act
            service.AddTransaction(creditTransaction, out string _);
            service.AddTransaction(debitTransaction, out string _);

            var balance = service.GetBalance();

            // Assert
            Assert.Equal(6000, balance); // Solde attendu après les transactions
        }


    }
}


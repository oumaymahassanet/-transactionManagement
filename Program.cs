using testApp.models;
using testApp.rules;
using testApp.services;

class Program

{
    static void Main()
    {
        // Créer un service de transaction avec un solde initial de 5000
        var transactionService = new TransactionService(5000);

        // Ajouter des règles de validation
        transactionService.AddValidationRule(new CreditMaxAmountRule());
        transactionService.AddValidationRule(new DebitBalanceLimitRule());

        // Créer des transactions
        var creditTransaction = new CreditTransaction
        {
            Date = DateTime.Now,
            Amount = 9000, // Dépasse la limite
            Description = "Paiement client"
        };

        var debitTransaction = new DebitTransaction
        {
            Date = DateTime.Now,
            Amount = 11000,
            Description = "Achat matériel"
        };

        // Essayer d'ajouter les transactions
        string validationMessage;

        if (transactionService.AddTransaction(creditTransaction, out validationMessage))
        {
            Console.WriteLine("Transaction ajoutée : Crédit.");
        }
        else
        {
            Console.WriteLine($"Erreur : {validationMessage}");
        }

        if (transactionService.AddTransaction(debitTransaction, out validationMessage))
        {
            Console.WriteLine("Transaction ajoutée : Débit.");
        }
        else
        {
            Console.WriteLine($"Erreur : {validationMessage}");
        }

        // Afficher le solde actuel
        Console.WriteLine($"Solde actuel : {transactionService.GetBalance()}");
    }
}
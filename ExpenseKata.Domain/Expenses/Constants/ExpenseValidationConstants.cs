﻿namespace ExpenseKata.Domain.Expenses.Constants
{
    public static class ExpenseValidationConstants
    {
        public const string ExpenseShouldHaveCommentMessage = "Le commentaire est obligatoire";

        public const string ExpenseCannotBeInFuture = "Une dépense ne peut pas avoir une date dans le futur";

        public const string ExpenseCannotBeOlderThanThreeMonth = "Une dépense ne peut pas être datée de plus de 3 mois";

        public const string ExpenseShouldHaveSameCurrencyThanUser = "La devise de la dépense doit être identique à celle de l'utilisateur";
    }
}
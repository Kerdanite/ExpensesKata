namespace ExpenseKata.Domain.Expenses.Constants
{
    public static class ExpenseValidationConstants
    {
        public const string ExpenseShouldHaveCommentMessage = "Le commentaire est obligatoire";

        public const string ExpenseCannotBeInFuture = "Une dépense ne peut pas avoir une date dans le futur";
    }
}
# ExpensesKata

Le test consiste à réaliser une application web en .NET Core 3.1 avec une API pour :

    Créer des dépenses
    Lister les dépenses

Qu'est-ce qu'une dépense ?

Une dépense est caractérisée par :

    Un utilisateur (personne qui a effectué l'achat)
    Une date
    Une nature (valeurs possibles : Restaurant, Hotel et Misc)
    Un montant et une devise
    Un commentaire

Un utilisateur est caractérisé par :

    Un nom de famille
    Un prénom
    Une devise dans laquelle il effectue ses achats

Création d'une dépense

Avant de persister la dépense, il faut vérifier qu'elle soit valide.

Règles de validation d'une dépense :

    Une dépense ne peut pas avoir une date dans le futur,
    Une dépense ne peut pas être datée de plus de 3 mois,
    Le commentaire est obligatoire,
    Un utilisateur ne peut pas déclarer deux fois la même dépense (même date et même montant),
    La devise de la dépense doit être identique à celle de l'utilisateur.

Liste des dépenses

L'API doit permettre de :

    Lister les dépenses pour un utilisateur donné,
    Trier les dépenses par montant ou par date,
    Afficher toutes les propriétés de la dépense ; l'utilisateur de la dépense doit apparaitre sous la forme {FirstName} {LastName} (eg: "Anthony Stark").

Stockage

Les données doivent être persistées dans SQL Server via Entity Framework.

La table des utilisateurs doit être initialisée avec les utilisateurs suivants :

    Stark Anthony (devise = Dollar américain),
    Romanova Natasha (devise = Rouble russe).

Test unitaires

Les règles de validation de la dépense doivent être testées unitairement (avec xUnit).
Notes

    Pas besoin d'authentification,
    Aucune interface utilisateur requise.

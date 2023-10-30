CREATE TABLE [dbo].[Transaction] (
    [TransactionID]          INT             NOT NULL,
    [BudgetID]               INT             NOT NULL,
    [UserID]                 INT             NOT NULL,
    [TransactionTypeID]      INT             NOT NULL,
    [TransactionAmount]      DECIMAL (18, 2) NOT NULL,
    [TransactionDescription] NVARCHAR (100)  NOT NULL,
    PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    FOREIGN KEY ([TransactionTypeID]) REFERENCES [dbo].[TransactionType] ([TransactionTypeID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);


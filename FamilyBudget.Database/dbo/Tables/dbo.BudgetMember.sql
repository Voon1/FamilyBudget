CREATE TABLE [dbo].[BudgetMember] (
    [BudgetMemberID] INT NOT NULL,
    [UserID]         INT NOT NULL,
    PRIMARY KEY CLUSTERED ([BudgetMemberID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);


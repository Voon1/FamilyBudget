CREATE TABLE [dbo].[Budget] (
    [BudgetID]          INT            NOT NULL,
    [BudgetName]        NVARCHAR (100) NOT NULL,
    [BudgetOwnerUserID] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([BudgetID] ASC)
);


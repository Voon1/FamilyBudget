CREATE TABLE [dbo].[BudgetCategory] (
    [BudgetCategoryID]     INT NOT NULL,
    [BudgetID]             INT NOT NULL,
    [BudgetCategoryTypeID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([BudgetCategoryID] ASC),
    FOREIGN KEY ([BudgetCategoryTypeID]) REFERENCES [dbo].[BudgetCategoryType] ([BudgetCategoryTypeID]),
    FOREIGN KEY ([BudgetID]) REFERENCES [dbo].[Budget] ([BudgetID])
);


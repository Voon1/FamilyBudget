CREATE TABLE [dbo].[BudgetCategoryType] (
    [BudgetCategoryTypeID]   INT            NOT NULL IDENTITY(1,1),
    [BudgetCategoryTypeName] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([BudgetCategoryTypeID] ASC)
);


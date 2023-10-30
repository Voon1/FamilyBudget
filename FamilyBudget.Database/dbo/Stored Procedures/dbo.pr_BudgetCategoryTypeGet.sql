
CREATE PROCEDURE [dbo].[pr_BudgetCategoryTypeGet] 
@BudgetCategoryTypeId [int] = null,
@BudgetCategoryTypeName nvarchar(100) = null
AS
BEGIN
    SET NOCOUNT ON;

	SELECT BudgetCategoryTypeId, BudgetCategoryTypeName	 FROM BudgetCategoryType
	WHERE (
	(@BudgetCategoryTypeId IS NULL OR BudgetCategoryTypeID = @BudgetCategoryTypeId)
	AND (@BudgetCategoryTypeName IS NULL
	OR BudgetCategoryTypeName LIKE '%' + @BudgetCategoryTypeName + '%')
	)

END;
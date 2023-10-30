
CREATE PROCEDURE [dbo].[pr_BudgetCategoryTypeGet] 
@BudgetCategoryTypeId [int] = null
AS
BEGIN
    SET NOCOUNT ON;


	SELECT BudgetCategoryTypeId, BudgetCategoryTypeName	 FROM BudgetCategoryType
	WHERE (@BudgetCategoryTypeId IS NULL OR
			BudgetCategoryTypeId = @BudgetCategoryTypeId)

END;
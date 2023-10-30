
CREATE PROCEDURE [dbo].[pr_BudgetCategoryTypeInsert] 
@BudgetCategoryTypeName NVARCHAR(100) = null
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO BudgetCategoryType (BudgetCategoryTypeName)
	SELECT @BudgetCategoryTypeName

	SELECT @@ROWCOUNT

END;



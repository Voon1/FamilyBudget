/*
Post-Deployment Script Dictionary Values							
--------------------------------------------------------------------------------------
Post deploy script that creates dictionary Data.
Frankly, we should have ledger that would reporesent it as +/- val.
Just for DEMOwe have two sides of operations Income + and Expenses - side.
Missing my red gate intellisense :(((((((			
--------------------------------------------------------------------------------------
*/


SELECT
	'Dictionary: TransactionType';
SELECT
	'Dictionary data pre upgrade';
SELECT
	TransactionTypeID
   ,TransactionTypeDescription
FROM TransactionType tt;


INSERT INTO TransactionType (TransactionTypeID, TransactionTypeDescription)
	SELECT
		Z.TransactionTypeID
	   ,Z.TransactionTypeDescription
	FROM (SELECT
			1 AS TransactionTypeID
		   ,'Income' AS TransactionTypeDescription
		UNION
		SELECT
			2 AS TransactionTypeID
		   ,'Expense' AS TransactionTypeDescription) Z
	LEFT JOIN TransactionType tt
		ON tt.TransactionTypeID = Z.TransactionTypeID
	WHERE tt.TransactionTypeID IS NULL;

SELECT
	'Dictionary data post upgrade';
SELECT
	TransactionTypeID
   ,TransactionTypeDescription
FROM TransactionType tt;



SELECT
	'Dictionary: BudgetCategoryType';
SELECT
	'Dictionary data pre upgrade';
SELECT
	bct.BudgetCategoryTypeID
   ,bct.BudgetCategoryTypeName
FROM BudgetCategoryType bct;

INSERT INTO dbo.BudgetCategoryType (BudgetCategoryTypeID, BudgetCategoryTypeName)
	SELECT
		Z.BudgetCategoryTypeID
	   ,Z.BudgetCategoryTypeName
	FROM (SELECT
			1 AS BudgetCategoryTypeID
		   ,'Household' AS BudgetCategoryTypeName
		UNION
		SELECT
			2 AS TransactionTypeID
		   ,'Utilities' AS BudgetCategoryTypeName
		UNION
		SELECT
			3 AS TransactionTypeID
		   ,'Car' AS BudgetCategoryTypeName) Z
	LEFT JOIN BudgetCategoryType tt
		ON tt.BudgetCategoryTypeID = Z.BudgetCategoryTypeID
	WHERE tt.BudgetCategoryTypeName IS NULL;

SELECT
	'Dictionary data post upgrade';
SELECT
	bct.BudgetCategoryTypeID
   ,bct.BudgetCategoryTypeName
FROM BudgetCategoryType bct;


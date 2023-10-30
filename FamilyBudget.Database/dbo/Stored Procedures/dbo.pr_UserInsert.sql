
CREATE PROCEDURE [dbo].[pr_UserInsert] 
@UserName nvarchar(100),
@UserPassword nvarchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO [User] (UserName,UserPassword )
	SELECT @UserName,@UserPassword

	SELECT @@ROWCOUNT

END;



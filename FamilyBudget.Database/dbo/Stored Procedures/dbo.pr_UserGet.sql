
CREATE PROCEDURE [dbo].[pr_UserGet] 
@UserName nvarchar(100),
@UserPassword nvarchar(100)
AS
BEGIN
--fffffff no time no hashing

    SET NOCOUNT ON;

	SELECT UserId, UserName	 FROM [User] --ehhh
	WHERE UserName = @UserName AND UserPassword = @UserPassword

END;
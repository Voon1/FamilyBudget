CREATE TABLE [dbo].[User] (
    [UserID]   INT            NOT NULL IDENTITY(1,1),
    [UserName] NVARCHAR (100) NOT NULL,
    [UserPassword] NVARCHAR(100) NOT NULL, 
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);


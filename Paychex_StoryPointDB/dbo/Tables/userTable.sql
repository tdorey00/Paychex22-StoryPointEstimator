CREATE TABLE [dbo].[userTable]
(
	[userId] INT NOT NULL PRIMARY KEY,
	[userName]  varchar(50) NOT NULL,
	[isAdmin] BIT NOT NULL DEFAULT (0),
	[vote]  varchar(2)
)

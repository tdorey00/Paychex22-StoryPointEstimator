CREATE TABLE [dbo].[userTable]
(
	[userId] INT NOT NULL PRIMARY KEY,
	[userName]  varchar(50) NOT NULL,
	[isAdmin] BIT NOT NULL DEFAULT (0),
	[fibVote] VARCHAR(20),
	[fistVote] VARCHAR(3),
	[scaleVote] VARCHAR (3),
	[tshirtVote] VARCHAR(3),
	[joinDate] DATETIME NOT NULL DEFAULT(GETUTCDATE())
)

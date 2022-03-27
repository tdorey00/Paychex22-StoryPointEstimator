CREATE PROCEDURE [dbo].[RemoveAllData]
AS
	DROP TABLE dbo.roomUserTable
	
	TRUNCATE TABLE dbo.roomTable
	TRUNCATE TABLE dbo.userTable

	CREATE TABLE [dbo].[roomUserTable]
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		[roomId] INT NOT NULL FOREIGN KEY REFERENCES roomTable (roomId),
		[userId] INT NOT NULL FOREIGN KEY REFERENCES userTable (userId),
		[joinDate] DATETIME NOT NULL DEFAULT(GETUTCDATE())
	);
RETURN 0

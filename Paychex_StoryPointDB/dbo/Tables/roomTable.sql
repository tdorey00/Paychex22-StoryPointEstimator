CREATE TABLE [dbo].[roomTable]
(
	[roomId] INT NOT NULL PRIMARY KEY,
	[roomName] VARCHAR(100) NOT NULL,
	[currentTab] INT NOT NULL DEFAULT(1), --Not currently used
	[24ScaleTitle] VARCHAR(100) NULL,
	[24Scale] INT NOT NULL DEFAULT(24),
	[creationDate] datetime NOT NULL DEFAULT(GETUTCDATE())
)

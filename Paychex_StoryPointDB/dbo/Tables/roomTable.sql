CREATE TABLE [dbo].[roomTable]
(
	[roomId] INT NOT NULL PRIMARY KEY,
	[roomName] VARCHAR(100) NOT NULL,
	[currentTab] INT NOT NULL DEFAULT(1),
	[24ScaleTitle] VARCHAR(50) NOT NULL DEFAULT('Enter A Title'),
	[creationDate] datetime NOT NULL DEFAULT(GETUTCDATE())
)

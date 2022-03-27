CREATE TABLE [dbo].[roomTable]
(
	[roomId] INT NOT NULL PRIMARY KEY,
	[roomName] VARCHAR(100) NOT NULL,
	[scaleTitle] VARCHAR(100) NULL,
	[currentScale] INT NOT NULL DEFAULT(24),
	[creationDate] datetime NOT NULL DEFAULT(GETUTCDATE())
)

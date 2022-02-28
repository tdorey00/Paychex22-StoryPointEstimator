CREATE PROCEDURE [dbo].[ExampleDataInsert]
AS
	INSERT INTO dbo.roomTable(roomId, roomName)
	VALUES (1234, 'Testing Room');
	INSERT INTO dbo.userTable(userId, userName, isAdmin)
	VALUES (6969, 'I hope this works :)', 1);
	INSERT INTO dbo.roomUserTable(roomId, userId)
	VALUES (1234, 6969);
RETURN 0

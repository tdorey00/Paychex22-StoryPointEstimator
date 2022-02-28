CREATE PROCEDURE [dbo].[RemoveExampleData]
AS
	DELETE FROM dbo.roomUserTable WHERE userId = 6969 AND roomId = 1234;
	DELETE FROM dbo.userTable WHERE userId = 6969;
	DELETE FROM dbo.roomTable WHERE roomId = 1234;
RETURN 0

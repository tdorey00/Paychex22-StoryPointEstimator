namespace StoryPointEstimatorBlazorApp.Hubs
{
    public class connectionModel
    {
        public connectionModel(string connection, int user, int room)
        {
            connectionId = connection;
            userId = user;
            roomId = room;
        }
        public string connectionId { get; set; } = "";
        public int userId { get; set; }
        public int roomId { get; set; }

        public override bool Equals(object? obj)
        {
            connectionModel otherConnection;
            try
            {
                otherConnection = (connectionModel)obj;
                return userId == otherConnection.userId && connectionId == otherConnection.connectionId && roomId == otherConnection.roomId;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "ConnectionID: " + connectionId + " userId: " + userId + " roomId: " + roomId;
        }
    }
}
